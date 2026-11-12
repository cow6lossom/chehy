
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;

public class PassiveInfoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private Button _unequipButton;
    [SerializeField] private PassiveManager _passiveManager;
    [SerializeField] private PassiveEquipController _equipController;
    [SerializeField] private Vector2 _mouseOffset = new Vector2(20, -20);

    private RectTransform _infoRect;
    private Canvas _canvas;
    private PassiveBase _currentPassive;

    private void Awake()
    {
        _infoRect = _infoPanel.GetComponent<RectTransform>();
        _canvas = _infoPanel.GetComponentInParent<Canvas>();

        _infoPanel.SetActive(false);
        _unequipButton.onClick.AddListener(OnUnequipClicked);
    }

    private void OnDestroy()
    {
        _unequipButton.onClick.RemoveListener(OnUnequipClicked);
    }

    private void Update()
    {
        if (!_infoPanel.activeSelf || Mouse.current == null || _canvas == null)
            return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        RectTransform parentRect = _infoRect.parent as RectTransform;
        Camera cam = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, mousePosition, cam, out Vector2 localPoint))
            _infoRect.anchoredPosition = localPoint + _mouseOffset;
    }

    public void ShowInfo(PassiveBase passive)
    {
        if (passive == null)
        {
            HideInfo();
            return;
        }

        _infoPanel.SetActive(true);
        _nameText.text = passive.Data.PassiveName;
        _descriptionText.text = passive.Data.Description;

        bool isEquipped = _passiveManager.EquippedPassives.Contains(passive);
        _unequipButton.gameObject.SetActive(isEquipped);
    }

    public void HideInfo()
    {
        _currentPassive = null;
        _infoPanel.SetActive(false);
    }

    private void OnUnequipClicked()
    {
        if (_currentPassive == null)
            return;

        _passiveManager.UnequipPassive(_currentPassive);

        _equipController.RefreshAll();
        ShowInfo(_currentPassive);
    }
}
