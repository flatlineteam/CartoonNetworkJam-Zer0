using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class ShakeScreenWhileEnabled : MonoBehaviour
    {
        [Range(0, 2)]
        public float Factor = 1.0f;

        public void OnEnable()
        {
            Camera.main.GetComponent<ScreenShake>().ShakeCamera(Factor, TimeSpan.MaxValue);
        }

        public void OnDisable()
        {
            Camera.main.GetComponent<ScreenShake>().StopShaking();
        }

        public void OnDestroy()
        {
            Camera.main.GetComponent<ScreenShake>().StopShaking();
        }
    }
}