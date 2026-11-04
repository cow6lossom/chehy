using System.Collections;
using PrimeTween;
using UnityEngine;

namespace WSH._01.Scripts
{
    public enum PTbossState
    {
        Idle,
        Move,
        Attack,
        OneSkill,
        TwoSkill,
        ThreeSkill
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class PTboss : MonoBehaviour // ㄹㅈㄷ 갓 클래스~
    {
        [SerializeField] private Rigidbody2D _rigid;
        [SerializeField] private Vector2 _moveDir;

        [SerializeField] private PTPlayer player;
        [SerializeField] private Transform _playerTrm;

        [SerializeField] private GameObject _bullet;
        [SerializeField] private GameObject _longHand;

        [SerializeField] private int _randomSkill;
        
        [SerializeField] private float _speed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _detectRange;

        [SerializeField] private float _attackCoolTime;
        [SerializeField] private float _lastaAtackTime;
        [SerializeField] private float _skillCoolTime;
        [SerializeField] private float _skillCurrentCoolTime;

        [SerializeField] private bool _attacking; // 공격 사용 중
        [SerializeField] private bool _skilling; // 스킬 사용 중
        [SerializeField] private bool _canMove;

        [SerializeField] private LayerMask _whatIsPlayer;

        private PTbossState _state;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _rigid = GetComponent<Rigidbody2D>();

            _state = PTbossState.Idle;
        }

        private void Update()
        {
            ChangeState();

            if (!_canMove)
                StopMove();
        }
        
        void ChangeState()
        {
            if (_state == PTbossState.Idle)
            {
                IdleState();
            }
            else if (_state == PTbossState.Move)
            {
                MoveState();
            }
            else if (_state == PTbossState.Attack && !_attacking)
            {
                AttackState();
            }
            else if (_state == PTbossState.OneSkill && !_skilling)
            {
                OneSkillState();
            }
            else if (_state == PTbossState.TwoSkill && !_skilling)
            {
                TwoSkillState();
            }
            else if (_state == PTbossState.ThreeSkill && !_skilling)
            {
                ThreeSkillState();
            }
        }

        void IdleState()
        {
            Debug.Log("idle");
            
            _canMove = false;
            Collider2D target = Physics2D.OverlapCircle(transform.position, _detectRange, _whatIsPlayer);

            // 추격 범위 안에 적이 있을 경우
            if (target != null)
            {
                _state = PTbossState.Move;
            }
        }

        void MoveState()
        {
            Debug.Log("move");
            _canMove = true;
            SetMove(_speed);

            Collider2D chaseTarget = Physics2D.OverlapCircle(transform.position, _detectRange, _whatIsPlayer);
            Collider2D target = Physics2D.OverlapCircle(transform.position, _attackRange, _whatIsPlayer);

            // 범위 안에 추격자가 플레이어가 없을 경우
            if (chaseTarget == null)
            {
                _state = PTbossState.Idle;
            }

            // 공격 범위 안에 적이 있을 경우
            if (target != null && !_attacking && !_skilling && _attackCoolTime + _lastaAtackTime < Time.time)
            {
                _state = PTbossState.Attack;
            }

            // 스킬 쿨타임이 다 되었을 경우
            if (!_skilling && !_attacking && _skillCoolTime > _skillCurrentCoolTime)
            {
                _skillCurrentCoolTime += Time.deltaTime;
            }
            else if (!_skilling && !_attacking)
            {
                _randomSkill = Random.Range(0, 2);

                if (_randomSkill == 0)
                {
                    _state = PTbossState.OneSkill;
                }
                else if (_randomSkill == 1)
                {
                    _state = PTbossState.TwoSkill;
                }
                //     else if (_randomSkill == 2)
                //     {
                //         _state = PTbossState.ThreeSkill;
                //     }
            }
        }

        void AttackState()
        {
            Debug.Log("attack");
            StartCoroutine(AttackLogic());
        }

        void OneSkillState()
        {
            Debug.Log("Oneattack");
            StartCoroutine(OneSkillLogic(65, 5, 3));
        }
        
        void TwoSkillState()
        {
            Debug.Log("Twoattack");
            StartCoroutine(TwoSkillLogic(6, 3, 3));
        }

        void ThreeSkillState()
        {
            Debug.Log("Threeattack");
            StartCoroutine(ThreeSkillLogic(3));
        }

        void SetMove(float speed)
        {
            _moveDir = TargetDirection();
            _rigid.linearVelocity = speed * _moveDir;
        }

        void StopMove()
        {
            _rigid.linearVelocity = Vector2.zero;
        }

        IEnumerator AttackLogic()
        {
            _attacking = true;
            _canMove = false;
            yield return new WaitForSeconds(2f);
            
            Collider2D target = Physics2D.OverlapCircle(transform.position, _attackRange, _whatIsPlayer);

            if (target != null)
            {
                Debug.Log("아무튼 공격");
            }
            else
            {
                Debug.Log("아무튼 공격 실패");
            }

            yield return new WaitForSeconds(2f);
            _lastaAtackTime = Time.time + _attackCoolTime;
            _attacking = false;
            _state = PTbossState.Idle;
        }

        IEnumerator OneSkillLogic(float amount, float slowTime, float time) // 슬로우 - 플레이어에게 속도를 감소 시킨다
        {
            _skilling = true;
            _canMove = false;
            yield return new WaitForSeconds(time);
            player.SlowEffect(amount, slowTime);
            _skillCurrentCoolTime = 0;
            _state = PTbossState.Idle;
            _skilling = false;
        }

        IEnumerator TwoSkillLogic(int amount, float time, float endTime) // 파편 던지기 - 투사체를 던진다
        {
            _skilling = true;
            _canMove = false;
            yield return new WaitForSeconds(time);
            
            for (int i = 0; i < amount; i++)
            {
                float x = Random.Range(-1f, 1f);
                float y = Random.Range(-1f, 1f);

                GameObject bullet = Instantiate(_bullet, new Vector3(transform.position.x + x, transform.position.y + y, 0), Quaternion.identity);
                bullet.transform.parent = transform;
                bullet.GetComponent<Rigidbody2D>().linearVelocity = TargetDirection() * 20f;
            }
            
            yield return new WaitForSeconds(endTime);
            _skillCurrentCoolTime = 0;
            _state = PTbossState.Idle;
            _skilling = false;
        }

        IEnumerator ThreeSkillLogic(float time) // 손 아귀 - 플레이어 쪽으로 손을 날려 잡히면 끌고온다.
        {
            _skilling = true;
            _canMove = false;
            yield return new WaitForSeconds(time);
            StartCoroutine(LongHand(1));
        }

        IEnumerator LongHand(float time)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan2(TargetDirection().y, TargetDirection().x);
            float distance = Vector2.Distance(_longHand.transform.position, _playerTrm.position);
            _longHand.SetActive(true);
            _longHand.transform.localScale = new Vector3(0, 1, angle);
            
            Tween.ScaleX(_longHand.transform, distance , time);
            yield return new WaitForSeconds(time);
            Tween.ScaleX(_longHand.transform, 0, time);
            yield return new WaitForSeconds(time);
            _skillCurrentCoolTime = 0;
            _state = PTbossState.Idle;
            _skilling = false;
        }
        
        Vector2 TargetDirection()
        {
            return (_playerTrm.position - transform.position).normalized;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, _detectRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}
