using System.Collections.Generic;
using UnityEngine;

namespace Chahn._00.Scripts.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private ShopController shopController;
        [SerializeField] private ShopInteractable shopInteractable;
        [SerializeField] private ShopOfferSlotUI[] offerSlots;
        [SerializeField] private GameObject shopPanel;
        private bool IsOpen => shopPanel.activeSelf;

        private void Awake()
        {
            shopInteractable.OnInteracted += Toggle;
            shopController.OnOffersGenerated += DisplayOfferSlots;
        }

        private void OnDestroy()
        {
            shopInteractable.OnInteracted -= Toggle;
            shopController.OnOffersGenerated -= DisplayOfferSlots;
        }

        private void Toggle()
        {
            if (IsOpen)
            {
                shopPanel.SetActive(false);
                return;
            }

            if (shopController.CurrentOffers.Count == 0)
                shopController.GenerateOffers();
            
            shopPanel.SetActive(true);
        }

        private void DisplayOfferSlots(IReadOnlyList<ShopOffer> offers)
        {
            for (int i = 0; i < offerSlots.Length; i++)
            {
                offerSlots[i].Bind(offers[i]);
            }
        }
    }
}