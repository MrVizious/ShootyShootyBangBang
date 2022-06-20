using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class Extensions
    {
        public static float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
        {
            value = Mathf.Clamp(value, fromSource, toSource);
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }
    }
}