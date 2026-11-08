using System;
using Tyu.Scripts.Modules;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ShopInteractable : MonoBehaviour, IInteractable
{
    
    public event Action OnInteracted;
    public bool CanShopInteract { get; private set; }
    
    public void InteractEffect()
    {
        if (!CanShopInteract) return;
        
        OnInteracted?.Invoke();
    }
    
    public void SetAvailable(bool value)
    {
        CanShopInteract = value;
    }
    
    
}
