using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Key._00.Scripts {
    public class Boss : MonoBehaviour {
        [SerializeField] private SpriteRenderer _sr;
        [SerializeField] private ContactFilter2D _whatIsPlayer;
        [SerializeField] private GameObject _healthBar;


        [SerializeField] private Transform _target;
        private Collider2D _bossCol;
        private float _health = 100;
        [SerializeField] private float attackRange;

        [SerializeField] private SpriteRenderer AttackSprite;

        private bool _isAttacking = false;


        private void Awake() {
            _sr = GetComponentInChildren<SpriteRenderer>();
            _sr.color = Color.magenta;
            AttackSprite.enabled = false;
        }


        private void Update() {
            if (_isAttacking || _target == null) return;

            Vector2 range = _target.position - transform.position;
            if (range.magnitude > attackRange) return;

            _isAttacking = true;
            StartCoroutine(WaitAttack());
        }

        public void GetDamage(float damage) {
            _health -= damage;
            Debug.Log(_health);
            if (_health <= 0) {
                Destroy(gameObject);
            }
        }


        private IEnumerator WaitAttack() {
            int pattern = Random.Range(0, 2);
            Transform tr = AttackSprite.transform;

            if (pattern == 0) {
                _sr.color = Color.red;

                tr.position = _target.position;
                tr.localScale = Vector3.one;
            }
            else {
                _sr.color = Color.darkMagenta;

                tr.position = transform.position;
                tr.localScale = new Vector3(7, 7, 1);
            } //선딜레이

            AttackSprite.enabled = true;

            yield return new WaitForSeconds(0.5f);

            _sr.color = Color.magenta;

            if (pattern == 0) {
                //패턴
                Pattern1(tr);
            }
            else {
                Pattern2(tr);
            }

            yield return StartCoroutine(EndAttackCoroutine()); //후딜레이
        }



        private void Pattern1(Transform target) {
            Debug.Log("attack 1");

            for (int i = 0; i < 5; i++) {
                Vector3 ranV = new Vector2(
                    Random.Range(-3.0f, 3.0f),
                    Random.Range(-3.0f, 3.0f)
                );

                Attack(target.position + ranV, new Vector2(1, 1));
            }
        }

        private void Pattern2(Transform target) {
            Debug.Log("attack 2");

            Attack(target.position, new Vector2(7, 7));
        }



        private IEnumerator EndAttackCoroutine() {
            AttackSprite.enabled = false;
            
            yield return new WaitForSeconds(1.5f); 
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