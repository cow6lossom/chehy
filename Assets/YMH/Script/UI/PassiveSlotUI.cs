using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PassiveSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private PassiveInfoUI _infoUI;

    private PassiveBase _passive;
    private Action<PassiveBase> _onClick;

    public void SetPassive(PassiveBase passive, Action<PassiveBase> onClick)
    {
        _button.onClick.RemoveListener(OnClick);

        _passive = passive;
        _onClick = onClick;

        bool hasPassive = passive != null;

        _iconImage.enabled = hasPassive;
        _button.interactable = hasPassive;

        if (!hasPassive)
        {
            _iconImage.sprite = null;
            _levelText.text = "";
            return;
        }

        _iconImage.sprite = passive.Data.Icon;
        _levelText.text = "Lv." + passive.Level;

        if (_onClick != null)
            _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _onClick?.Invoke(_passive);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_passive != null && _infoUI != null)
            _infoUI.ShowInfo(_passive);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_infoUI != null)
            _infoUI.HideInfo();
    }
}