using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopConfig", menuName = "Shop/ShopConfig")]
public class ShopConfig : ScriptableObject //패시브 상품에 대한 정보
{
    [Header("판매 가능한 12개의 패시브")]
    [SerializeField] private List<PassiveData> passivePool;
    public IReadOnlyList<PassiveData> PassivePool => passivePool; //외부에서 접근할 용

    [Header("상점 기본 설정")] 
    [SerializeField, Min(1)] private int displayCount = 5; //5개의 스킬 랜덤 뽑기

    [SerializeField, Min(0)] private int refreshCost = 5; //새로 고침하기 위한 엽전 수
    
    [Header("레벨별 설정")]
    [SerializeField] private List<ShopLevelConfig> levelConfigs;
    
    public int DisplayCount => displayCount;
    public int RefreshCost => refreshCost;
    public IReadOnlyList<ShopLevelConfig> LevelConfigs => levelConfigs;
}

[Serializable]
public sealed class ShopLevelConfig //레벨에 대한 정보
{
    [SerializeField, Range(1, 4)] private int level;

    [SerializeField, Min(0)] private int purchasePrice;

    [SerializeField, Min(0)] private float weight; //가중치
    
    public int Level => level;
    public int PurchasePrice => purchasePrice;
    public float Weight => weight;
}
