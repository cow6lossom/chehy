using UnityEngine;

namespace Tyu.Scripts.Modules
{
    public class RenderModule : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Material _mat;
        private IStateMachine _machine;

        public void Initialize(IStateMachine machine)
        {
            _machine = machine;
        }

        public void StateTrigger() => _machine.StateTrigger();

        public void ChangeAnimFloat(int hash, float value) => _animator.SetFloat(hash, value);
        public void ChangeAnimBool(int hash, bool value) => _animator.SetBool(hash, value);
        public void CallAnimTrigger(int hash) => _animator.SetTrigger(hash);
        public void ChangeMatFloat(int hash, float value) => _mat.SetFloat(hash, value);

    }
}