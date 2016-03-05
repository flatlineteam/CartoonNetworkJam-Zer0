using System;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    public class DryHair : MinigameScriptBase
    {
        public Collider2D TapArea;

        public Transform ArmPivot;

        public AudioClip HairDry;

        private SoundKit.SKSound HairDryer;

        private FlickGesture flickGesture;

        public float MaxAngle;
        public float MinAngle;
        public float SecondsToAngle;

        public GameObject GoUp;
        public GameObject GoDown;

        public int CountNeeded;

        private int currentCount;
        private float currentAngle;
        private float currentTimer;
        private bool IsAtMax;

        protected override void OnUnityStart()
        {
            flickGesture = TapArea.GetComponent<FlickGesture>();

            flickGesture.Flicked += OnFlicked;

            HairDryer = SoundKit.instance.playSound(HairDry);
        }         

        private void OnFlicked(object sender, EventArgs eventArgs)
        {
            if (Stopped)
                return;

            Hit();
        }          

        protected override void OnStartMinigame()
        {
            currentCount = 0;
            currentAngle = Mathf.Clamp(0, MinAngle, MaxAngle);
            GoUp.SetActive(false);
            GoDown.SetActive(true);
        }

        protected override void OnUnityUpdate()
        {
            if(IsAtMax)currentTimer += (Time.deltaTime / SecondsToAngle) * StartInfo.SpeedFactor;
            else currentTimer -= Time.deltaTime / SecondsToAngle;

            if(currentTimer >= 1.0f) currentTimer = 1.0f;
            if(currentTimer <= 0.0f) currentTimer = 0.0f;

            currentAngle = MaxAngle + currentTimer * (MinAngle - MaxAngle);

            ArmPivot.transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
        }

        private void Hit()
        {
            IsAtMax = !IsAtMax;

            GoUp.SetActive(IsAtMax);
            GoDown.SetActive(!IsAtMax);

            currentCount++;

            if (currentCount == CountNeeded)
            {
                MarkAsSuccess();
                HairDryer.stop();

                currentAngle = MaxAngle + 0.5f * (MinAngle - MaxAngle);

                ArmPivot.transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);

                GoUp.SetActive(false);
                GoDown.SetActive(false);
            }
        }            

        protected override void CleanUp()
        {
        }
    }
}