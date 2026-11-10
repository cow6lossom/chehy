
using System;
using UnityEngine;

public class BossPassiveSlotUI : MonoBehaviour
{
    [SerializeField] private PassiveManager _passiveManager;
    [SerializeField] private PassiveSlotUI[] _ownedSlots;
    [SerializeField] private PassiveSlotUI _equippedSlot;

    public event Action<PassiveBase> OnSlotSelected;

    public void Refresh()
    {
        for (int i = 0; i < _ownedSlots.Length; i++)
        {
            PassiveBase passive = null;

            if (i < _passiveManager.OwnedBossPassives.Count)
                passive = _passiveManager.OwnedBossPassives[i];

            _ownedSlots[i].SetPassive(passive, OnBossPassiveClicked);
        }

        _equippedSlot.SetPassive(_passiveManager.EquippedBossPassive, null);
    }

    private void OnBossPassiveClicked(PassiveBase passive)
    {
        OnSlotSelected?.Invoke(passive);
    }
}
