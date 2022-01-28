using UnityEngine;

namespace Produktivkeller.SimpleCoroutines
{
    internal class Ease : IMappingFunction
    {
        public float MapToValueBetween0And1(float valueBetween0And1)
        {
            return (Mathf.Cos(Mathf.PI * valueBetween0And1 + Mathf.PI) + 1f) * 0.5f;
        }
    }
}