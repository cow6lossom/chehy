using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace Key._00.Scripts {
    public class Boss : MonoBehaviour { 
        [SerializeField] private SpriteRenderer _sr;
        [SerializeField] private ContactFilter2D  _whatIsPlayer;
        [SerializeField] private GameObject _healthBar;
        

        [SerializeField]private Transform _target;
        private Collider2D _bossCol;
        private float _health = 100;
        [SerializeField]private float attackRange;

        [SerializeField]
        private SpriteRenderer AttackSprite;
        
        private bool _isAttacking = false;
        private bool _canAttack = false;
        

        private void Awake() {
            _sr = GetComponentInChildren<SpriteRenderer>();
            _sr.color = Color.magenta;
            AttackSprite.enabled = false;
        }

        private void Update() {
            Vector2 range = _target.position - transform.position;
            if (range.magnitude > attackRange) return;
            
            if (_isAttacking ) return;
            
            if (_target != null) {
                StartCoroutine(WaitAttack());
                _isAttacking = true;
            }
        }

        public void GetDamage(float damage) {
            _health -= damage;
            Debug.Log(_health);
            if (_health <= 0) {
                Destroy(gameObject);
            }
        }

        private IEnumerator WaitAttack() {
            Debug.Log("Wait Attack");
            
            int pattern = Random.Range(0, 2);
            
            yield return new WaitForSeconds(3f);
            Debug.Log("3f");            
            
            Transform tr = AttackSprite.gameObject.transform;
                        
            
            if (pattern == 0) {
                _sr.color = Color.red;

                yield return StartCoroutine(Pattern1(tr));
            }
            else {
                _sr.color = Color.darkMagenta;
                yield return StartCoroutine(Pattern2(tr));

            }

            yield return StartCoroutine(EndAttackCoroutine());
        }

        private IEnumerator Pattern1(Transform target) {
            Debug.Log("attack 1");

            target.position = _target.position;
            target.localScale =  Vector3.one ;

            AttackSprite.enabled = true;
            
            yield return new WaitForSeconds(0.4f);

            for (int i = 0; i < 5; i++) {
                Vector3 ranV = new Vector2(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));  
                Attack(target.position + ranV, new Vector2(1, 1));
                Debug.Log("ATK" + i);
            }
        }
        
        private IEnumerator Pattern2(Transform target) {
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
            ColliderArray2D atcCol = Physics2D.OverlapBox(point, range, 0, _whatIsPlayer);

            if (atcCol.Length == 0) return;

            if (atcCol[0].TryGetComponent(out ImsiPlayer player)) {
                player.GetDamage(2);
            }
        }
        

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}