using System;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Input InputCompo {  get; private set; }

    public PlayerMovment MoveCompo { get; private set; }

    private void Awake()
    {
        InputCompo = GetComponentInChildren<Input>();

        MoveCompo = GetComponentInChildren<PlayerMovment>();
    }

    private void Start()
    {
        InputCompo.OnMoveSet += MoveCompo.SetMoveDir;
    }

    private void OnDestroy()
    {
        InputCompo.OnMoveSet -= MoveCompo.SetMoveDir;
    }
}
