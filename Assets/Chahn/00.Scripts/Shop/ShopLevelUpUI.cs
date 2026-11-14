using System.Collections.Generic;
using TMPro;
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

    [Header("Upgrade Info")] 
    [SerializeField] private TextMeshProUGUI passiveNameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI currentDescText;
    [SerializeField] private TextMeshProUGUI nextDescText;
    [SerializeField] private TextMeshProUGUI levelUpPriceText;
    [SerializeField] private Button confirmLevelUpButton;


    private PassiveBase selectedPassive; //레벨업 하려고 선택한 패시브
    private readonly List<PassiveSlotUI> ownedSlotInstances = new(); //코드로 생성한 보유 패시브 슬롯
    private PassiveSlotUI selectedSlotInstance;
    
    private void Awake()
    {
        addPassiveButton.onClick.AddListener(ToggleSelectionPanel);
        confirmLevelUpButton.onClick.AddListener(HandleConfirmLevelUp);
    }

    private void OnDestroy()
    {
        addPassiveButton.onClick.RemoveListener(ToggleSelectionPanel);
        confirmLevelUpButton.onClick.RemoveListener(HandleConfirmLevelUp);
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
        RefreshLevelUpInfo();
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
        RefreshLevelUpInfo();
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
       
        ClearSelectedPassive();
    }

    private void HandleConfirmLevelUp()
    {
        if (selectedPassive == null) return;
        
        bool succeeded = shopController.TryLevelUpPassive(selectedPassive);

        if (!succeeded) return;
        
        ClearSelectedPassive();
    }

    private void ClearSelectedPassive()
    {
        selectedPassive = null;
        
        RefreshSelectedSlot();
        RefreshLevelUpInfo();
        
        passiveSelectionPanel.SetActive(false);
    }
    private void RefreshLevelUpInfo()
    {
        if (selectedPassive == null)
        {
            passiveNameText.SetText("");
            levelText.SetText("");
            currentDescText.SetText("");
            nextDescText.SetText("");
            levelUpPriceText.SetText("");
            
            confirmLevelUpButton.interactable = false;
            return;
        }

        int currentLevel = selectedPassive.Level;
        int nextLevel = currentLevel + 1;
        int levelUpPrice = shopController.GetLevelUpPrice(selectedPassive);
        
        passiveNameText.SetText(selectedPassive.Data.PassiveName);
        levelText.SetText($"Lvl.{currentLevel} -> Lvl.{nextLevel}");
        currentDescText.SetText(selectedPassive.Data.GetLevelUpDescription(currentLevel));
        nextDescText.SetText(selectedPassive.Data.GetLevelUpDescription(nextLevel));
        levelUpPriceText.SetText(levelUpPrice.ToString());
        
        confirmLevelUpButton.interactable = currentLevel < 3 && shopController.CanAffordLevelUp(selectedPassive);
    }
}
