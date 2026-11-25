using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.InputSystem;
public class TutorialRunner : MonoBehaviour
{
    [SerializeField] private TutorialDataSO[] datas;

    private TaskCompletionSource<bool> taskCompletionSource;

    private Dictionary<int, TutorialDataSO> tutorialDic;

    private TutorialDataSO currentTutorialDataSO;

    public void Init()
    {
        tutorialDic = new();
        taskCompletionSource = new();
        for (int i = 0; i < datas.Length; i++)
        {
            tutorialDic.Add(i, datas[i]);
        }
        currentTutorialDataSO = tutorialDic[0];
    }

    public void Change()
    {
        int currentNumberQuest = tutorialDic.FirstOrDefault(x => x.Value == currentTutorialDataSO).Key;
        currentTutorialDataSO = tutorialDic[currentNumberQuest+1];
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        RunningTutorial();
    }

    public async void RunningTutorial()
    {
        Debug.Log($"QuestNumber Start");
        await WaitForStepAsync();
        Debug.Log($"QuestNumber End");

    }

    public async Task WaitForStepAsync()
    {
        taskCompletionSource = new TaskCompletionSource<bool>();
        await taskCompletionSource.Task;
    }


    public void CheackSuscessTutorial()
    {
        if(taskCompletionSource != null && !taskCompletionSource.Task.IsCompleted)
        {
            if(currentTutorialDataSO.Type == TutorialType.GetItem)
            {
                taskCompletionSource.SetResult(true);
            }
            else if(currentTutorialDataSO.Type == TutorialType.GoArrived)
            {
                taskCompletionSource.SetResult(true);
            }
        Change();
        }
    }

    private void OnDrawGizmos()
    {
        if (currentTutorialDataSO == null) return; 
        if(currentTutorialDataSO.Type == TutorialType.GoArrived)
        {
            Gizmos.DrawWireSphere(currentTutorialDataSO.Position, currentTutorialDataSO.Radius);
        }
    }
}
