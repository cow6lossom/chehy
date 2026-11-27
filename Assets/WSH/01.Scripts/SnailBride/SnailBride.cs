using UnityEngine;

namespace WSH._01.Scripts.SnailBride
{
    public class SnailBride : MonoBehaviour
    {
        [SerializeField] private bool _secondPage;

        private void Update()
        {
            
            if (!_secondPage)
                return;
        }

        void ChangePage()
        {
            _secondPage = true;
        }
    }
}
