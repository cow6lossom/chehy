using UnityEngine;
using UnityEngine.Splines;

public class Player : MonoBehaviour
{
    public Input InputCompo { get; private set; }

    private void Awake()
    {
        InputCompo = GetComponentInChildren<Input>();
    }

    private void Update()
    {
    }
}
