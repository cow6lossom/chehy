using Tyu.Scripts.Modules;
using UnityEngine;

namespace Tyu.Scripts.Test
{
    public class InteractTestet : MonoBehaviour
    {
        [SerializeField] private InteractModule itMd;

        [ContextMenu("adfasdf")]
        public void Dfdsa() => itMd.TryInteract();
    }
}