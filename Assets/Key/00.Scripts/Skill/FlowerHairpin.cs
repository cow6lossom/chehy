using System;
using UnityEngine;

namespace Key._00.Scripts.Skill {
    public class FlowerHairpin : MonoBehaviour {
        [SerializeField] private ParticleSystem ps;
        [SerializeField] private float delay;
        
        public event Action OnAttackend;

        
        public void Initialize() {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // 원래 것들 지우고 생성 ㄴㄴ
            ps.time = 0f;

            var burst = ps.emission.GetBurst(0);
            burst.time = delay;
            ps.emission.SetBurst(0, burst);
            
            
            ps.Play();  
        }


        private void Update() {
            if (ps.IsAlive(true)) {
                OnAttackend?.Invoke();
            }
        }

        // 딜 처리는 Particle ParticleDamage에서 처리합니다
    }
}