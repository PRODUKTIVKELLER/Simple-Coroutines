using UnityEngine;

namespace Produktivkeller.SimpleCoroutines
{
    internal class EaseOut : IMappingFunction
    {
        private const float X_OFFSET = 1.5f * Mathf.PI;
        private const float PERIOD   = Mathf.PI * 0.5f;

        public float MapToValueBetween0And1(float valueBetween0And1)
        {
            return Mathf.Cos(X_OFFSET + PERIOD * valueBetween0And1);
        }
    }
}
