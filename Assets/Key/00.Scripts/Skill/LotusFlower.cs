using System;
using System.Collections;
using Tyu.Scripts;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Key._00.Scripts.Skill {
    [RequireComponent(typeof(Rigidbody2D))]
    public class LotusFlower : MonoBehaviour, IHealthHavable {
        [Header("Lotus Flower")]
        [SerializeField] private float MaxHP = 10f;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private float grabTime = 3f;
        [SerializeField] private float damageInterval = 1f; // 딜 주는 간격

        public event Action OnCoolTimeReset;

        private static LotusFlower _currentFlower;
        
        [SerializeField] private Transform _playerTrm;
        private AttackSettingSO attackSetting;
        private Rigidbody2D rigid;
        private Player _player;
        private LotusFlowerAnimation _animator;
        
        private float currentHP;
        

        private void Awake() {
            rigid = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Transform pTrm, AttackSettingSO  setting, Vector3 pos) {
            if (_currentFlower != null && _currentFlower != this) {
                _currentFlower.Die(); // 2번 생성 방지용
            }
            
            _animator =  GetComponentInChildren<LotusFlowerAnimation>();
            _currentFlower = this;
            transform.position = pos;
            
            currentHP = MaxHP;
            _playerTrm = pTrm;
            attackSetting = setting;
            _player = _playerTrm.GetComponent<Player>();
            
            StartCoroutine(LifeCoroutine());
        }

        private IEnumerator LifeCoroutine() {
            float time = 0f;

            while (time < lifeTime) {
                rigid.linearVelocity = ((Vector2)_playerTrm.position - rigid.position).normalized * speed;
                yield return new WaitForFixedUpdate();
                time += Time.fixedDeltaTime;
            }

            Die();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.TryGetComponent(out Player player)) return;
            
            _animator.SetCatch();

            StopAllCoroutines();
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine() {
            _player.MoveCompo.StopMove();
            _player.enabled = false;
            rigid.linearVelocity = Vector2.zero;

            float time = 0f;

            while (time < grabTime) {
                float waitTime = Mathf.Min(damageInterval, grabTime - time);
                yield return new WaitForSeconds(waitTime);
                time += waitTime;

                if (_player == null) {
                    Die();
                    yield break;
                }

                if (waitTime >= damageInterval) {
                    _player.GetDamage(attackSetting.Damage);
                }
            }

            Die();
        }


        public void GetDamage(float damage) {
            if (currentHP <= 0) return;

            currentHP -= damage;
            if (currentHP <= 0) {
                Die();
            }
        }

        private void Die() {
            _animator.SetDead();
            currentHP = 0;
            rigid.linearVelocity = Vector2.zero;
            StopAllCoroutines();
            
            Destroy(gameObject); // 풀링 할 거면 나중에 수정 
        }

        private void OnDisable() {
            rigid.linearVelocity = Vector2.zero;
            StopAllCoroutines();

            if (_player != null) 
                _player.enabled = true;
            
            if (_currentFlower == this) 
                _currentFlower = null;
            
            OnCoolTimeReset?.Invoke();
        }
    }
}
