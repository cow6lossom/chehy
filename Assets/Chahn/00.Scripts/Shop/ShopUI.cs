using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chahn._00.Scripts.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private ShopInteractable shopInteractable;
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private TextMeshProUGUI coinText;
        
        [Header("UI Scripts")]
        [SerializeField] private ShopBuyUI shopBuyUI;
        [SerializeField] private ShopSellUI shopSellUI;
        [SerializeField] private ShopHealUI shopHealUI;
        [SerializeField] private ShopLevelUpUI shopLevelUpUI;
        
        [Header("Buttons")]
        [SerializeField] private Button buyPanelButton;
        [SerializeField] private Button sellPanelButton;
        [SerializeField] private Button levelUpPanelButton;
        
        private CurrencyWallet currencyWallet => GameSession.Instance.CurrencyWallet;
        
        private bool IsOpen => shopPanel.activeSelf;

        private void Awake()
        {
            shopInteractable.OnInteracted += Toggle;
            currencyWallet.OnCoinChanged += UpdateCoinText;
            
            buyPanelButton.onClick.AddListener(OpenBuyPanel);
            sellPanelButton.onClick.AddListener(OpenSellPanel);
            levelUpPanelButton.onClick.AddListener(OpenLevelUpPanel);
            
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

            buyPanelButton.onClick.RemoveListener(OpenBuyPanel);
            sellPanelButton.onClick.RemoveListener(OpenSellPanel);
            levelUpPanelButton.onClick.RemoveListener(OpenLevelUpPanel);
        }
        

        public void Toggle()
        {
            if (IsOpen)
            {
                Close();
                return;
            }
            
            MainShopPanelEnabled();
        }

        private void MainShopPanelEnabled()
        {
            if (shopHealUI == null || shopBuyUI == null || shopSellUI == null)
            {
                Debug.LogWarning("check inspector");
                return;
            }
            
            SetActiveTargetPanel(shopBuyUI.gameObject);
            
            shopHealUI.gameObject.SetActive(true);
            shopPanel.SetActive(true);
        }

        public void Close()
        {
            shopPanel.SetActive(false);
        }

        private void SetActiveTargetPanel(GameObject targetPanel)
        {
            shopBuyUI.gameObject.SetActive(shopBuyUI.gameObject == targetPanel);
            shopSellUI.gameObject.SetActive(shopSellUI.gameObject == targetPanel);
            shopLevelUpUI.gameObject.SetActive(shopLevelUpUI.gameObject == targetPanel);
        }

        private void OpenBuyPanel() => SetActiveTargetPanel(shopBuyUI.gameObject);
        private void OpenSellPanel() => SetActiveTargetPanel(shopSellUI.gameObject);
        private void OpenLevelUpPanel() => SetActiveTargetPanel(shopLevelUpUI.gameObject);

        private void UpdateCoinText(int coins)
        {
            coinText.SetText(coins.ToString());
        }
    }
}
