using System;
using UnityEngine;

public class CurrencyWallet : MonoBehaviour //보유하고 있는 엽전과 엽전 추가 spending을 다루고 있음
{
    [SerializeField, Min(0)] private int startingCoins; //게임 시작 시 가지고 있을 엽전
    
    public int CurrentCoins { get; private set; }
    
    public event Action<int> OnCoinChanged; //나중에 엽전 수 변경된다면 그 수를 전달하는 이벤트
    //UI 엽전 이 이벤트 구독한다

    private void Awake()
    {
        CurrentCoins = startingCoins;
    }


    public void Add(int amount)
    {
        if (amount <= 0) return;
        CurrentCoins += amount;
        OnCoinChanged?.Invoke(CurrentCoins);
    }

    public bool CanAfford(int price)
    {
        if (price <= 0)
        {
            Debug.LogWarning("Can't afford negative price");
            return false;
        }
        
        return CurrentCoins >= price;
    }

    public bool TrySpend(int price)
    {
        if (price <= 0)
        {
            Debug.LogWarning("There is no negative price");
            return false;
        }

        if (!CanAfford(price)) return false;
        
        CurrentCoins -= price;
        OnCoinChanged?.Invoke(CurrentCoins);
        return true;
    }
}
