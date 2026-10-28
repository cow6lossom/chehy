using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask isPlayer;

    [SerializeField] private GameObject UI;
    private bool isGameOver = false;

    public bool IsGameOver { get; set; }

    //stage 끝
    //player감지
    //UI보여주기
    //stage변경


    #region MyRegion

    #endregion
}
