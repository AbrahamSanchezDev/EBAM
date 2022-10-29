using UnityEngine;
using UnityEngine.Events;

namespace UTS
{
    [System.Serializable]
    public class SwipeDirectionRealTimeEvent : UnityEvent<Vector2, Vector2>
    {
    }
}