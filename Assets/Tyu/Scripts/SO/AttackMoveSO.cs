using PrimeTween;
using UnityEngine;

namespace Tyu.Scripts.SO
{
    [CreateAssetMenu(fileName = "AttackMoveTweenSetting", menuName = "DataSetting/Attack/TweenSetting", order = 0)]
    public class AttackMoveSO : ScriptableObject
    {
        [field:SerializeField] public TweenSettings<Vector2> Setting { get; private set; }
        [field:SerializeField] public float Distance { get; private set; }
        [field:SerializeField] public AttackEnum Type { get; private set; }
    }
}