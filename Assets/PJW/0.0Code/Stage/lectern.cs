using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class lectern : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask isPlayer;

    [SerializeField] private GameObject[] stageSelectUI;

    public bool IsGameOver { get; set; }

    public bool Isinteraction { get; set; }

    private void Awake()
    {
        ActiveSelectStageUI(false);
    }

    private void Update()
    {
        if(RangePlayer())
        {
            ShowTheSelectStage();
        }
    }

    public bool RangePlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectRange,isPlayer);
        return player != null;
    }

    public void ShowTheSelectStage()
    {
        Debug.Log("상호작용 UI만들기");
        if(Keyboard.current.fKey.wasPressedThisFrame)
        {
            Debug.Log("F키누름");
            ActiveSelectStageUI(true);
        }
    }

    public void SelectStage(int NumberScene)
    {
        SceneManager.LoadScene(NumberScene);
    }

    public void ActiveSelectStageUI(bool value)
    {
        for (int i = 1; i < stageSelectUI.Length; i++)
        {
            stageSelectUI[i].SetActive(value);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
