using Tyu.Scripts.SO;
using UnityEngine;

namespace Tyu.Scripts.Test
{
    public class AttackRangeViewer : MonoBehaviour
    {
        [SerializeField] private AttackSettingSO settingSo;
        [SerializeField] private bool debug;

        private void OnDrawGizmos()
        {
            if (settingSo == null) return;
            if (!debug) return;
            Gizmos.color = Color.red;
            Matrix4x4 basicMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(
                (Vector2)transform.position,
                Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z),
                Vector3.one
            );

            var isRight = transform.right.x > 0;
            Gizmos.DrawWireCube(Vector2.zero + settingSo.Pos * new Vector2(isRight ? 1 :-1, 1), settingSo.Size);

            Gizmos.matrix = basicMatrix;
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, settingSo.minDetect);
            Gizmos.color = Color.gold;
            Gizmos.DrawWireSphere(transform.position, settingSo.maxDetect);
        }
    }
}