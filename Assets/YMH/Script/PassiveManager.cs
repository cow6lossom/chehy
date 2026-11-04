using System.Collections.Generic;
using UnityEngine;

public class PassiveManager : MonoBehaviour //소유 관리
{
    private List<PassiveBase> _ownedPassives = new List<PassiveBase>();
    private List<PassiveBase> _equippedPassives = new List<PassiveBase>();

    private const int MaxOwnedCount = 12;
    private const int MaxEquippedCount = 3;

    public void AddPassive(PassiveBase passive)
    {
        if (_ownedPassives.Count >= MaxOwnedCount)
            return;

        _ownedPassives.Add(passive);
    }

    public void EquipPassive(PassiveBase passive)
    {
        if (_equippedPassives.Count >= MaxEquippedCount)
            return;

        if (!_ownedPassives.Contains(passive))
            return;

        _equippedPassives.Add(passive);
        passive.Apply();
    }

    public void UnequipPassive(PassiveBase passive)
    {
        if (!_equippedPassives.Contains(passive))
            return;

        passive.Remove();
        _equippedPassives.Remove(passive);
    }
}