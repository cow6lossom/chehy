using UnityEngine;
using UnityEngine.UI;

public class ShopHealUI : MonoBehaviour
{
    [SerializeField] private GameObject healConfirmPanel;
    [Header("Buttons")] 
    [SerializeField] private Button selectHealButton;
    [SerializeField] private Button confirmHealButton;
    [SerializeField] private Button cancelHealButton;


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
    }

    private void HandleConfirmHeal()
    {
        CloseConfirmPanel();
    }
    private void HandleCancel()
    {
        CloseConfirmPanel();
    }
    
    private void CloseConfirmPanel()
    {
        healConfirmPanel.SetActive(false);
    }

}
