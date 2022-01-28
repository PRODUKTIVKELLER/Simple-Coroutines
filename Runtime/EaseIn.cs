using UnityEngine;

namespace Produktivkeller.SimpleCoroutines
{
    internal class EaseIn : IMappingFunction
    {
        private const float X_OFFSET  = Mathf.PI;
        private const float PERIOD    = Mathf.PI * 0.5f;
        private const float AMPLITUDE = 1f;

        public float MapToValueBetween0And1(float valueBetween0And1)
        {
            return (Mathf.Cos(X_OFFSET + PERIOD * valueBetween0And1) + 1f) * (1f / AMPLITUDE);
        }
    }
}