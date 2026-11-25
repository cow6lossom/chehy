using System;
using System.Linq;
using UnityEngine;

public class PassiveInventorySlotUI : MonoBehaviour
{
    [SerializeField] private PassiveManager _passiveManager;
    [SerializeField] private PassiveSlotUI _slotPrefab;
    [SerializeField] private Transform _slotParent;

    private PassiveSlotUI[] _slots = new PassiveSlotUI[8];

    public event Action<PassiveBase, RectTransform> OnSlotSelected;
    public event Action<PassiveBase, RectTransform> OnSlotHovered;
    public event Action OnSlotExited;

    private void Awake()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i] = Instantiate(_slotPrefab, _slotParent);
            _slots[i].SetHover(OnPassiveHovered, OnPassiveExited);
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            PassiveBase passive = null;

            if (i < _passiveManager.OwnedPassives.Count)
                passive = _passiveManager.OwnedPassives[i];

            bool isEquipped = passive != null && _passiveManager.EquippedPassives.Contains(passive);

            _slots[i].SetPassive(passive, OnPassiveClicked, isEquipped);
            _slots[i].SetHover(OnPassiveHovered, OnPassiveExited);
        }
    }

    private void OnPassiveClicked(PassiveBase passive, RectTransform slotRect)
    {
        OnSlotSelected?.Invoke(passive, slotRect);
    }

    private void OnPassiveHovered(PassiveBase passive, RectTransform slotRect)
    {
        OnSlotHovered?.Invoke(passive, slotRect);
    }

    private void OnPassiveExited()
    {
        OnSlotExited?.Invoke();
    }
}