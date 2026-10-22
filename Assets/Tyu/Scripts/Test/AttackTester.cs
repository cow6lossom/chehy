using System;
using Tyu.Modules;
using Tyu.Scripts.Results;
using Tyu.Scripts.SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tyu.Scripts.Test
{
    public class AttackTester : MonoBehaviour
    {
        [SerializeField] private AttackModule attackMd;
        [SerializeField] private AttackSettingSO testSO;
        private void Update()
        {
            if (Keyboard.current.hKey.wasPressedThisFrame)
            {
                var test = new AttackResult(testSO);
                attackMd.TryAttack(test);
            }
        }
    }
}