using System.Collections.Generic;
using UnityEngine;

public class PassiveManager : MonoBehaviour
{
    private List<PassiveBase> _passives = new();

    public void AddPassive(PassiveBase passive)
    {
        _passives.Add(passive);
        passive.Apply();
    }

    public void RemovePassive(PassiveBase passive)
    {
        passive.Remove();
        _passives.Remove(passive);
    }
}