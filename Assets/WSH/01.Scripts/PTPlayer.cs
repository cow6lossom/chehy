using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WSH._01.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PTPlayer : MonoBehaviour
    {
        private Rigidbody2D _rigid;
        private Vector2 _moveDir;
        
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _currentSpeed;
        [SerializeField] private float _health;

        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigid.linearVelocity = _moveDir * _currentSpeed;
        }
        
        private void OnMove(InputValue value)
        {
            _moveDir = value.Get<Vector2>();
        }

        public void SlowEffect(float amount, float time)
        {
            StartCoroutine(SlowLogic(amount, time));
        }

        IEnumerator SlowLogic(float amount, float time)
        {
            _currentSpeed -= amount/_maxSpeed;
            yield return new WaitForSeconds(time);
            _currentSpeed += amount/_maxSpeed;
        }
    }
}
