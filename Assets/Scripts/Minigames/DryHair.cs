using System;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    public class DryHair : MinigameScriptBase
    {
        public Transform Character;

        public Collider2D TapArea;

        public Transform ArmPivot;

        public AudioClip HairDry;

        private SoundKit.SKSound HairDryer;
        
        private TransformGesture transformGesture;
        private PressGesture pressGesture;
        private ReleaseGesture releaseGesture;

        private Vector2 startTouchPosition;
        private Vector2 currentTouchPosition;

        public float MaxAngle;
        public float MinAngle;

        public GameObject GoUp;
        public GameObject GoDown;

        [Range(0, 10)]
        public float Sensitivity = 1;

        private bool? neededHitIsTop;
        public int CountNeeded;

        private int currentCount;
        private bool isTouching;
        private float currentAngle;

        protected override void OnUnityStart()
        {
            transformGesture = TapArea.GetComponent<TransformGesture>();
            pressGesture = TapArea.GetComponent<PressGesture>();
            releaseGesture = TapArea.GetComponent<ReleaseGesture>();

            transformGesture.StateChanged += TransformGestureOnStateChanged;
            pressGesture.Pressed += Pressed;
            releaseGesture.Released += Released;

            HairDryer = SoundKit.instance.playSound(HairDry);
        }

        private void Released(object sender, EventArgs eventArgs)
        {
            if (Stopped)
                return;

            isTouching = false;
        }

        private void Pressed(object sender, EventArgs eventArgs)
        {
            if (Stopped)
                return;

            startTouchPosition = pressGesture.ActiveTouches[0].Hit.Point;
            currentTouchPosition = startTouchPosition;
            isTouching = true;
        }

        private void TransformGestureOnStateChanged(object sender, GestureStateChangeEventArgs e)
        {
            if (Stopped)
                return;

            if (e.State == Gesture.GestureState.Changed)
            {
                currentTouchPosition += (Vector2)transformGesture.LocalDeltaPosition;
            }
        }

        protected override void OnStartMinigame()
        {
            currentCount = 0;
            isTouching = false;
            currentAngle = Mathf.Clamp(0, MinAngle, MaxAngle);
            GoUp.SetActive(false);
            GoDown.SetActive(false);
        }

        protected override void OnUnityUpdate()
        {
            var delta = transformGesture.LocalDeltaPosition;

            var change = delta.y * Sensitivity;

            currentAngle += change;

            var clamped = Mathf.Clamp(currentAngle, MinAngle, MaxAngle);

            if (Mathf.Approximately(clamped, MaxAngle))
            {
                if (neededHitIsTop == null || neededHitIsTop.Value)
                {
                    HitTop();
                }
            }
            else if (Mathf.Approximately(clamped, MinAngle))
            {
                if (neededHitIsTop == null || neededHitIsTop.Value == false)
                {
                    HitBottom();
                }
            }

            ArmPivot.transform.rotation = Quaternion.AngleAxis(clamped, Vector3.forward);
        }

        private void HitTop()
        {
            currentCount++;
            neededHitIsTop = false;

            GoUp.SetActive(false);
            GoDown.SetActive(true);

            if (currentCount == CountNeeded)
            {
                MarkAsSuccess();
                HairDryer.stop();
            }
        }

        private void HitBottom()
        {
            currentCount++;
            neededHitIsTop = true;

            GoUp.SetActive(true);
            GoDown.SetActive(false);

            if (currentCount == CountNeeded)
            {
                MarkAsSuccess();
                HairDryer.stop();
            }
        }

        protected override void CleanUp()
        {
        }
    }
}