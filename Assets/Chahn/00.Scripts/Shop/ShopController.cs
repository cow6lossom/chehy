using System;
using System.Collections.Generic;
using Chahn._00.Scripts.Shop;
using UnityEngine;
using Random = UnityEngine.Random;


public class ShopController : MonoBehaviour
{
    [SerializeField] ShopConfig shopConfig;

    private readonly List<ShopOffer> currentOffers = new();
    public IReadOnlyList<ShopOffer> CurrentOffers => currentOffers; //접근할때
    public event Action<IReadOnlyList<ShopOffer>> OnOffersGenerated;

    public void GenerateOffers()
    {
        currentOffers.Clear();

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
                currentOffers.Clear();
                return;
            }
            
            ShopOffer offer = new ShopOffer(selectedPassive, selectedLevel.Level, selectedLevel.PurchasePrice);
            currentOffers.Add(offer);
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
}
