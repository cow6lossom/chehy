using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Chahn._00.Scripts.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private ShopController shopController;
        [SerializeField] private ShopInteractable shopInteractable;
        [SerializeField] private ShopOfferSlotUI[] offerSlots;
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private Button resetOfferButton;
        [SerializeField] private CurrencyWallet currencyWallet;
        [SerializeField] private TextMeshProUGUI coinText;
        private bool IsOpen => shopPanel.activeSelf;

        private void Awake()
        {
            shopInteractable.OnInteracted += Toggle;
            shopController.OnOffersGenerated += DisplayOfferSlots;
            resetOfferButton.onClick.AddListener(HandleRefreshButton);
            currencyWallet.OnCoinChanged += UpdateCoinText;

            foreach (var slot in offerSlots)
            {
                slot.OnPurchaseRequested += HandlePurchaseRequested; //HandlePurchaseRequested 실행
            }
        }

        private void Start()
        {
            UpdateCoinText(currencyWallet.CurrentCoins);
        }

        private void OnDestroy()
        {
            shopInteractable.OnInteracted -= Toggle;
            shopController.OnOffersGenerated -= DisplayOfferSlots;
            resetOfferButton.onClick.RemoveListener(HandleRefreshButton);
            currencyWallet.OnCoinChanged -= UpdateCoinText;
            
            foreach (var slot in offerSlots)
            {
                slot.OnPurchaseRequested -= HandlePurchaseRequested; //HandlePurchaseRequested 실행
            }
        }
        

        private void Toggle()
        {
            if (IsOpen)
            {
                shopPanel.SetActive(false);
                return;
            }

            shopController.EnsureOffersGenerated();
            shopPanel.SetActive(true);
        }

        private void HandlePurchaseRequested(ShopOffer offer) //Invoke 되면 어떤 슬롯에서 정보를 받았는지 받고 어떤 패시브인지도
        {
            bool succeeded = shopController.TryPurchaseOffer(offer);

            if (!succeeded) return;

            foreach (ShopOfferSlotUI slot in offerSlots) //그 슬롯한테 이제 샀으니까 버튼 끄라고 함
            {
                slot.SetPurchaseAvailable(false);
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

        private void Update()
        {
            if (Keyboard.current == null)
                return;

            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                Toggle();
            }
        }

        private void UpdateCoinText(int coins)
        {
            coinText.SetText(coins.ToString());
        }
    }
}