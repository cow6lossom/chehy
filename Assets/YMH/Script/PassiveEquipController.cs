using System.Linq;
using UnityEngine;

public class PassiveEquipController : MonoBehaviour
{
    [SerializeField] private PassiveManager _passiveManager;
    [SerializeField] private PassiveInventorySlotUI _normalInventoryUI;
    [SerializeField] private PassiveEquippedSlotUI _normalEquippedUI;
    [SerializeField] private PassiveInfoUI _infoUI;

    private PassiveBase _pendingReplacementPassive;

    private void Awake()
    {
        _normalInventoryUI.OnSlotSelected += OnNormalPassiveSelected;
        _normalEquippedUI.OnSlotSelected += OnEquippedSlotSelected;

        _normalInventoryUI.OnSlotHovered += OnPassiveHovered;
        _normalEquippedUI.OnSlotHovered += OnPassiveHovered;

        _normalInventoryUI.OnSlotExited += OnPassiveExited;
        _normalEquippedUI.OnSlotExited += OnPassiveExited;
    }

    private void Start()
    {
        RefreshAll();
    }

    private void OnDestroy()
    {
        _normalInventoryUI.OnSlotSelected -= OnNormalPassiveSelected;
        _normalEquippedUI.OnSlotSelected -= OnEquippedSlotSelected;

        _normalInventoryUI.OnSlotHovered -= OnPassiveHovered;
        _normalEquippedUI.OnSlotHovered -= OnPassiveHovered;

        _normalInventoryUI.OnSlotExited -= OnPassiveExited;
        _normalEquippedUI.OnSlotExited -= OnPassiveExited;
    }

    private void OnNormalPassiveSelected(PassiveBase passive, RectTransform slotRect)
    {
        if (passive == null) return;
        _infoUI.ShowInfo(passive, slotRect);
    }

    private void OnEquippedSlotSelected(int index, RectTransform slotRect)
    {
        if (_pendingReplacementPassive != null)
        {
            if (index < 0 || index >= _passiveManager.EquippedPassives.Count) return;

            PassiveBase current = _passiveManager.EquippedPassives[index];
            PassiveBase replacement = _pendingReplacementPassive;

            if (!_passiveManager.ReplaceEquippedPassive(current, replacement)) return;

            _pendingReplacementPassive = null;
            _infoUI.SetReplacing(false);

            RefreshAll();
            _infoUI.ShowInfo(replacement, slotRect);
            return;
        }

        if (index < _passiveManager.EquippedPassives.Count)
            _infoUI.ShowInfo(_passiveManager.EquippedPassives[index], slotRect);
    }

    private void OnPassiveHovered(PassiveBase passive, RectTransform slotRect)
    {
        if (passive == null || _pendingReplacementPassive != null) return;
        _infoUI.OnSlotEnter(passive, slotRect);
    }

    private void OnPassiveExited()
    {
        _infoUI.OnSlotExit();
    }

    public void StartReplace(PassiveBase passive)
    {
        if (passive == null) return;
        if (_passiveManager.EquippedPassives.Contains(passive)) return;

        _pendingReplacementPassive = passive;
        _infoUI.SetReplacing(true);
    }

    public void RefreshAll()
    {
        _normalInventoryUI.Refresh();
        _normalEquippedUI.Refresh();
    }

    public void CancelReplace()
    {
        _pendingReplacementPassive = null;
        _infoUI.SetReplacing(false);
    }
}