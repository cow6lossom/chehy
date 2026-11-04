using System;
using UnityEngine;

namespace Key._00.Scripts.DummyEnemy {
    public class Dummy : MonoBehaviour, IDamageableDummy, IKnockbackableDummy {
        [SerializeField] private float maxHealth;

        [SerializeField] private float knockbackForce = 5f;
        private Rigidbody2D rigid;

        public float CurrentHealth { get; private set; }

        private void Awake() {
            CurrentHealth = maxHealth;
            rigid = GetComponent<Rigidbody2D>();
        }

        public void GetDamage(float damage) {
            CurrentHealth -= damage;
            GetKnockback(transform.position);
            Debug.Log(CurrentHealth);

            if (CurrentHealth <= 0) {
                Die();
            }
        }

        private void Die() {
            Destroy(gameObject);
        }
        

        public void GetKnockback(Vector2 dir) {
            Debug.Log("GET KNOCKBACK");
            Vector2 direction =
                ((Vector2)transform.position - dir).normalized;

            rigid.linearVelocity = Vector2.zero;
            rigid.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        }
    }
}