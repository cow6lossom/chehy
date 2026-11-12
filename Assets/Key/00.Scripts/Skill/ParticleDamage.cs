using System.Collections.Generic;
using UnityEngine;

namespace Key._00.Scripts.Skill {
    public class ParticleDamage : MonoBehaviour {
        [SerializeField] private float damage;
        [SerializeField] private Player _player;

        private ParticleSystem _ps;
        private List<ParticleSystem.Particle> particles = new();

        
        
        
        private void Awake() {
            
            _ps = GetComponent<ParticleSystem>();

            ParticleSystem.TriggerModule trigger = _ps.trigger;
            Collider2D col = _player.GetComponent<Collider2D>();
            trigger.enabled = true;
            trigger.enter = ParticleSystemOverlapAction.Callback;
            trigger.inside = ParticleSystemOverlapAction.Ignore;

            if (trigger.colliderCount == 0) {
                trigger.AddCollider(col);
            }
            else {
                trigger.SetCollider(0, col);
            }
        }

        private void OnParticleTrigger() {
            int count = _ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

            if (count > 0) {
                _player.GetDamage(damage);
                Debug.Log(damage);
            }
        }
    }
}
