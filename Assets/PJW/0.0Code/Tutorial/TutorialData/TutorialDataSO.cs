using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "TutorialDataSO", menuName = "SO/TutorialDataSO")]
public class TutorialDataSO : ScriptableObject
{
    [field:SerializeField]public string QuestName {  get; private set; }

    [field:SerializeField]public TutorialType Type {  get; private set; }

    [field:SerializeField]public Vector3 Position { get; private set; }

    [field:SerializeField]public float Radius { get; private set; }
}
public enum TutorialType
{
    GetItem,
    GoArrived,
}
