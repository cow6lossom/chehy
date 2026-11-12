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
        
        [SerializeField] private float flowerInterval = 0.5f;
        [SerializeField] private float explosionDelay = 1f;
        [SerializeField] private float explosionRange = 2f;
        
        [SerializeField] private LayerMask _whatIsPlayer;
        [SerializeField] private ParticleSystem gatherParticle;
        [SerializeField] private ParticleSystem explosionParticle;

        public event Action OnAttackEnd;

        private Vector2 _dir;
        private AttackSettingSO attackSetting;

        public void Initialize(Vector2 dir, AttackSettingSO setting) {
            _dir = dir.normalized;
            attackSetting = setting;
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine() {
            for (int i = 0; i < flowers.Length; i++) {
                StartCoroutine(FlowerCoroutine(flowers[i], i == flowers.Length - 1));
                yield return new WaitForSeconds(flowerInterval);
            }
        }

        private IEnumerator FlowerCoroutine(Rigidbody2D flower, bool isLast) {
            flower.linearVelocity = _dir * speed;
            
            yield return new WaitForSeconds(distance / speed);
            
            flower.linearVelocity = Vector2.zero;

            ParticleSystem particle = Instantiate(gatherParticle, flower.position, gatherParticle.transform.rotation, flower.transform);
            particle.gameObject.SetActive(true);
            particle.Stop(true);
            particle.Play();
            yield return new WaitForSeconds(explosionDelay);

            Explode(flower.position);
            Destroy(flower.gameObject);

            if (isLast) {
                OnAttackEnd?.Invoke();
                Destroy(gameObject);
            }
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
