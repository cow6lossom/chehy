using System.Collections.Generic;
using Chahn._00.Scripts.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSellUI : MonoBehaviour
{

   [Header("Manager & Controller")]
   [SerializeField] private PassiveManager passiveManager;
   [SerializeField] private ShopController shopController;
   
   [Header("SelectionPanel")]
   [SerializeField] private GameObject passiveSelectionPanel; //전체
   
   [Header("Slot")]
   [SerializeField] private PassiveSlotUI selectedSlotPrefab;
   [SerializeField] private PassiveSlotUI ownedSlotPrefab;
   [SerializeField] private Transform ownedSlotContainer; //스크롤
   [SerializeField] private Transform selectedSlotContainer; //addbutton있는곳
   
   [Header("Buttons")]
   [SerializeField] private Button addPassiveButton;
   [SerializeField] private Button cancelSellButton;
   [SerializeField] private Button confirmSellButton;
   
   [Header("Text")]
   [SerializeField] private TextMeshProUGUI totalSellPriceText;
   
   private readonly List<PassiveBase> selectedPassives = new(); //판매하려고 선택한 패시브
   private readonly List<PassiveSlotUI> ownedSlotInstances = new(); //코드로 생성한 보유 패시브 슬롯
   private readonly List<PassiveSlotUI> selectedSlotInstances = new(); //코드로 생성한 선택한 패시브 슬롯
   private int TotalSellPrice { get; set; }
   
   private void Awake()
   {
       addPassiveButton.onClick.AddListener(ToggleSelectionPanel);
       cancelSellButton.onClick.AddListener(ClearSelection);
       confirmSellButton.onClick.AddListener(HandleConfirmSell);
   }

   private void OnEnable()
   {
       ClearSelection();
       passiveSelectionPanel.SetActive(false);
   }

   private void OnDestroy()
   {
       addPassiveButton.onClick.RemoveListener(ToggleSelectionPanel);
       cancelSellButton.onClick.RemoveListener(ClearSelection);
       confirmSellButton.onClick.RemoveListener(HandleConfirmSell);
   }
   

   private void RefreshOwnedSlots()
   {
       ClearSlotInstances(ownedSlotInstances);
       
       IReadOnlyList<PassiveBase> ownedPassives = passiveManager.OwnedPassives;

       foreach (PassiveBase passiveBase in ownedPassives)
       {
           PassiveSlotUI slot = Instantiate(ownedSlotPrefab, ownedSlotContainer);
           
           slot.SetPassive(passiveBase, HandleOwnedPassiveSelected);
           
           ownedSlotInstances.Add(slot);
           
           bool isSelected = selectedPassives.Contains(passiveBase);

           if (isSelected)
           {
               if (slot.TryGetComponent(out Button selectButton))
               {
                   selectButton.interactable = false;
                   Debug.Log("버튼 꺼짐");
               }
                   
           }
       }
   }


   private void RefreshSelectedSlots()
   {
       ClearSlotInstances(selectedSlotInstances);
       
       TotalSellPrice = 0;
       foreach (PassiveBase passive in selectedPassives)
       {
           PassiveSlotUI slot = Instantiate(selectedSlotPrefab, selectedSlotContainer);
           
           slot.SetPassive(passive, HandleSelectedPassiveRemoved);
           
           selectedSlotInstances.Add(slot);
           
           TotalSellPrice += shopController.GetSellPrice(passive);
       }
       
       totalSellPriceText.SetText(TotalSellPrice.ToString());
       addPassiveButton.transform.SetAsLastSibling();
       
       if (selectedPassives.Count <= 0) 
           confirmSellButton.interactable = false;
       else
           confirmSellButton.interactable = true;
   }

   private void HandleSelectedPassiveRemoved(PassiveBase passive)
   {
       if (passive == null)
           return;
       
       selectedPassives.Remove(passive);
       
       RefreshSelectedSlots();
   }

   private void HandleConfirmSell()
   {
       if (selectedPassives.Count <= 0) return;
       
       if (shopController.TrySellPassives(selectedPassives))
       {
           ClearSelection();
       }
   }

   private void ClearSelection()
   {
       selectedPassives.Clear();
       RefreshSelectedSlots();
   }

   private void HandleOwnedPassiveSelected(PassiveBase passive)
   {
       if (passive == null) return;
       if (selectedPassives.Contains(passive)) return;
       
       selectedPassives.Add(passive);
       RefreshSelectedSlots();
       
       ToggleSelectionPanel();
   }

   private void ToggleSelectionPanel()
   {
       bool isPanelOpened = passiveSelectionPanel.activeSelf;
       passiveSelectionPanel.SetActive(!isPanelOpened);
       
       if (!isPanelOpened) 
           RefreshOwnedSlots();
   }
   

   private void ClearSlotInstances(List<PassiveSlotUI> slotInstances)
   {
       foreach (var slot in slotInstances)
       {
           if (slot == null) continue;
           
           Destroy(slot.gameObject);
       }
       
       slotInstances.Clear();
   }
   
}
