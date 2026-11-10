using UnityEngine;
using System.Linq;

public class PassiveEquipController : MonoBehaviour
{
    [SerializeField] private PassiveManager _passiveManager;
    [SerializeField] private PassiveInventorySlotUI _normalInventoryUI;
    [SerializeField] private PassiveEquippedSlotUI _normalEquippedUI;
    [SerializeField] private BossPassiveSlotUI _bossInventoryUI;
    [SerializeField] private PassiveInfoUI _infoUI;

    private void Awake()
    {
        _normalInventoryUI.OnSlotSelected += OnNormalPassiveSelected;
        _bossInventoryUI.OnSlotSelected += OnBossPassiveSelected;
    }

    private void Start()
    {
        RefreshAll();
    }

    private void OnDestroy()
    {
        _normalInventoryUI.OnSlotSelected -= OnNormalPassiveSelected;
        _bossInventoryUI.OnSlotSelected -= OnBossPassiveSelected;
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

    private void OnBossPassiveSelected(PassiveBase passive)
    {
        if (_passiveManager.EquippedBossPassive == passive)
            _passiveManager.UnequipBossPassive();
        else
            _passiveManager.EquipBossPassive(passive);

        _infoUI.ShowInfo(passive);
        RefreshAll();
    }

    public void RefreshAll()
    {
        _normalInventoryUI.Refresh();
        _normalEquippedUI.Refresh();
        _bossInventoryUI.Refresh();
    }
}