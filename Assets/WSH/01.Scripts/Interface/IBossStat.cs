using UnityEngine;

namespace WSH._01.Scripts.Interface
{
    public interface IBossStat
    {
        public float Damage { get; set; }
        public float Range { get; set; }
        
        public float NoticeTime { get; set; }
        public float DealTime { get; set; }
    }
}