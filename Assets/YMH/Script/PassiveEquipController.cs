using UnityEngine;

public class PassiveEquipController : MonoBehaviour
{
    [SerializeField] private PassiveManager _passiveManager;
    [SerializeField] private PassiveInventorySlotUI _normalInventoryUI;
    [SerializeField] private PassiveEquippedSlotUI _normalEquippedUI;
    [SerializeField] private PassiveInfoUI _infoUI;

    private PassiveBase _selectedPassive;

    private void Awake()
    {
        _normalInventoryUI.OnSlotSelected += OnNormalPassiveSelected;
        _normalEquippedUI.OnSlotSelected += OnEquippedSlotSelected;
    }

    private void Start()
    {
        RefreshAll();
    }

    private void OnDestroy()
    {
        _normalInventoryUI.OnSlotSelected -= OnNormalPassiveSelected;
        _normalEquippedUI.OnSlotSelected -= OnEquippedSlotSelected;
    }

    private void OnNormalPassiveSelected(PassiveBase passive)
    {
        if (passive == null) return;

        _selectedPassive = passive;
        _infoUI.ShowInfo(passive);
    }

    private void OnEquippedSlotSelected(int index)
    {
        if (_selectedPassive == null)
        {
            if (index < _passiveManager.EquippedPassives.Count)
                _infoUI.ShowInfo(_passiveManager.EquippedPassives[index]);

            return;
        }

        if (index < _passiveManager.EquippedPassives.Count)
        {
            PassiveBase current = _passiveManager.EquippedPassives[index];

            if (!_passiveManager.ReplaceEquippedPassive(current, _selectedPassive))
                return;
        }
        else
        {
            _passiveManager.EquipPassive(_selectedPassive);
        }

        _selectedPassive = null;
        _infoUI.HideInfo();
        RefreshAll();
    }

    public void RefreshAll()
    {
        _normalInventoryUI.Refresh();
        _normalEquippedUI.Refresh();
    }

    public void ClearSelection()
    {
        _selectedPassive = null;
        _infoUI.HideInfo();
    }
}
