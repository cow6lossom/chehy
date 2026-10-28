using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Key._00.Scripts {
    public class Boss : MonoBehaviour { 
        [SerializeField] private SpriteRenderer _sr;
        [SerializeField] private ContactFilter2D  _whatIsPlayer;
        [SerializeField] private GameObject _healthBar;
        [SerializeField] private float _detectRange;
        

        [SerializeField]private Transform _target;
        private Collider2D _bossCol;
        private Collider2D[] _targetCol =  new Collider2D[1];
        private int _health = 100;
        private float attackRange;

        [SerializeField]
        private SpriteRenderer AttackSprite;
        
        private bool _isAttacking = false;

        private void Awake() {
            _sr = GetComponentInChildren<SpriteRenderer>();
            _sr.color = Color.magenta;
            AttackSprite.enabled = false;
        }

        private void Update() {
            if (_isAttacking) return;

            DetectTarget();


            if (_target != null) {
                _isAttacking = true;
                StartCoroutine(WaitAttack());
            }
        }

        private void DetectTarget() {
            Physics2D.OverlapCircle(transform.position, _detectRange, _whatIsPlayer,  _targetCol);
            try {
                _target = _targetCol[0].transform;
            }
            catch (NullReferenceException) {
                _target = null;
            }

            if (_target == null) return;
            
            Vector2 v = _target.position - transform.position;
            if (v.magnitude > attackRange) {
                _target = null;
            }
        }
        
        

        public void GetDamage(int damage) {
            _health -= damage;
            Debug.Log(_health);
            if (_health <= 0) {
                Destroy(gameObject);
            }
        }

        private IEnumerator WaitAttack() {
            int pattern = Random.Range(0, 2);

            yield return new WaitForSeconds(1.5f);
            
            Transform tr = AttackSprite.gameObject.transform;
                        
            
            if (pattern == 0) {
                _sr.color = Color.red;

                yield return StartCoroutine(Attack1(tr));
            }
            else {
                _sr.color = Color.darkMagenta;
                yield return StartCoroutine(Attack2(tr));

            }

            yield return StartCoroutine(EndAttackCoroutine());
        }

        private IEnumerator Attack1(Transform target) {
            Debug.Log("attack 1");

            target.position = _target.position;
            target.localScale =  Vector3.one ;

            AttackSprite.enabled = true;
            
            yield return new WaitForSeconds(0.4f);
            
            Attack(target.position, new Vector2(1, 1));
        }
        
        private IEnumerator Attack2(Transform target) {
            Debug.Log("attack 2");
            
            target.position = transform.position;
            target.localScale =  new Vector3(7, 7, 1) ;

            AttackSprite.enabled = true;
            
            yield return new WaitForSeconds(0.5f);
            Attack(target.position, new Vector2(7, 7));
            
        }

        private IEnumerator EndAttackCoroutine() {
            yield return new WaitForSeconds(0.5f);
            AttackSprite.enabled = false;
            _sr.color = Color.magenta;
            _isAttacking = false;
        }
        
        private void Attack(Vector2 point, Vector2 range) {
            Physics2D.OverlapBox(point, range , 0, _whatIsPlayer, _targetCol);
            
            try {
                _target = _targetCol[0].transform;
            }
            catch (NullReferenceException) {
                _target = null;
            }
            
            _target.gameObject.GetComponent<ImsiPlayer>().GetDamage(2);
        }
        
        
        //target이 null이 안 돼요 이거 하기

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, _detectRange);
        }
    }
}