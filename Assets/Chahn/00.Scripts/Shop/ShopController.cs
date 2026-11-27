using System;
using System.Collections.Generic;
using System.Linq;
using Chahn._00.Scripts.Shop;
using Tyu.Scripts.Modules;
using UnityEngine;
using Random = UnityEngine.Random;


public class ShopController : MonoBehaviour
{
    [SerializeField] ShopConfig shopConfig;
    
    private CurrencyWallet currencyWallet => GameSession.Instance.CurrencyWallet;
    private PassiveManager passiveManager => GameSession.Instance.PassiveManager;
    public bool HasOffers => CurrentOffers.Count > 0; //이미 상점이 한번 뽑혔는지 확인한다 (상점이 처음 뽑혔을때는 리스트가 비어있어야 한다)
    
    private readonly List<ShopOffer> _currentOffers = new();
    public IReadOnlyList<ShopOffer> CurrentOffers => _currentOffers; //접근할때
    public event Action<IReadOnlyList<ShopOffer>> OnOffersGenerated;

    [SerializeField] private MonoBehaviour healthProvider;
    private IHealthHavable playerHealth;
    private bool hasHealedThisVisit; //체력회복은 상점 들어오고 나서 딱 한번만 가능하기 때문에 추가함

    private void Awake()
    {
        if (healthProvider is not IHealthHavable provider)
        {
            Debug.LogError("플레이어가 인터페이스 상속을 안함");
            return;
        }
        
        playerHealth = provider;
    }
    

    #region BuyOffers
    public void GenerateOffers()
    {
        _currentOffers.Clear();

        if (shopConfig == null)
        {
            Debug.LogError("ShopConfig연결 안됨");
            return;
        }
        
        List<PassiveData> allPassives = ClonePassivesList();

        if (allPassives.Count < shopConfig.DisplayCount) //팔수 있는 상품 개수보다 존재하는 상품이 적으면 리턴
            return;

        for (int i = 0; i < shopConfig.DisplayCount; i++)
        {
            int randomIdx = Random.Range(0, allPassives.Count);
            PassiveData selectedPassive = allPassives[randomIdx];
            allPassives.RemoveAt(randomIdx);
            
            ShopLevelConfig selectedLevel = RollLevelConfig();

            if (selectedLevel == null)
            {
                Debug.LogError("상품 레벨 추첨 실패함");
                _currentOffers.Clear();
                return;
            }
            
            ShopOffer offer = new ShopOffer(selectedPassive, selectedLevel.Level, selectedLevel.PurchasePrice);
            _currentOffers.Add(offer);
        }
        
        OnOffersGenerated?.Invoke(CurrentOffers); //지금 뽑은 5개의 상점 상품들을 넘겨준다
    }

    private ShopLevelConfig RollLevelConfig()
    {
        float totalWeight = 0f;

        foreach (ShopLevelConfig config in shopConfig.LevelConfigs)
        {
            if (config == null || config.Weight <= 0f)
            {
                Debug.LogWarning("레벨에 대한 정보가 없다 또는 레벨에 대한 가중치가 음수이다");
                continue;
            }
            
            totalWeight += config.Weight;
        }

        if (totalWeight <= 0f) return null;
        
        float accumulatedWeight = 0f;
        ShopLevelConfig lastValidConfig = null;
        float randomValue = Random.Range(0f, totalWeight);
        foreach (ShopLevelConfig config in shopConfig.LevelConfigs)
        {
            if (config == null || config.Weight <= 0f)
                continue;
            
            accumulatedWeight += config.Weight;
            lastValidConfig = config;
            
            if (randomValue < accumulatedWeight)
                return config;
        }
        
        return lastValidConfig;
    }

    private List<PassiveData> ClonePassivesList()
    {
        List<PassiveData> passives = new();

        foreach (PassiveData passive in shopConfig.PassivePool)
        {
            if (passive == null)
                continue;
            
            if (passives.Contains(passive))
                continue;

            bool isMaxLevelOwned = passiveManager.OwnedPassives.Any(owned =>
                owned != null &&
                owned.Data == passive &&
                owned.Level >= 3); //보유하고 있는 Lvl.3 패시브는 후보 제외시킨다

            if (isMaxLevelOwned)
                continue;
            
            passives.Add(passive);
        }
            
        return passives;
    }

    public bool TryRefreshOffers()
    {
        if (currencyWallet == null) 
        {
            Debug.LogError("currencyWallet is null");
            return false;
        }
        
        if (shopConfig == null)
        {
            Debug.LogError("shopConfig is null");
            return false;
        }
        
        if (!currencyWallet.TrySpend(shopConfig.RefreshCost))
            return false;

        GenerateOffers();
        Debug.Log("Refreshed offers");
        return true;
    }

    public void EnsureOffersGenerated()
    {
        if (HasOffers) return;
        
        Debug.Log("Genrated offers");
        GenerateOffers();
    }

    public bool TryPurchaseOffer(ShopOffer offer)
    {
        if (offer == null) return false;
        
        if (!_currentOffers.Contains(offer)) return false;

        if (offer.IsPurchased) return false;
        
        if (currencyWallet == null || passiveManager == null) return false;

        PassiveBase passive = PassiveFactory.Create(offer.PassiveData);
        
        if (passive == null) return false;
        
        while (passive.Level < offer.Level) //상품의 레벨로 맞춘다
        {
            passive.LevelUp();
        }
        
        if (!currencyWallet.TrySpend(offer.Price)) return false;

        if (!passiveManager.AddPassive(passive))
        {
            currencyWallet.Add(offer.Price);
            return false;
        }

        offer.MarkPurchased();
        return true;
    }
    
    #endregion
    
    #region SellOffer

    public int GetSellPrice(PassiveBase passive)
    {
        if (passive == null || shopConfig == null) return 0;

        foreach (var config in shopConfig.LevelConfigs)
        {
            if (config == null) continue;
            
            if (config.Level == passive.Level) 
                return config.SellPrice;
        }
        
        return 0;
    }
    
    public bool TrySellPassives(IReadOnlyList<PassiveBase> passives)
    {
        if (passives == null || 
            passives.Count == 0 || 
            passiveManager == null ||
            currencyWallet == null ||
            shopConfig == null) return false;
        
        int totalSellPrice = 0;

        foreach (PassiveBase passive in passives)
        {
            if (passive == null) return false;
            if (!passiveManager.OwnedPassives.Contains(passive)) return false;
            
            int sellPrice = GetSellPrice(passive);
            
            if (sellPrice <= 0) return false;
            
            totalSellPrice += sellPrice;
        }

        foreach (PassiveBase passive in passives)
        {
            if (!passiveManager.RemovePassive(passive))
                return false;
        }
        
        currencyWallet.Add(totalSellPrice);
        return true;
    }
    

    #endregion
    
    #region LevelUp
    public int GetLevelUpPrice(PassiveBase passive)
    {
        if (passive == null || shopConfig == null || passive.Level >= 3)
        {
            return 0;
        }

        foreach (var config in shopConfig.LevelConfigs)
        {
            if (config == null) continue;
            
            if (config.Level == passive.Level)
                return config.LevelUpPrice;
        }

        return 0;
    }


    public bool TryLevelUpPassive(PassiveBase passive)
    {
        if (passive == null || passiveManager == null || currencyWallet == null || shopConfig == null) return false;
        
        if (!passiveManager.OwnedPassives.Contains(passive)) return false;
        if (passive.Level >= 3) return false;
        
        int cost = GetLevelUpPrice(passive);

        if (cost <= 0) return false;

        if (!currencyWallet.TrySpend(cost)) return false;

        bool wasEquippedPassive = passiveManager.EquippedPassives.Contains(passive);
        
        if (wasEquippedPassive) passiveManager.UnequipPassive(passive);
        
        int previousLevel = passive.Level;
        
        passive.LevelUp();

        if (previousLevel == passive.Level)
        {
            currencyWallet.Add(cost);
            
            if (wasEquippedPassive) passiveManager.EquipPassive(passive);

            return false;
        }
        
        if (wasEquippedPassive) passiveManager.EquipPassive(passive);
        
        return true;

    }

    public bool CanAffordLevelUp(PassiveBase selectedPassive)
    {
        if (selectedPassive == null || currencyWallet == null) return false;
        
        int cost = GetLevelUpPrice(selectedPassive);

        return currencyWallet.CurrentCoins >= cost && cost > 0;
    }
    #endregion
   
    #region Heal

    public void BeginShopVisit()
    {
        _currentOffers.Clear();
        hasHealedThisVisit = false;
    }

    public void EndShopVisit()
    {
        _currentOffers.Clear();
        hasHealedThisVisit = false;
    }

    private bool CanHealPlayer()
    {
        if (playerHealth == null || currencyWallet == null || shopConfig == null) return false;
        if (hasHealedThisVisit) return false;

        float currentHp = playerHealth.GetCurrentHp();
        float maxHp = playerHealth.GetMaxHp();
        
        if (maxHp <= 0f) return false;
        
        if (currentHp >= maxHp) return false;
        
        int healCost = shopConfig.HealCost;
        
        if (healCost <= 0) return false;
        
        return currencyWallet.CurrentCoins >= healCost;
    }
    public bool TryHealPlayer()
    {
        if (!CanHealPlayer()) return false;
        
        int healCost = shopConfig.HealCost;
        float healRatio = shopConfig.HealRatio;
        
        if (healRatio <= 0f) return false;

        if (!currencyWallet.TrySpend(healCost)) return false;

        float maxHp = playerHealth.GetMaxHp();
        float healAmount = maxHp * healRatio;
        
        playerHealth.HealHp(healAmount);
        hasHealedThisVisit = true;
        
        return true;
    }

    #endregion
}


