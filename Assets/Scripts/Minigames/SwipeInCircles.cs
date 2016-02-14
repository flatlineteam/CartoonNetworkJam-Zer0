using System;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    public class SwipeInCircles : MinigameScriptBase
    {
        public Collider2D TapArea;

        public Transform Target;

        public float DegreesToRotate = 720;

        public ParticleSystem ParticleSystem;

        public int ParticleCount;

        private PressGesture pressGesture;
        private TransformGesture transformGesture;
        private ReleaseGesture releaseGesture;

        private Vector2 tapStartPosition;
        private Vector2 tapCurrentPosition;
        private Vector2 lastTapPosition;

        private bool isPressed;
        private float angleAccumulator;
        
        protected override void OnUnityStart()
        {
            pressGesture = TapArea.GetComponent<PressGesture>();
            transformGesture = TapArea.GetComponent<TransformGesture>();
            releaseGesture = TapArea.GetComponent<ReleaseGesture>();

            pressGesture.Pressed += PressGestureOnPressed;
            releaseGesture.Released += ReleaseGestureOnReleased;
            transformGesture.StateChanged += (o, e) => { if(e.State == Gesture.GestureState.Changed) TransformChanged(); };
        }

        private void PressGestureOnPressed(object sender, EventArgs eventArgs)
        {
            if (Stopped)
                return;

            isPressed = true;

            tapStartPosition = pressGesture.ActiveTouches[0].Hit.Point;
            tapCurrentPosition = lastTapPosition = tapStartPosition;
        }

        private void TransformChanged()
        {
            if (Stopped)
                return;

            var delta = transformGesture.LocalDeltaPosition;
            lastTapPosition = tapCurrentPosition;
            tapCurrentPosition += (Vector2)delta;
        }

        private void ReleaseGestureOnReleased(object sender, EventArgs eventArgs)
        {
            if (Stopped)
                return;

            isPressed = false;
        }

        protected override void OnStartMinigame()
        {
        }

        protected override void OnUnityUpdate()
        {
            if (isPressed == false)
                return;

            var currentVector = tapCurrentPosition - (Vector2)Target.transform.localPosition;
            var lastVector = lastTapPosition - (Vector2)Target.transform.localPosition;

            var currentAngle = Mathf.Rad2Deg * Mathf.Atan2(currentVector.y, currentVector.x);
            var lastAngle = Mathf.Rad2Deg * Mathf.Atan2(lastVector.y, lastVector.x);

            var angleChange = currentAngle - lastAngle;

            angleAccumulator += Mathf.Clamp(angleChange, -30, 30);
            lastTapPosition = tapCurrentPosition; // We've used lastTapPosition so reset it
            
            if (DegreesToRotate < 0)
            {
                if (angleChange < 1)
                {
                    if (ParticleSystem != null)
                        ParticleSystem.Emit(ParticleCount);
                }

                if(angleAccumulator <= DegreesToRotate)
                    MarkAsSuccess();
            }
            else if (DegreesToRotate > 0)
            {
                if (angleChange > 1)
                {
                    if (ParticleSystem != null)
                        ParticleSystem.Emit(ParticleCount);
                }

                if(angleAccumulator >= DegreesToRotate)
                    MarkAsSuccess();
            }

            Target.transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
        }
        
        protected override void CleanUp()
        {
        }
    }
}