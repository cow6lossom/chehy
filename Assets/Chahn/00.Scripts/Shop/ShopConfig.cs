using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopConfig", menuName = "Shop/ShopConfig")]
public class ShopConfig : ScriptableObject //상점 전체에서 사용하는 고정 설정값 관리
{
    [Header("판매 가능한 12개의 패시브")]
    [SerializeField] private List<PassiveData> passivePool;
    public IReadOnlyList<PassiveData> PassivePool => passivePool; //외부에서 접근할 용

    [Header("상점 기본 설정")] 
    [SerializeField, Min(1)] private int displayCount = 5; //5개의 스킬 랜덤 뽑기

    [SerializeField, Min(0)] private int refreshCost = 5; //새로 고침하기 위한 엽전 수
    
    [Header("레벨별 설정")]
    [SerializeField] private List<ShopLevelConfig> levelConfigs;

    [Header("체력회복 설정")] 
    [SerializeField] private int healCost = 50;
    [SerializeField] private float healRatio = 0.3f;
    
    public int HealCost => healCost;
    public float HealRatio => healRatio;
    public int DisplayCount => displayCount;
    public int RefreshCost => refreshCost;
    public IReadOnlyList<ShopLevelConfig> LevelConfigs => levelConfigs;
}

[Serializable]
public sealed class ShopLevelConfig //레벨에 대한 정보
{
    [SerializeField, Range(1, 3)] private int level;

    [SerializeField, Min(0)] private int purchasePrice;

    [SerializeField, Min(0)] private int sellPrice;

    [SerializeField, Min(0)] private float weight; //가중치
    
    [SerializeField, Min(0)] private int levelUpPrice;
    
    public int Level => level;
    public int PurchasePrice => purchasePrice;
    public int SellPrice => sellPrice;
    public float Weight => weight;
    public int LevelUpPrice => levelUpPrice;
    
}
