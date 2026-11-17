using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopHealUI : MonoBehaviour
{
    [SerializeField] private ShopController shopController;
    [SerializeField] private GameObject healConfirmPanel;
    [Header("Buttons")] 
    [SerializeField] private Button selectHealButton;
    [SerializeField] private Button confirmHealButton;
    [SerializeField] private Button cancelHealButton;

    public event Action OnHealSucceeded;


    private void Awake()
    {
        selectHealButton.onClick.AddListener(ActivateConfirmPanel);
        confirmHealButton.onClick.AddListener(HandleConfirmHeal);
        cancelHealButton.onClick.AddListener(HandleCancel);
    }

    private void OnDestroy()
    {
        selectHealButton.onClick.RemoveListener(ActivateConfirmPanel);
        confirmHealButton.onClick.RemoveListener(HandleConfirmHeal);
        cancelHealButton.onClick.RemoveListener(HandleCancel);
    }


    private void OnEnable()
    {
        CloseConfirmPanel();
    }

    private void ActivateConfirmPanel()
    {
        healConfirmPanel.SetActive(true);
        selectHealButton.interactable = false;
    }

    private void HandleConfirmHeal()
    {
        bool succeeded = shopController.TryHealPlayer();
        
        if (succeeded)
            OnHealSucceeded?.Invoke();
            
        CloseConfirmPanel();
    }
    private void HandleCancel()
    {
        CloseConfirmPanel();
    }
    
    private void CloseConfirmPanel()
    {
        healConfirmPanel.SetActive(false); 
        selectHealButton.interactable = true;
    }

}
