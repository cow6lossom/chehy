using UnityEngine;

namespace Key._00.Scripts.DummyEnemy {
    public interface IKnockbackableDummy {
        public void GetKnockback(Vector2 dir);
    }
}