using System;
using Tyu.Scripts.Modules;
using UnityEngine;

public class ShopInteractable : MonoBehaviour, IInteractable
{
    
    public event Action OnInteracted;
    public bool IsAvailable { get; private set; }

    void Start()
    {
        Debug.Log(IsAvailable);
    }
    
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
