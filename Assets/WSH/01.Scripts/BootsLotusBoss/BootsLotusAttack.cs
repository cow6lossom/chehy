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

        [Header("BossStatSO")] public BLBossListSO List;

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
            lotusGrudge.GetBossStat(List.onePageList[0]);
            waterPillar.GetBossStat(List.onePageList[1]);
            pondHand.GetBossStat(List.onePageList[2]);
            dontTouch.GetBossStat(List.onePageList[3]);

            foreach (var item in List.twoPageList)
            {
                if (item == null)
                    return;
            }
            
            // lotusGrudge.GetBossStat(List.twoPageList[0]);
            // waterPillar.GetBossStat(List.twoPageList[1]);
            // pondHand.GetBossStat(List.twoPageList[2]);
            // dontTouch.GetBossStat(List.twoPageList[3]);
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
