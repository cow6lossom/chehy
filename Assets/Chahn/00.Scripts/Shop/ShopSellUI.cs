using System.Collections.Generic;
using Chahn._00.Scripts.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSellUI : MonoBehaviour
{

   [Header("Managers")]
   [SerializeField] private PassiveManager passiveManager;
   [SerializeField] private ShopController shopController;
   
   [Header("SelectionPanel")]
   [SerializeField] private GameObject passiveSelectionPanel; //전체
   [SerializeField] private Button closeSelectionPanelButton;
   
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

   private void Awake()
   {
       addPassiveButton.onClick.AddListener(OpenSelectionPanel);
       closeSelectionPanelButton.onClick.AddListener(CloseSelectionPanel);
       cancelSellButton.onClick.AddListener(ClearSelection);
   }

   private void OnEnable()
   {
       ClearSelection();
       CloseSelectionPanel();
   }

   private void OnDestroy()
   {
       addPassiveButton.onClick.RemoveListener(OpenSelectionPanel);
       closeSelectionPanelButton.onClick.RemoveListener(CloseSelectionPanel);
       cancelSellButton.onClick.RemoveListener(ClearSelection);
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

       foreach (PassiveBase passive in selectedPassives)
       {
           PassiveSlotUI slot = Instantiate(selectedSlotPrefab, selectedSlotContainer);
           
           slot.SetPassive(passive, HandleSelectedPassiveRemoved);
           
           selectedSlotInstances.Add(slot);
       }
       
       addPassiveButton.transform.SetAsFirstSibling();
   }

   private void HandleSelectedPassiveRemoved(PassiveBase passive)
   {
       if (passive == null)
           return;
       
       selectedPassives.Remove(passive);
       RefreshSelectedSlots();
   }

   private void ClearSelection()
   {
       selectedPassives.Clear();
       ClearSlotInstances(selectedSlotInstances);
       
       totalSellPriceText.text = "0";
       addPassiveButton.transform.SetAsFirstSibling();
   }

   private void HandleOwnedPassiveSelected(PassiveBase passive)
   {
       if (passive == null) return;
       if (selectedPassives.Contains(passive)) return;
       
       selectedPassives.Add(passive);
       RefreshSelectedSlots();
       CloseSelectionPanel();
   }


   private void OpenSelectionPanel()
   {
       RefreshOwnedSlots();
       passiveSelectionPanel.SetActive(true);
   }

   private void CloseSelectionPanel()
   {
       passiveSelectionPanel.SetActive(false);
       Debug.Log("close selection panel");
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
