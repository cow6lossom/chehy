
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private bool _isCombat;
    [SerializeField] private PassiveEquipController _equipController;

    private bool _isOpen;

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.rKey.wasPressedThisFrame)
            ToggleInventory();
    }

    public void ToggleInventory()
    {
        if (_isCombat && !_isOpen)
            return;

        _isOpen = !_isOpen;
        _inventoryPanel.SetActive(_isOpen);

        if (_isOpen)
            _equipController.RefreshAll();
    }

    public void SetCombatState(bool isCombat)
    {
        _isCombat = isCombat;

        if (_isCombat && _isOpen)
            CloseInventory();
    }

    public void CloseInventory()
    {
        _isOpen = false;
        _inventoryPanel.SetActive(false);
    }
}
