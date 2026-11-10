using UnityEngine;
using TMPro;

public class PassiveInfoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private GameObject _infoPanel;

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
    }

    public void HideInfo()
    {
        _infoPanel.SetActive(false);
    }
}
