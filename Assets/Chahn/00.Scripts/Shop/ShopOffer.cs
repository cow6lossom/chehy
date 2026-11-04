using System.Collections.Generic;
using UnityEngine;

namespace Chahn._00.Scripts.Shop
{
    public sealed class ShopOffer //이번 상점에 제시된 판매 상품 (모음 아닌 indivisual)
    {
        public PassiveData PassiveData;
        public int Level { get; }
        public int Price { get; }
        
        public ShopOffer(PassiveData passiveData, int level, int price)
        {
            PassiveData = passiveData;
            Level = level;
            Price = price;
        }
        

    }
}