using System;
using UnityEngine;
public class PassiveEquippedSlotUI : MonoBehaviour
{
    [SerializeField] private PassiveManager _passiveManager;
    [SerializeField] private PassiveSlotUI[] _slots;

    public event Action<int, RectTransform> OnSlotSelected;
    public event Action<PassiveBase, RectTransform> OnSlotHovered;
    public event Action OnSlotExited;

    private void Awake()
    {
        for (int i = 0; i < _slots.Length; i++)
            _slots[i].SetHover(OnPassiveHovered, OnPassiveExited);
    }

    public void Refresh()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            int index = i;
            PassiveBase passive = null;

            if (i < _passiveManager.EquippedPassives.Count)
                passive = _passiveManager.EquippedPassives[i];

            _slots[i].SetPassive(passive, (selected, slotRect) => OnSlotSelected?.Invoke(index, slotRect), passive != null);
            _slots[i].SetHover(OnPassiveHovered, OnPassiveExited);
        }
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