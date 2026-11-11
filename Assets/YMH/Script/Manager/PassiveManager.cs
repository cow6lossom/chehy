using System.Collections.Generic;
using UnityEngine;

public class PassiveManager : MonoBehaviour
{
    private List<PassiveBase> _ownedPassives = new List<PassiveBase>();
    private List<PassiveBase> _equippedPassives = new List<PassiveBase>();
    private List<PassiveBase> _ownedBossPassives = new List<PassiveBase>();
    private PassiveBase _equippedBossPassive;

    public IReadOnlyList<PassiveBase> OwnedBossPassives => _ownedBossPassives;
    public PassiveBase EquippedBossPassive => _equippedBossPassive;
    private const int MaxOwnedCount = 8;
    private const int MaxEquippedCount = 3;



    public IReadOnlyList<PassiveBase> OwnedPassives => _ownedPassives;
    public IReadOnlyList<PassiveBase> EquippedPassives => _equippedPassives;
    public List<PassiveBase> GetAvailablePassives(List<PassiveBase> candidates)
    {
        List<PassiveBase> available = new List<PassiveBase>();

        foreach (PassiveBase passive in candidates)
        {
            if (passive == null || passive.Data == null || passive.Data.Grade != PassiveGrade.Normal)
                continue;

            PassiveBase owned = _ownedPassives.Find(p => p.Data.PassiveName == passive.Data.PassiveName);

            if (owned == null || owned.Level < 3)
                available.Add(passive);
        }

        return available;
    }
    public bool AddPassive(PassiveBase passive)
    {
        if (passive == null || passive.Data == null || passive.Data.Grade != PassiveGrade.Normal)
            return false;

        PassiveBase existing = _ownedPassives.Find(p => p.Data.PassiveName == passive.Data.PassiveName);

        if (existing != null)
        {
            if (existing.Level >= 3)
                return false;

            if (passive.Level == existing.Level)
                existing.LevelUp();

            return true;
        }

        if (_ownedPassives.Count >= MaxOwnedCount)
            return false;

        _ownedPassives.Add(passive);
        return true;
    }
    public bool RemovePassive(PassiveBase passive)
    {
        if (passive == null || !_ownedPassives.Contains(passive))
            return false;

        if (_equippedPassives.Contains(passive))
            UnequipPassive(passive);

        _ownedPassives.Remove(passive);
        return true;
    }

    public void EquipPassive(PassiveBase passive)
    {
        if (_equippedPassives.Count >= MaxEquippedCount)
            return;

        if (!_ownedPassives.Contains(passive) || _equippedPassives.Contains(passive))
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
    public bool AddBossPassive(PassiveBase passive)
    {
        if (passive == null || passive.Data == null || passive.Data.Grade != PassiveGrade.Boss)
            return false;

        if (_ownedBossPassives.Exists(p => p.Data.PassiveName == passive.Data.PassiveName))
            return false;

        _ownedBossPassives.Add(passive);
        return true;
    }

    public void EquipBossPassive(PassiveBase passive)
    {
        if (!_ownedBossPassives.Contains(passive))
            return;

        if (_equippedBossPassive == passive)
            return;

        if (_equippedBossPassive != null)
            _equippedBossPassive.Remove();

        _equippedBossPassive = passive;
        _equippedBossPassive.Apply();
    }

    public void UnequipBossPassive()
    {
        if (_equippedBossPassive == null)
            return;

        _equippedBossPassive.Remove();
        _equippedBossPassive = null;
    }
}
