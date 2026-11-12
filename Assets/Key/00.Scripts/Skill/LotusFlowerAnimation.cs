using UnityEngine;

namespace Key._00.Scripts.Skill {
    public class LotusFlowerAnimation : MonoBehaviour {
        private Animator _animator;
        
        
        private readonly int deadHash = Animator.StringToHash("DEAD");
        private readonly int catchHash = Animator.StringToHash("CATCH");
        
        private void Awake() {
            _animator = GetComponent<Animator>();
        }

        public void SetDead() {
            _animator.SetBool(deadHash, true);
        }

        public void SetCatch() {
            _animator.SetBool(catchHash, true);
        }
    }
}