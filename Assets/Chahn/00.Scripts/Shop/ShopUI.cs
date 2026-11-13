using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Chahn._00.Scripts.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private ShopInteractable shopInteractable;
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private CurrencyWallet currencyWallet;
        [SerializeField] private TextMeshProUGUI coinText;
        
        [Header("UI Scripts")]
        [SerializeField] private ShopBuyUI shopBuyUI;
        [SerializeField] private ShopSellUI shopSellUI;
        
        [Header("Buttons")]
        [SerializeField] private Button buyPanelButton;
        [SerializeField] private Button sellPanelButton;
        
        
        private bool IsOpen => shopPanel.activeSelf;

        private void Awake()
        {
            shopInteractable.OnInteracted += Toggle;
            currencyWallet.OnCoinChanged += UpdateCoinText;
            
            buyPanelButton.onClick.AddListener(() => SetActiveTargetPanel(shopBuyUI.gameObject));
            sellPanelButton.onClick.AddListener(() => SetActiveTargetPanel(shopSellUI.gameObject));
            
        }

        private void Start()
        {
            UpdateCoinText(currencyWallet.CurrentCoins);
            shopPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            shopInteractable.OnInteracted -= Toggle;
            currencyWallet.OnCoinChanged -= UpdateCoinText;
        }
        

        private void Toggle()
        {
            if (IsOpen)
            {
                shopPanel.SetActive(false);
                return;
            }

            MainShopPanelEnabled();
        }

        private void MainShopPanelEnabled()
        {
            if (shopBuyUI == null || shopSellUI == null) return;
            
            SetActiveTargetPanel(shopBuyUI.gameObject);
            shopPanel.SetActive(true);
        }

        private void SetActiveTargetPanel(GameObject targetPanel)
        {
            shopBuyUI.gameObject.SetActive(shopBuyUI.gameObject == targetPanel);
            shopSellUI.gameObject.SetActive(shopSellUI.gameObject == targetPanel);
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