using System.Collections.Generic;
using Chahn._00.Scripts.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSellUI : MonoBehaviour
{
   [SerializeField] private PassiveManager passiveManager;
   [SerializeField] private ShopController shopController;
   [SerializeField] private GameObject passiveSelectionPanel;
   [SerializeField] private PassiveSlotUI ownedSlotPrefab;
   [SerializeField] private Transform ownedSlotContainer;
   [SerializeField] private PassiveSlotUI selectedSlotPrefab;
   [SerializeField] private Transform selectedSlotContainer;
   [SerializeField] private Button addPassiveButton;
   [SerializeField] private Button cancelButton;
   [SerializeField] private Button confirmSellButton;
   [SerializeField] private Button cancelSellButton;
   [SerializeField] private TextMeshProUGUI totalSellPriceText;

   private readonly List<PassiveBase> selectedPassives = new();

   private void Awake()
   {
       addPassiveButton.onClick.AddListener(ActivatePassiveSelectionPanel);
   }

   private void ActivatePassiveSelectionPanel()
   {
       passiveSelectionPanel.SetActive(true);
   }
}
