using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class ShakeScreenWhenSpawned : MonoBehaviour
    {
        public bool ShakeOnStart = true;

        public bool ShakeOnEnable = false;

        public float Time = 1f;

        public float Factor = 1f;

        public void Start()
        {
            if (ShakeOnStart)
                Shake();
        }

        public void OnEnable()
        {
            if (ShakeOnEnable)
                Shake();
        }

        private void Shake()
        {
            Camera.main.GetComponent<ScreenShake>().ShakeCamera(Factor, TimeSpan.FromSeconds(Time));
        }
    }
}