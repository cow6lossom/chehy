using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PassiveSlotUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _iconImage;

    private PassiveBase _passive;
    private Action<PassiveBase> _onClick;

    public void SetPassive(PassiveBase passive, Action<PassiveBase> onClick)
    {
        _button.onClick.RemoveListener(OnClick);

        _passive = passive;
        _onClick = onClick;

        bool hasPassive = passive != null;

        _iconImage.enabled = hasPassive;
        _button.interactable = hasPassive || onClick != null;

        if (!hasPassive)
        {
            _iconImage.sprite = null;
        }
        else
        {
            _iconImage.sprite = passive.Data.Icon;
        }

        if (_onClick != null)
            _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _onClick?.Invoke(_passive);
    }
}
