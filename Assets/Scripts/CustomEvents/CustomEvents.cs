using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CustomEvents
{
    [System.Serializable]
    public class Vector3Event : UnityEvent<Vector3>
    {
        public Vector3 parameter;
    }


    [System.Serializable]
    public class Vector2Event : UnityEvent<Vector2>
    {
        public Vector2 parameter;
    }
}