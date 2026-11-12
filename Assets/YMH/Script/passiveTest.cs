using UnityEngine;

public class passiveTest : MonoBehaviour
{
    [SerializeField] public PassiveManager passiveManager;
    [SerializeField] public PassiveData[] _testPassive;

    private void Start()
    {
        foreach(PassiveData data in _testPassive)
        {
            PassiveBase passive = PassiveFactory.Create(data);
            passiveManager.AddPassive(passive);
        }
    }

}
