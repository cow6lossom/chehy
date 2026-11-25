using System;
using Tyu.Scripts;
using Tyu.Scripts.Results;
using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Modules
{
    public class AttackModule : MonoBehaviour
    {
        [SerializeField] private int maxBuffer;
        [SerializeField] private ContactFilter2D target;
        [SerializeField] private bool isPlayer;
        private Collider2D[] _resultArray;

        private void Awake()
        {
            _resultArray = new Collider2D[maxBuffer];
        }

        public void TryAttack(AttackSettingSO data)
        {
            var isRight = transform.right.x > 0 || isPlayer;
                Vector2 pos = (Vector2)transform.position + (data.Pos * new Vector2(isRight ? 1 : -1, 1));
                if (isPlayer)
                    pos = (Vector2)transform.position + (Vector2)(transform.rotation * data.Pos);
                int cnt = Physics2D.OverlapBox(pos, data.Size, transform.rotation.eulerAngles.z, target, _resultArray);
                if (cnt < 1) return;
            
                for (int i = 0; i < cnt; i++)
                {
                    var target = _resultArray[i];
                    Debug.Log(122323);
                    if (target.TryGetComponent<IDamageable>(out var compo))
                        compo.GetDamage(data.Damage);
                }
        }
    }
}