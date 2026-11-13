using System.Collections.Generic;
using Chahn._00.Scripts.Shop;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyUI : MonoBehaviour
{
    [SerializeField] private ShopController shopController;
    [SerializeField] private Button resetOfferButton;
    [SerializeField] private ShopOfferSlotUI[] offerSlots;
    
    
    private void Awake()
    {
        shopController.OnOffersGenerated += DisplayOfferSlots;
        resetOfferButton.onClick.AddListener(HandleRefreshButton);
        
        foreach (var slot in offerSlots)
        {
            slot.OnPurchaseRequested += HandlePurchaseRequested; //HandlePurchaseRequested 실행
        }
    }

    private void OnEnable()
    {
        if (shopController.HasOffers)
        {
            DisplayOfferSlots(shopController.CurrentOffers);
            return;
        }
        
        shopController.EnsureOffersGenerated();
    }
    
    private void OnDestroy()
    {
        shopController.OnOffersGenerated -= DisplayOfferSlots;
        resetOfferButton.onClick.RemoveListener(HandleRefreshButton);
            
        foreach (var slot in offerSlots)
        {
            slot.OnPurchaseRequested -= HandlePurchaseRequested; //HandlePurchaseRequested 실행
        }
    }
    
    private void HandlePurchaseRequested(ShopOffer offer) //Invoke 되면 어떤 슬롯에서 정보를 받았는지 받고 어떤 패시브인지도
    {
        bool succeeded = shopController.TryPurchaseOffer(offer);

        if (!succeeded) return;

        foreach (ShopOfferSlotUI slot in offerSlots) //그 슬롯한테 이제 샀으니까 버튼 끄라고 함
        {
            slot.SetPurchaseAvailable(true);
        }
    }

    private void DisplayOfferSlots(IReadOnlyList<ShopOffer> offers)
    {
        for (int i = 0; i < offerSlots.Length; i++)
        {
            offerSlots[i].Bind(offers[i]);
        }
    }


    private void HandleRefreshButton()
    {
        shopController.TryRefreshOffers();
    }
}
