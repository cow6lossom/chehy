using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chahn._00.Scripts.Shop
{
    public sealed class ShopOffer //이번 상점에 제시된 판매 상품 (모음 아닌 indivisual)
    {
        public PassiveData PassiveData { get; private set; }
        public int Level { get; }
        public int Price { get; }

        public string Name => PassiveData.PassiveName;
        public string PassiveDesc => PassiveData.Description;
        public Sprite PassiveIcon => PassiveData.Icon;
        
        public ShopOffer(PassiveData passiveData, int level, int price)
        {
            PassiveData = passiveData;
            Level = level;
            Price = price;
        }
        
        public bool IsPurchased {get; private set;}
        
        public void MarkPurchased()
        {
            IsPurchased = true;
        }

    }
}