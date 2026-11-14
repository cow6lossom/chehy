using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Shaders;
using WSH._09.SO;

namespace WSH._01.Scripts.BootsLotusBoss
{
    public class BootsLotusAttack : MonoBehaviour
    {
        [Header("BossSkill")]
        [SerializeField] private BLGrudge _lotusGrudge;
        [SerializeField] private BLWaterPillar _waterPillar;
        [SerializeField] private BLPondHand _pondHand;
        [SerializeField] private BLDontTouch _dontTouch;

        [Header("BossStatSO")] public BossListSO List;

        [Header("Number")]
        [SerializeField] private int _currentOrder;
        [SerializeField] private float _duringTime;
        
        public event Action OnDead;
        
        private void Awake()
        {
            Initialize();
            SkillWork();
        }

        private void OnEnable()
        {
            OnDead += _lotusGrudge.Dead;
            OnDead += _waterPillar.Dead;
            OnDead += _pondHand.Dead;
        }

        private void OnDisable()
        {
            OnDead -= _lotusGrudge.Dead;
            OnDead -= _waterPillar.Dead;
            OnDead -= _pondHand.Dead;
        }

        void Initialize()
        {
            InitializeCompo();
            // InitializeValue();
        }
        
        void InitializeCompo()
        {
            _lotusGrudge = GetComponentInChildren<BLGrudge>();
            _waterPillar = GetComponentInChildren<BLWaterPillar>();
            _pondHand = GetComponentInChildren<BLPondHand>();
            _dontTouch = GetComponentInChildren<BLDontTouch>();
        }

        void InitializeValue()
        {
            _lotusGrudge.GetBossStat(List.list[0]);
            _waterPillar.GetBossStat(List.list[1]);
            _pondHand.GetBossStat(List.list[2]);
            _dontTouch.GetBossStat(List.list[3]);
        }

        public void SkillWork()
        {
            if (_currentOrder == 0)
            {
                _lotusGrudge.StartCoroutine(_lotusGrudge.NoticeTime());
            }
            else if (_currentOrder == 1)
            {
                _waterPillar.StartCoroutine(_lotusGrudge.NoticeTime());
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

        public void ChangeSkill(int skillNumber)
        {
            _currentOrder = skillNumber;
        }
    }
}
