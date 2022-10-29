using UnityEngine;
using UnityEngine.Events;

namespace UTS
{
    /// <summary>
    /// Unity event with parameter of the type Verctor3
    /// </summary>
    [System.Serializable]
    public class OnPositionEvent : UnityEvent<Vector3>
    {
    }
}