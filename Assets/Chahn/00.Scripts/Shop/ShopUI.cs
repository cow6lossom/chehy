using UnityEngine;

namespace Chahn._00.Scripts.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private ShopInteractable shopInteractable;
        [SerializeField] private GameObject shopPanel;
        private bool IsOpen => shopPanel.activeSelf;

        private void Awake()
        {
            shopInteractable.OnInteracted += Toggle;
        }

        private void OnDestroy()
        {
            shopInteractable.OnInteracted -= Toggle;
        }

        private void Toggle()
        {
            if (IsOpen)
            {
                shopPanel.SetActive(false);
                return;
            }
            
            shopPanel.SetActive(true);
            
        }
    }
}