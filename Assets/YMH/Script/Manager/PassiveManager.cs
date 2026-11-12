using System.Collections.Generic;
using UnityEngine;

public class PassiveManager : MonoBehaviour
{
    private List<PassiveBase> _ownedPassives = new List<PassiveBase>();
    private List<PassiveBase> _equippedPassives = new List<PassiveBase>();

    private const int MaxOwnedCount = 8;
    private const int MaxEquippedCount = 3;



    public IReadOnlyList<PassiveBase> OwnedPassives => _ownedPassives;
    public IReadOnlyList<PassiveBase> EquippedPassives => _equippedPassives;
    public List<PassiveBase> GetAvailablePassives(List<PassiveBase> candidates)
    {
        List<PassiveBase> available = new List<PassiveBase>();

        foreach (PassiveBase passive in candidates)
        {
            if (passive == null || passive.Data == null)
                continue;

            PassiveBase owned = _ownedPassives.Find(p => p.Data.PassiveName == passive.Data.PassiveName);

            if (owned == null || owned.Level < 3)
                available.Add(passive);
        }

        return available;
    }
    public bool AddPassive(PassiveBase passive)
    {
        if (passive == null || passive.Data == null)
            return false;

        PassiveBase existing = _ownedPassives.Find(p => p.Data.PassiveName == passive.Data.PassiveName);

        if (existing != null) //ㅣ기존 패시브가 있는 경우
        {
            if (existing.Level >= 3) //그 기존 패시브가 레벨 3보다 크다면 리턴
                return false;

            if (passive.Level < existing.Level) //구매한 페시브의 레벨이 더 낮다면 그냥 구매
                return true;
            
            //레벨업 될수있는 상황에서
            bool isEquipped = _equippedPassives.Contains(existing); //착용하고 있다면
            
            if (isEquipped) //다시 뺀다
                UnequipPassive(existing);
            
            if (passive.Level == existing.Level) 
            {
                existing.LevelUp(); //레벨업 한번
            }
            
            while (existing.Level < passive.Level) //보유한 패시브가 새로운 패시브 보다 레벨이 작다면 
            {
                existing.LevelUp(); //레벨업해서 그 레벨로
            }
            
            if (isEquipped)
                EquipPassive(existing);// 다시 페시브 착용
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
    public bool ReplaceEquippedPassive(PassiveBase current, PassiveBase next)
    {
        int index = _equippedPassives.IndexOf(current);

        if (index < 0 || next == null || !_ownedPassives.Contains(next))
            return false;

        if (_equippedPassives.Contains(next))
            return false;

        current.Remove();
        _equippedPassives[index] = next;
        next.Apply();

        return true;
    }
}
