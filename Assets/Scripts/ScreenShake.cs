using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Camera))]
    public class ScreenShake : MonoBehaviour
    {
        private float startShakeTime;
        private Vector3 cameraBasePosition;

        private Coroutine shakeCoroutine;

        public void Awake()
        {
            cameraBasePosition = transform.localPosition;
        }

        public void ShakeCamera(float factor, TimeSpan duration)
        {
            if (shakeCoroutine != null)
                StopCoroutine(shakeCoroutine);

            startShakeTime = Time.time;
            shakeCoroutine = StartCoroutine(ShakeCoroutine(factor, duration));
        }

        public void StopShaking()
        {
            if (shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
                transform.localPosition = cameraBasePosition;
            }
        }

        private IEnumerator ShakeCoroutine(float factor, TimeSpan duration)
        {
            while (true)
            {
                var elapsed = TimeSpan.FromSeconds(Time.time - startShakeTime);
                var percentThrough = (float)elapsed.TotalMilliseconds / (float)duration.TotalMilliseconds;

                if (percentThrough >= 1)
                {
                    transform.localPosition = cameraBasePosition;
                    break;
                }

                var amount = factor * (1.0f - percentThrough);
                var angle = Random.Range(0, Mathf.PI * 2);

                var movement = new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0) * amount;

                transform.localPosition = cameraBasePosition + movement;

                yield return null;
            }
        }
    }
}