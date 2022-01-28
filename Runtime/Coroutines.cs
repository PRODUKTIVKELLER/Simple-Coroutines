using System;
using System.Collections;
using UnityEngine;

namespace Produktivkeller.SimpleCoroutines
{
    public class Coroutines
    {
        private const float DEFAULT_ANIMATION_DURATION_IN_SECONDS = 1.0f;

        private static IEnumerator AsynchronousLerpValue<T>(Action<T>           updateAction, T startValue, T targetValue, Func<T, T, float, T> lerpFunction,
                                                            float               animationTime       = DEFAULT_ANIMATION_DURATION_IN_SECONDS,
                                                            MappingFunctionType mappingFunctionType = MappingFunctionType.Ease,
                                                            bool                useRealTime         = false)
        {
            float timeInSecondsWhenAnimationBegan  = useRealTime ? Time.realtimeSinceStartup : Time.time;
            float timeInSecondsSinceAnimationBegan = 0;

            IMappingFunction mappingFunction = DetermineMappingFunction(mappingFunctionType);

            updateAction(startValue);

            while (timeInSecondsSinceAnimationBegan < animationTime)
            {
                float valueBetween0And1 = timeInSecondsSinceAnimationBegan / animationTime;
                float lerpProgress      = mappingFunction.MapToValueBetween0And1(valueBetween0And1);
                updateAction(lerpFunction(startValue, targetValue, lerpProgress));

                yield return null;
                timeInSecondsSinceAnimationBegan = (useRealTime ? Time.realtimeSinceStartup : Time.time) - timeInSecondsWhenAnimationBegan;
            }

            updateAction(targetValue);
        }

        public static IEnumerator AsynchronousLerpValue(Action<Quaternion> updateAction, Quaternion startValue, Quaternion targetValue,
                                                        float              animationTime = DEFAULT_ANIMATION_DURATION_IN_SECONDS)
        {
            return AsynchronousLerpValue(updateAction, startValue, targetValue, Quaternion.Lerp, animationTime);
        }

        public static IEnumerator AsynchronousLerpValue(Action<Vector3>     updateAction, Vector3 startValue, Vector3 targetValue,
                                                        float               animationTime       = DEFAULT_ANIMATION_DURATION_IN_SECONDS,
                                                        MappingFunctionType mappingFunctionType = MappingFunctionType.Ease)
        {
            return AsynchronousLerpValue(updateAction, startValue, targetValue, Vector3.Lerp, animationTime, mappingFunctionType);
        }

        public static IEnumerator AsynchronousLerpValue(Action<Vector2>     updateAction, Vector2 startValue, Vector2 targetValue,
                                                        float               animationTime       = DEFAULT_ANIMATION_DURATION_IN_SECONDS,
                                                        MappingFunctionType mappingFunctionType = MappingFunctionType.Ease)
        {
            return AsynchronousLerpValue(updateAction, startValue, targetValue, Vector2.Lerp, animationTime, mappingFunctionType);
        }

        public static IEnumerator AsynchronousLerpValue(Action<float>       updateAction, float startValue, float targetValue,
                                                        float               animationTime       = DEFAULT_ANIMATION_DURATION_IN_SECONDS,
                                                        MappingFunctionType mappingFunctionType = MappingFunctionType.Ease)
        {
            return AsynchronousLerpValue(updateAction, startValue, targetValue, Mathf.Lerp, animationTime, mappingFunctionType, Time.timeScale == 0);
        }

        public static IEnumerator AsynchronousLerpValue(Action<int>         updateAction, int startValue, int targetValue,
                                                        float               animationTime       = DEFAULT_ANIMATION_DURATION_IN_SECONDS,
                                                        MappingFunctionType mappingFunctionType = MappingFunctionType.Ease)
        {
            return AsynchronousLerpValue(updateAction, startValue, targetValue, IntegerLerp, animationTime, mappingFunctionType);
        }

        public static IEnumerator AsynchronousLerpValue(Action<Color>       updateAction, Color startValue, Color targetValue,
                                                        float               animationTime       = DEFAULT_ANIMATION_DURATION_IN_SECONDS,
                                                        MappingFunctionType mappingFunctionType = MappingFunctionType.Ease)
        {
            return AsynchronousLerpValue(updateAction, startValue, targetValue, Color.Lerp, animationTime, mappingFunctionType);
        }

        private static int IntegerLerp(int a, int b, float lerp)
        {
            return (int)Mathf.Lerp(a, b, lerp);
        }

        private static IMappingFunction DetermineMappingFunction(MappingFunctionType mappingFunctionType)
        {
            if (mappingFunctionType == MappingFunctionType.Linear)
            {
                return new Linear();
            }

            if (mappingFunctionType == MappingFunctionType.Ease)
            {
                return new Ease();
            }

            if (mappingFunctionType == MappingFunctionType.EaseIn)
            {
                return new EaseIn();
            }

            return new EaseOut();
        }

        public static IEnumerator InvokeAction(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }
    }
}