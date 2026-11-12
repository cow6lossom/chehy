using System;
using System.Collections.Generic;
using Chahn._00.Scripts.Shop;
using UnityEngine;
using Random = UnityEngine.Random;


public class ShopController : MonoBehaviour
{
    [SerializeField] ShopConfig shopConfig;
    
    [SerializeField] CurrencyWallet currencyWallet;
    [SerializeField] private PassiveManager passiveManager;
    public bool HasOffers => CurrentOffers.Count > 0; //이미 상점이 한번 뽑혔는지 확인한다 (상점이 처음 뽑혔을때는 리스트가 비어있어야 한다)
    
    private readonly List<ShopOffer> _currentOffers = new();
    public IReadOnlyList<ShopOffer> CurrentOffers => _currentOffers; //접근할때
    public event Action<IReadOnlyList<ShopOffer>> OnOffersGenerated;
    
    public bool HasPurchasedCurrentOffers => _currentOffers.Exists(x => x.IsPurchased);

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

        if (HasPurchasedCurrentOffers) return false;
        
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

    public bool TryGetSellPrice(PassiveBase passive, out int sellPrice)
    {
        sellPrice = 0;

        if (passive == null || passive.Data == null || shopConfig == null)
            return false;

        foreach (ShopLevelConfig config in shopConfig.LevelConfigs)
        {
              
        }

        return false;
    }
}


