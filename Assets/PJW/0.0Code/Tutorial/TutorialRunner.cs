using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
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
        for (int i = 0; i < datas.Length; i++)
        {
            tutorialDic.Add(i, datas[i]);
            Debug.Log(i);   
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

    public async void RunningTutorial()
    {
        Debug.Log($"QuestNumber Start");
        await taskCompletionSource.Task;

    }

    public void CheackSuscessTutorial()
    {
        if(currentTutorialDataSO.Type == TutorialType.GetItem)
        {

        }
        else if(currentTutorialDataSO.Type == TutorialType.GoArrived)
        {

        }
    }

    private void OnDrawGizmos()
    {
        if(currentTutorialDataSO.Type == TutorialType.GoArrived)
        {
            Gizmos.DrawSphere(currentTutorialDataSO.Position, currentTutorialDataSO.Radius);
        }
    }
}
