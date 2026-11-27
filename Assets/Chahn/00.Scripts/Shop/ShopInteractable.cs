using System;
using Tyu.Scripts.Modules;
using UnityEngine;

public class ShopInteractable : MonoBehaviour, IInteractable
{
    public event Action OnInteracted;
    public bool IsAvailable { get; private set; }
    
    public void InteractEffect()
    {
        if (!IsAvailable) return;
        
        OnInteracted?.Invoke();
    }
    
    public void SetAvailable(bool value)
    {
        IsAvailable = value;
    }
}
