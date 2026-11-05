using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class lectern : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask isPlayer;

    [SerializeField] private TextMeshPro text;

    public bool IsGameOver { get; set; }

    public bool isinteraction { get; set; }

    [SerializeField] private GameObject uiparent;

    private void Awake()
    {
        text.fontSize = 0;
        IsInteraction(false);
        uiparent.SetActive(false);
    }

    private void Update()
    {
        if(RangePlayer())
        {
            ShowTheSelectStage();
        }
        else
        {
            text.fontSize = 0;
        }
    }

    public bool RangePlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectRange,isPlayer);
        return player != null;
    }

    public void ShowTheSelectStage()
    {
        text.fontSize = 4;
        isinteraction = Keyboard.current.fKey.wasPressedThisFrame;
        if (isinteraction)
        {
            IsInteraction(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange); 
    }

    public void StageSelect(int stageNumber)
    {
        if(isinteraction)
        {
            Debug.Log(stageNumber);
        }
    }

    public void IsInteraction(bool value)
    {
        uiparent.SetActive(value);
    }
}
