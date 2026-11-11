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

    private void Awake()
    {
        
    }
    
    
    public void Bind(ShopOffer offer) 
    {
        CurrentOffer = offer;
        offerNameText.text = CurrentOffer.Name;
        offerDescriptionText.text = CurrentOffer.PassiveDesc;
        offerLevelText.text = CurrentOffer.Level.ToString();
        offerPriceText.text = CurrentOffer.Price.ToString();
        offerIcon.sprite = CurrentOffer.PassiveIcon;
    }
}
