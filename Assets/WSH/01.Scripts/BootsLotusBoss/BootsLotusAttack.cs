using System;
using UnityEngine;
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

        private ISkill _currentSkill;
        
        private void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            InitializeCompo();
            InitializeValue();
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

        void SkillWork()
        {
            _currentSkill.Attack();
        }
    }
}
