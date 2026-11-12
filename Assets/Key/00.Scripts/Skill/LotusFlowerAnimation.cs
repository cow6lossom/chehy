using UnityEngine;

namespace Key._00.Scripts.Skill {
    public class LotusFlowerAnimation : MonoBehaviour {
        private Animator _animator;
        
        
        public bool IsEnd { get; private set; }

        private readonly int deadHash = Animator.StringToHash("DEAD");
        private readonly int catchHash = Animator.StringToHash("CATCH");
        

        private void Awake() {
            _animator = GetComponent<Animator>();
            IsEnd = false;
        }

        public void SetDead() {
            IsEnd = false;
            _animator.SetBool(deadHash, true);
        }

        public void SetCatch() {
            _animator.SetBool(catchHash, true);
        }

        public void AnimationEnd() {
            IsEnd = true;
        }
    }
}
