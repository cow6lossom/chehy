using System;
using UnityEngine;

namespace Tyu.Scripts.Modules
{
    public class RenderModule : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Material _mat;
        private IStateMachine _machine;
        private Transform _flipTarget;
        public void Initialize(IStateMachine machine, Transform trm)
        {
            _machine = machine;
            _flipTarget = trm;
        }

        public void FlipX(Vector2 dir)
        {
            dir = dir.normalized;
            _flipTarget.rotation = new Quaternion(0f, MathF.Sign(dir.x) > 0 ? 0 : 180, 0f, 0f);
        }
        
        public void EndTrigger() => _machine.EndTrigger();
        public void CallBackTrigger() => _machine.CallBackTrigger();

        public void ChangeAnimFloat(int hash, float value) => _animator.SetFloat(hash, value);
        public void ChangeAnimBool(int hash, bool value) => _animator.SetBool(hash, value);
        public void CallAnimTrigger(int hash) => _animator.SetTrigger(hash);
        public void ChangeMatFloat(int hash, float value) => _mat.SetFloat(hash, value);

    }
}