using System;
using PrimeTween;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Boss.RedbeanMouse
{
    public class RedbeanMouseHairpin : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private LineRenderer attackLine;
        [SerializeField] private Rigidbody2D rb;
        [Header("Settings")]
        [SerializeField ]private ContactFilter2D target;
        [SerializeField] private TweenSettings<Vector2> ShootingSetting;
        [SerializeField] private float ShootingDistance;
        [SerializeField] private TweenSettings<Vector2> GetSetting;

        private RedbeanMouseHairpinState _state;
        private bool _isAttacked;
        private AttackSettingSO _atkData;
        private BoxCollider2D _box;
        private Collider2D[] _results = new Collider2D[10];
        
        public void Init(AttackSettingSO data)
        {
            _atkData = data;
        }

        public void SetLine(Vector2 destination) //공격 범위 표시용
        {
            attackLine.useWorldSpace = true;
            attackLine.positionCount = 2;
            attackLine.SetPosition(0, transform.position);
            attackLine.SetPosition(1, destination);
            attackLine.enabled = true;
        }
        public void RemoveLine() //공격 범위 지우기
        {
            attackLine.enabled = false;
            attackLine.positionCount = 0;
        }
        public void Shoot(Vector2 dir) //쏘기
        {
            RemoveLine();
            gameObject.SetActive(true);
            _isAttacked = false;
            dir.Normalize();
            var twData = ShootingSetting;
            twData.startFromCurrent = true;
            twData.endValue = rb.position + dir * ShootingDistance;
            Tween.RigidbodyMovePosition(rb, twData);
        }

        public void Returning(Vector2 destination) //회수
        {
            _isAttacked = false;
            var twData = ShootingSetting;
            twData.startFromCurrent = true;
            twData.endValue = destination;
            Sequence.Create().Chain(Tween.RigidbodyMovePosition(rb, twData)).ChainCallback(() =>
            {
                _state.Returned();
                gameObject.SetActive(false);
            });

        }

        private void TryAttack()
        {
            int cnt = Physics2D.OverlapBox(transform.position, _box.size, transform.eulerAngles.z, target, _results);
            if (cnt < 1) return;

            for (int i = 0; i < cnt; i++)
            {
                var target = _results[i];

                if (target.TryGetComponent<IDamageable>(out var compo))
                {
                    compo.GetDamage(_atkData.Damage);
                    _isAttacked = true;
                    return;
                }
            }
        }

        private void FixedUpdate()
        {
            if(!_isAttacked)
                TryAttack();
        }
    }
}