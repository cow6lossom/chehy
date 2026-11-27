using Tyu.Scripts.Modules;
using UnityEngine;

namespace Chahn._00.Scripts.Shop
{
    public sealed class ShopFlowController : MonoBehaviour
    {
        [Header("Shop")]
        [SerializeField] private GameObject shopWorldObject;
        [SerializeField] private ShopInteractable shopInteractable;
        [SerializeField] private ShopUI shopUI;
        [SerializeField] private ShopController shopController;
        

        [Header("Player Interaction")]
        [SerializeField] private Input playerInput;
        [SerializeField] private InteractModule interactModule;

      

        private void Awake()
        {
            HideShop(false);
        }
        
        private void OnEnable()
        {

            if (playerInput != null && interactModule != null)
                playerInput.OnlnteractPressed += interactModule.TryInteract;
        }

        private void OnDisable()
        {

            if (playerInput != null && interactModule != null)
                playerInput.OnlnteractPressed -= interactModule.TryInteract;
        }
        
        [ContextMenu("Test/Reveal Shop")]
        public void HandleBossDefeated() //보스 사망 연출과 보상 처리가 끝난 순간 hopFlowController.HandleBossDefeated()를 호출
        {
            if (shopWorldObject == null || shopInteractable == null)
            {
                Debug.LogError("상점 월드 오브젝트 또는 ShopInteractable이 연결되지 않았다.", this);
                return;
            }

            shopController?.BeginShopVisit();
            shopWorldObject.SetActive(true);
            shopInteractable.SetAvailable(true);
        }
        
        // 다음 방 이동 코드 또는 ShopRoomExitTrigger에서 호출
        public void HandleNextRoomEntered()
        {
            HideShop(true);
        }

        private void HideShop(bool endVisit)
        {
            shopUI?.Close();

            if (shopInteractable != null)
                shopInteractable.SetAvailable(false);

            if (endVisit)
                shopController?.EndShopVisit();

            if (shopWorldObject != null)
                shopWorldObject.SetActive(false);
        }
    }
}
