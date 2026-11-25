using System;
using System.Collections;
using UnityEngine;
using WSH._09.SO;

namespace WSH._01.Scripts.BootsLotusBoss
{
    public class BootsLotusAttack : MonoBehaviour
    {
        [Header("BossSkill")]
        public BLGrudge lotusGrudge { get; private set; }
        public BLWaterPillar waterPillar { get; private set; }
        public BLPondHand pondHand { get; private set; }
        public BLDontTouch dontTouch { get; private set; }

        [Header("BossStatSO")] public BossListSO[] List;

        [Header("Number")]
        [SerializeField] private int _currentOrder;
        [SerializeField] private float _duringTime;
        
        public event Action OnDead;
        
        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            OnDead += lotusGrudge.Dead;
            OnDead += waterPillar.Dead;
            OnDead += pondHand.Dead;
        }

        private void OnDisable()
        {
            OnDead -= lotusGrudge.Dead;
            OnDead -= waterPillar.Dead;
            OnDead -= pondHand.Dead;
        }

        void Initialize()
        {
            InitializeCompo();
            InitializeDataSet(0);
        }
        
        void InitializeCompo()
        {
            lotusGrudge = GetComponentInChildren<BLGrudge>();
            waterPillar = GetComponentInChildren<BLWaterPillar>();
            pondHand = GetComponentInChildren<BLPondHand>();
            dontTouch = GetComponentInChildren<BLDontTouch>();
        }

        void InitializeDataSet(int value)
        {
            lotusGrudge.GetBossStat(List[value].list[0]);
            waterPillar.GetBossStat(List[value].list[1]);
            pondHand.GetBossStat(List[value].list[2]);
            dontTouch.GetBossStat(List[value].list[3]);
        }

        public void SkillWork()
        {
            if (_currentOrder == 0)
            {
                lotusGrudge.StartCoroutine(lotusGrudge.NoticeTime());
            }
            else if (_currentOrder == 1)
            {
                waterPillar.StartCoroutine(waterPillar.NoticeTime());
            }
        }

        public IEnumerator NextSkill()
        {
            _currentOrder++;
            if (_currentOrder >= 2) _currentOrder = 0;
            yield return new WaitForSeconds(_duringTime);
            SkillWork();
        }

        public void RaiseDead()
        {
            OnDead?.Invoke();
        }
    }
}
