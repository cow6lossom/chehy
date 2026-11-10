
using System;
using UnityEngine;

public class PassiveInventorySlotUI : MonoBehaviour
{
    [SerializeField] private PassiveManager _passiveManager;
    [SerializeField] private PassiveSlotUI[] _slots;

    public event Action<PassiveBase> OnSlotSelected;

    public void Refresh()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            PassiveBase passive = null;

            if (i < _passiveManager.OwnedPassives.Count)
                passive = _passiveManager.OwnedPassives[i];

            _slots[i].SetPassive(passive, OnPassiveClicked);
        }
    }

    private void OnPassiveClicked(PassiveBase passive)
    {
        OnSlotSelected?.Invoke(passive);
    }
}
