using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Key._00.Scripts {
    public class ImsiPlayer : MonoBehaviour {
        [SerializeField] private ContactFilter2D _whatIsEnemy;

        [SerializeField] private float damage ;
        [SerializeField] private float health = 5;
        [SerializeField] private Transform _attackPoint;
        
        private SpriteRenderer sr;
        private Rigidbody2D rigid;
        
        private float _attackRange = 2;
        private float speed = 5f;
        
        private Collider2D[] _targetArr = new Collider2D[1];
        private Collider2D _target;
        private Color _color;

        private void Awake() {
            rigid = GetComponent<Rigidbody2D>();
            sr = GetComponentInChildren<SpriteRenderer>();
            _color = sr.color;
        }

        public void OnMove(InputValue v) {
            Vector2 input = v.Get<Vector2>();
            rigid.linearVelocity = input * speed;
        }

        public void OnAttack() {
            Physics2D.OverlapCircle(transform.position, _attackRange, _whatIsEnemy, _targetArr);

            if (_target == null) return;
            
            _target = _targetArr[0];
            StartCoroutine(ShowAttack());
        }

        private IEnumerator ShowAttack() {
            _attackPoint.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(0.3f);
            
            if(_target == null) yield break;
            
            _target.GetComponent<Boss>().GetDamage(damage);
            _attackPoint.gameObject.SetActive(false);
        }

        public void GetDamage(float dddam) {
            health -= dddam;
            if (health <= 0) {
                Destroy(gameObject);
            }

            StartCoroutine(DamageRoutine());
        }

        private IEnumerator DamageRoutine() {
            sr.color = Color.black;
            yield return new WaitForSeconds(0.05f);
            sr.color = _color;
        }
    }
}