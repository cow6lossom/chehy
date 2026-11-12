using Tyu.Scripts.SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Key._00.Scripts.Skill {
    public class LotusFlowerTest : MonoBehaviour {
        
        //연꽃들이 제대로 작동하는 지 테스트 하기 위해 이 부분은 AI로 작성하였습니다.
        
        
        
        
        
        [SerializeField] private Player _player;
        [SerializeField] private LotusFlower lotusFlower;
        [SerializeField] private AttackSettingSO attackSetting;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float damage = 5f;

        private LotusFlower _currentFlower;
        [SerializeField] private LotusFlower2 lotusFlower2;
        [SerializeField] private AttackSettingSO attackSetting2;
        private LotusFlower2 _currentFlower2;
        private float _nextSpawnTime;
        private float _nextSpawnTime2;

        private void Start() {
            _player.InputCompo.OnAttackPressed += TryAttack;
            SpawnFlower2();
        }

        private void Update() {
            if (Keyboard.current.rKey.wasPressedThisFrame) {
                SpawnFlower();
            }

            if (Keyboard.current.tKey.wasPressedThisFrame) {
                SpawnFlower2();
            }
        }

        private void SpawnFlower() {
            if (_currentFlower != null || Time.time < _nextSpawnTime) return;

            _currentFlower = Instantiate(lotusFlower, lotusFlower.transform.position, Quaternion.identity);
            _currentFlower.OnCoolTimeReset += ResetCoolTime;
            _currentFlower.gameObject.SetActive(true);
            _currentFlower.Initialize(_player.transform, attackSetting, lotusFlower.transform.position);
        }

        private void TryAttack() {
            if (_currentFlower == null) return;
            if (Vector2.Distance(_player.transform.position, _currentFlower.transform.position) > attackRange) return;

            _currentFlower.GetDamage(damage);
        }

        private void SpawnFlower2() {
            if (_currentFlower2 != null || Time.time < _nextSpawnTime2) return;

            _currentFlower2 = Instantiate(lotusFlower2, lotusFlower2.transform.position, Quaternion.identity);
            _currentFlower2.OnAttackEnd += ResetCoolTime2;
            _currentFlower2.gameObject.SetActive(true);
            Vector2 dir = _player.transform.position - _currentFlower2.transform.position;
            _currentFlower2.Initialize(dir, attackSetting2, lotusFlower2.transform.position);
        }

        private void ResetCoolTime2() {
            _nextSpawnTime2 = Time.time + attackSetting2.CoolTime;
            _currentFlower2 = null;
            Debug.Log("연꽃 2 공격 종료, 쿨타임 시작");
        }

        private void ResetCoolTime() {
            _nextSpawnTime = Time.time + attackSetting.CoolTime;
            _currentFlower = null;
            Debug.Log("연꽃 종료, 쿨타임 시작");
        }

        private void OnGUI() {
            GUI.Box(new Rect(15, 15, 330, 150), "Lotus Flower Test");
            GUI.Label(new Rect(30, 45, 300, 25), "WASD / Arrows: Move   |   Left Click: Attack");
            GUI.Label(new Rect(30, 70, 300, 25), "R: Lotus 1   |   T: Lotus 2");
            GUI.Label(new Rect(30, 95, 300, 25), _currentFlower != null
                ? "Lotus 1 Active"
                : "Lotus 1 Cooldown: " + Mathf.Max(0f, _nextSpawnTime - Time.time).ToString("F1"));
            GUI.Label(new Rect(30, 120, 300, 25), _currentFlower2 != null
                ? "Lotus 2 Active"
                : "Lotus 2 Cooldown: " + Mathf.Max(0f, _nextSpawnTime2 - Time.time).ToString("F1"));
        }

        private void OnDestroy() {
            if (_player != null) {
                _player.InputCompo.OnAttackPressed -= TryAttack;
            }
        }
    }
}
