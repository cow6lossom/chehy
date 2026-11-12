using System;
using System.Collections;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Key._00.Scripts.Skill {
    public class LotusFlower2 : MonoBehaviour {
        [Header("Lotus Flower 2")]
        [SerializeField] private Rigidbody2D[] flowers;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float distance = 5f;
        [SerializeField] private float flowerDistanceInterval = 1f; // 꽃 사이의 간격
        
        
        [SerializeField] private float flowerInterval = 0.5f;
        [SerializeField] private float explosionDelay = 1f;
        [SerializeField] private float explosionRange = 2f;
        
        [SerializeField] private LayerMask _whatIsPlayer;
        [SerializeField] private ParticleSystem gatherParticle;
        [SerializeField] private ParticleSystem explosionParticle;

        public event Action OnAttackEnd;

        private Vector2 _dir;
        private AttackSettingSO attackSetting;
        private LotusFlowerAnimation _animator;

        public void Initialize(Vector2 dir, AttackSettingSO setting, Vector3 pos) {
                _animator = gameObject.GetComponentInChildren<LotusFlowerAnimation>();
            
            transform.position = pos;
            foreach (Rigidbody2D flower in flowers) {
                flower.position = flower.transform.position;
            }

            _dir = dir.normalized;
            attackSetting = setting;
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine() {
            Coroutine[] flowerCoroutines = new Coroutine[flowers.Length];

            for (int i = 0; i < flowers.Length; i++) {
                flowerCoroutines[i] = StartCoroutine(FlowerCoroutine(flowers[i], i));
                if (i < flowers.Length - 1) {
                    yield return new WaitForSeconds(flowerInterval);
                }
            }

            for (int i = 0; i < flowers.Length; i++) {
                yield return flowerCoroutines[i];
                Explode(flowers[i].position);
                Destroy(flowers[i].gameObject);

                if (i < flowers.Length - 1) {
                    yield return new WaitForSeconds(flowerInterval);
                }
            }

            OnAttackEnd?.Invoke();
            Destroy(gameObject);
        }

        private IEnumerator FlowerCoroutine(Rigidbody2D flower, int index) {
            float moveDistance = Mathf.Max(0f, distance - flowerDistanceInterval * index);
            flower.linearVelocity = _dir * speed;

            if (moveDistance > 0f) {
                yield return new WaitForSeconds(moveDistance / speed);
            }

            flower.linearVelocity = Vector2.zero;

            ParticleSystem particle = Instantiate(gatherParticle, flower.position, gatherParticle.transform.rotation, flower.transform);
            particle.gameObject.SetActive(true);
            particle.Stop(true);
            particle.Play();
            yield return new WaitForSeconds(explosionDelay);
        }

        private void Explode(Vector2 pos) {
            ParticleSystem particle = Instantiate(explosionParticle, pos, explosionParticle.transform.rotation);
            particle.gameObject.SetActive(true);
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particle.Play();
            
            Destroy(particle.gameObject, particle.main.duration + particle.main.startLifetime.constantMax);

            Collider2D[] targets = Physics2D.OverlapCircleAll(pos, explosionRange, _whatIsPlayer);
            foreach (Collider2D target in targets) {
                if (target.TryGetComponent(out Player player)) {
                    player.GetDamage(attackSetting.Damage);
                    break;
                }
            }
        }
    }
}
