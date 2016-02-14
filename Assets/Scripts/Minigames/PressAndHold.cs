using System;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    public class PressAndHold : MinigameScriptBase
    {
        public Collider2D TapObject;

        public bool Holding;

        public float HoldTime;

        public float TimeHeld = 0;

        public AudioClip CoffeeMake;

        private SoundKit.SKSound CoffeeMaker;

        private float actualHoldTime;
        private TapGesture tapGesture;
        private PressGesture pressGesture;

        protected override void OnUnityStart()
        {
            tapGesture = TapObject.GetComponent<TapGesture>();
            pressGesture = TapObject.GetComponent<PressGesture>();

            tapGesture.StateChanged += TapGestureOnStateChanged;
            pressGesture.Pressed += PressGestureOnPressed;
        }

        private void PressGestureOnPressed(object sender, EventArgs eventArgs)
        {
            StartHold();
        }

        protected override void OnStartMinigame()
        {
            TimeHeld = 0;
            actualHoldTime = HoldTime / StartInfo.SpeedFactor;
        }

        private void TapGestureOnStateChanged(object sender, GestureStateChangeEventArgs e)
        {
            if (e.State == Gesture.GestureState.Cancelled || e.State == Gesture.GestureState.Ended ||
                     e.State == Gesture.GestureState.Failed)
            {
                EndHold();
            }
        }

        private void EndHold()
        {
            Holding = false;
        }

        private void StartHold()
        {
            Holding = true;
            CoffeeMaker = SoundKit.instance.playSound(CoffeeMake);
        }

        protected override void OnUnityUpdate()
        {
            if (Holding)
                TimeHeld += Time.deltaTime;

            if (TimeHeld >= actualHoldTime)
            {
                MarkAsSuccess();
            }
        }

        protected override void CancelAnyCoroutines()
        {
        }
    }
}