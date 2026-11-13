using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopLevelUpUI : MonoBehaviour
{
    [Header("Manager & Controller")]
    [SerializeField] private PassiveManager passiveManager;
    [SerializeField] private ShopController shopController;
    
    [Header("Buttons")]
    [SerializeField] private Button addPassiveButton;
    
    [Header("SelectionPanel")]
    [SerializeField] private GameObject passiveSelectionPanel; 
    
    [Header("Slot")]
    [SerializeField] private PassiveSlotUI selectedSlotPrefab;
    [SerializeField] private PassiveSlotUI ownedSlotPrefab;
    [SerializeField] private Transform ownedSlotContainer; //스크롤
    [SerializeField] private Transform selectedSlotContainer; //addbutton있는곳


    private PassiveBase selectedPassive; //레벨업 하려고 선택한 패시브
    private readonly List<PassiveSlotUI> ownedSlotInstances = new(); //코드로 생성한 보유 패시브 슬롯
    private PassiveSlotUI selectedSlotInstance;
    
    private void Awake()
    {
        addPassiveButton.onClick.AddListener(ToggleSelectionPanel);
    }

    private void OnDestroy()
    {
        addPassiveButton.onClick.RemoveListener(ToggleSelectionPanel);
    }
    
    private void ToggleSelectionPanel()
    {
        bool isPanelOpened = passiveSelectionPanel.activeSelf;
        passiveSelectionPanel.SetActive(!isPanelOpened);
       
        if (!isPanelOpened) 
            RefreshOwnedSlots();
    }

    private void OnEnable()
    {
        selectedPassive = null;
        passiveSelectionPanel.SetActive(false);
        
        RefreshSelectedSlot();
    }

    private void RefreshOwnedSlots()
    {
        ClearSlotInstances(ownedSlotInstances);

        IReadOnlyList<PassiveBase> ownedPassives = passiveManager.OwnedPassives;
        bool hasSelectedPassive = selectedPassive != null;

        foreach (PassiveBase passiveBase in ownedPassives)
        {
            PassiveSlotUI slot = Instantiate(ownedSlotPrefab, ownedSlotContainer);

            slot.SetPassive(passiveBase, HandleOwnedPassiveSelected);

            ownedSlotInstances.Add(slot);
            
            bool isLevelFull = passiveBase.Level >= 3;

            if (hasSelectedPassive || isLevelFull)
            {
                if (slot.TryGetComponent(out Button selectButton))
                {
                    selectButton.interactable = false;
                    Debug.Log("버튼 꺼짐");
                }

            }
        }
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

    
    private void HandleOwnedPassiveSelected(PassiveBase passive)
    {
        if (passive == null) return;
        if (selectedPassive != null) return;

        selectedPassive = passive;
        RefreshSelectedSlot();
       
        ToggleSelectionPanel();
    }
    
    private void RefreshSelectedSlot()
    {
        
        if (selectedSlotInstance != null)
        {
            Destroy(selectedSlotInstance.gameObject);
            selectedSlotInstance = null;
        }

        if (selectedPassive == null)
        {
            addPassiveButton.gameObject.SetActive(true);
            return;
        }
        addPassiveButton.gameObject.SetActive(false);
        
        PassiveSlotUI slot = Instantiate(selectedSlotPrefab, selectedSlotContainer);
        
        slot.SetPassive(selectedPassive, HandleSelectedPassiveRemoved);
        
        selectedSlotInstance = slot;
    }
    
    private void HandleSelectedPassiveRemoved(PassiveBase passive)
    {
        if (passive == null)
            return;
       
        selectedPassive = null;
       
        RefreshSelectedSlot();
    }

}
