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
                (Vector2)transform.position + (Vector2)(transform.right * settingSo.Distance),
                Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z),
                Vector3.one
            );
            Gizmos.DrawWireCube(Vector2.zero, settingSo.Size);

            Gizmos.matrix = basicMatrix;
        }
    }
}