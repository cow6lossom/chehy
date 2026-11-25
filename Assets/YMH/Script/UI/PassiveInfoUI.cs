using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PassiveInfoUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private Button _unequipButton;
    [SerializeField] private PassiveManager _passiveManager;
    [SerializeField] private PassiveEquipController _equipController;
    [SerializeField] private Vector2 _panelOffset = new Vector2(150, 0);
    [SerializeField] private Image _passiveIcon;
    private RectTransform _infoRect;
    private RectTransform _currentSlotRect;
    private PassiveBase _currentPassive;

    private bool _isPointerOnSlot;
    private bool _isPointerOnInfo;
    private bool _isReplacing;

    private void Awake()
    {
        _infoRect = _infoPanel.GetComponent<RectTransform>();
        _infoPanel.SetActive(false);

        _unequipButton.onClick.AddListener(OnUnequipClicked);
    }

    private void OnDestroy()
    {
        _unequipButton.onClick.RemoveListener(OnUnequipClicked);
    }

    public void ShowInfo(PassiveBase passive, RectTransform slotRect)
    {
        if (passive == null)
        {
            HideInfo();
            return;
        }
        _passiveIcon.sprite = passive.Data.Icon;
        _passiveIcon.enabled = passive.Data.Icon != null;

        _currentPassive = passive;
        _currentSlotRect = slotRect;

        _infoPanel.SetActive(true);

        _nameText.text = passive.Data.PassiveName;
        _descriptionText.text = passive.Data.Description;
        _levelText.text = "Lv." + passive.Level;

        bool isEquipped = _passiveManager.EquippedPassives.Contains(passive);
        int equippedCount = _passiveManager.EquippedPassives.Count;

        string buttonText = isEquipped ? "해제" : equippedCount >= 3 ? "교체" : "장착";

        _unequipButton.GetComponentInChildren<TMP_Text>().text = buttonText;
        _unequipButton.gameObject.SetActive(true);

        if (_currentSlotRect != null)
            _infoRect.position = _currentSlotRect.position + (Vector3)_panelOffset;
    }

    public void HideInfo()
    {
        _currentPassive = null;
        _currentSlotRect = null;
        _infoPanel.SetActive(false);
    }

    public void OnSlotEnter(PassiveBase passive, RectTransform slotRect)
    {
        _isPointerOnSlot = true;
        ShowInfo(passive, slotRect);
    }

    public void OnSlotExit()
    {
        _isPointerOnSlot = false;
        StartCoroutine(CheckHideNextFrame());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerOnInfo = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerOnInfo = false;
        StartCoroutine(CheckHideNextFrame());
    }

    private IEnumerator CheckHideNextFrame()
    {
        yield return null;

        if (!_isPointerOnSlot && !_isPointerOnInfo && !_isReplacing)
            HideInfo();
    }
    private void OnUnequipClicked()
    {
        if (_currentPassive == null) return;

        bool isEquipped = _passiveManager.EquippedPassives.Contains(_currentPassive);

        if (isEquipped)
        {
            _passiveManager.UnequipPassive(_currentPassive);
        }
        else if (_passiveManager.EquippedPassives.Count < 3)
        {
            _passiveManager.EquipPassive(_currentPassive);
        }
        else
        {
            _equipController.StartReplace(_currentPassive);
            return;
        }

        _equipController.RefreshAll();
        ShowInfo(_currentPassive, _currentSlotRect);
    }
    public void SetReplacing(bool isReplacing)
    {
        _isReplacing = isReplacing;

        if (!isReplacing)
            CheckHide();
    }
    private void CheckHide()
    {
        if (_isReplacing) return;

        if (!_isPointerOnSlot && !_isPointerOnInfo)
            HideInfo();
    }
}
