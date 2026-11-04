using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    [SerializeField] private int stageNum;

    public bool IsGameOver { get; set; }

    public delegate void isGameOverHandler();

    public event isGameOverHandler GameWinHandler;

    private void OnEnable()
    {
        GameWinHandler += OpenBook;
    }

    public void Execute()
    {
        GameWinHandler.Invoke();
    }

    public void OpenBook()
    {
        //애니매이션?
    }

    public void CloseBook()
    {
        //애니매이션?
    }

    public void ChangeStage()
    {
        SceneManager.LoadScene(stageNum);
    }
}
