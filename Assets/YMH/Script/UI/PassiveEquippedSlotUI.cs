
using System;
using UnityEngine;

public class PassiveEquippedSlotUI : MonoBehaviour
{
    [SerializeField] private PassiveManager _passiveManager;
    [SerializeField] private PassiveSlotUI[] _slots;

    public event Action<int> OnSlotSelected;

    public void Refresh()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            int index = i;
            PassiveBase passive = null;

            if (i < _passiveManager.EquippedPassives.Count)
                passive = _passiveManager.EquippedPassives[i];

            _slots[i].SetPassive(passive, _ => OnSlotSelected?.Invoke(index));
        }
    }
}
