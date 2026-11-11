using UnityEngine;
using System.Linq;

public class PassiveEquipController : MonoBehaviour
{
    [SerializeField] private PassiveManager _passiveManager;
    [SerializeField] private PassiveInventorySlotUI _normalInventoryUI;
    [SerializeField] private PassiveEquippedSlotUI _normalEquippedUI;
    [SerializeField] private PassiveInfoUI _infoUI;

    private void Awake()
    {
        _normalInventoryUI.OnSlotSelected += OnNormalPassiveSelected;
    }

    private void Start()
    {
        RefreshAll();
    }

    private void OnDestroy()
    {
        _normalInventoryUI.OnSlotSelected -= OnNormalPassiveSelected;
    }

    private void OnNormalPassiveSelected(PassiveBase passive)
    {
        if (_passiveManager.EquippedPassives.Contains(passive))
            _passiveManager.UnequipPassive(passive);
        else
            _passiveManager.EquipPassive(passive);

        _infoUI.ShowInfo(passive);
        RefreshAll();
    }

    public void RefreshAll()
    {
        _normalInventoryUI.Refresh();
        _normalEquippedUI.Refresh();
    }
}