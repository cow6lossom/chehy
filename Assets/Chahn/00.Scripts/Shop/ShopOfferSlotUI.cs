using System;
using Chahn._00.Scripts.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopOfferSlotUI : MonoBehaviour
{
    private ShopOffer CurrentOffer { get; set; }

    [SerializeField] private TextMeshProUGUI offerNameText;
    [SerializeField] private TextMeshProUGUI offerDescriptionText;
    [SerializeField] private TextMeshProUGUI offerLevelText;
    [SerializeField] private TextMeshProUGUI offerPriceText;
    [SerializeField] private Image offerIcon;
    [SerializeField] private Button buyButton;
    
    public event Action<ShopOffer> OnPurchaseRequested;


    private void Awake()
    {
        buyButton.onClick.AddListener(HandleBuyButtonClicked); //버튼 누루면
    }

    private void OnDestroy()
    {
        buyButton.onClick.RemoveListener(HandleBuyButtonClicked);
    }


    private void HandleBuyButtonClicked() //buy button clicked execute
    {
        if (CurrentOffer == null || CurrentOffer.IsPurchased) return; 
        
        OnPurchaseRequested?.Invoke(CurrentOffer); //이 슬롯 정보 넘겨준다, 지금 어떤 패시브를 보여주고 있는지도 그리고 구독한 함수 실행
    }
    
    
    public void Bind(ShopOffer offer) 
    {
        CurrentOffer = offer;
        offerNameText.text = CurrentOffer.Name;
        offerDescriptionText.text = CurrentOffer.PassiveDesc;
        offerLevelText.text = CurrentOffer.Level.ToString();
        offerPriceText.text = CurrentOffer.Price.ToString();
        offerIcon.sprite = CurrentOffer.PassiveIcon;
        buyButton.interactable = true;
    }

    public void SetPurchaseAvailable(bool value)
    {
        buyButton.interactable = value && (CurrentOffer != null && !CurrentOffer.IsPurchased);
    }
}
