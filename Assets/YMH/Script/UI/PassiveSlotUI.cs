using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PassiveSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _iconImage;
    [SerializeField] private GameObject _equippedBorder;

    private PassiveBase _passive;
    private Action<PassiveBase, RectTransform> _onClick;
    private Action<PassiveBase, RectTransform> _onHover;
    private Action _onExit;

    public void SetPassive(PassiveBase passive, Action<PassiveBase, RectTransform> onClick, bool isEquipped)
    {
        _button.onClick.RemoveListener(OnClick);

        _passive = passive;
        _onClick = onClick;

        bool hasPassive = passive != null;

        _iconImage.enabled = hasPassive;
        _iconImage.sprite = hasPassive ? passive.Data.Icon : null;
        _button.interactable = hasPassive;

        if (_equippedBorder != null)
            _equippedBorder.SetActive(hasPassive && isEquipped);

        if (hasPassive)
            _button.onClick.AddListener(OnClick);
    }

    public void SetPassive(PassiveBase passive, Action<PassiveBase, RectTransform> onClick)
    {
        SetPassive(passive, onClick, false);
    }

    public void SetPassive(PassiveBase passive, Action<PassiveBase> onClick)
    {
        SetPassive(passive, (selected, slotRect) => onClick?.Invoke(selected), false);
    }

    public void SetHover(Action<PassiveBase, RectTransform> onHover, Action onExit)
    {
        _onHover = onHover;
        _onExit = onExit;
    }

    private void OnClick()
    {
        _onClick?.Invoke(_passive, transform as RectTransform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_passive != null)
            _onHover?.Invoke(_passive, transform as RectTransform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _onExit?.Invoke();
    }
}