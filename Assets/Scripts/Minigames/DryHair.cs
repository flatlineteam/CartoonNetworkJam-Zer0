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

        public float MaxAngleInDegrees = 75;

        private bool? neededHitIsTop;
        public int CountNeeded;

        private int currentCount;
        private bool isTouching;

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
        }

        protected override void OnUnityUpdate()
        {
            if (currentTouchPosition.x > Character.transform.localPosition.x || isTouching == false)
                return;

            var diff = currentTouchPosition - (Vector2)Character.transform.localPosition;

            var angleUnitCircle = Mathf.Atan2(diff.y, diff.x);
            var angleVector = new Vector2(Mathf.Cos(angleUnitCircle), Mathf.Sin(angleUnitCircle));

            var isUp = currentTouchPosition.y > Character.transform.localPosition.y;
            var angle = Vector2.Angle(angleVector, Vector2.left);

            if (!isUp)
                angle = -angle;

            var clamped = Mathf.Clamp(angle, -MaxAngleInDegrees, MaxAngleInDegrees);

            if (Mathf.Approximately(clamped, MaxAngleInDegrees))
            {
                if (neededHitIsTop == null || neededHitIsTop.Value)
                {
                    HitTop();
                }
            }
            else if (Mathf.Approximately(clamped, -MaxAngleInDegrees))
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