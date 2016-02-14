using System;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    public class PressAndHold : MinigameScriptBase
    {
        public event Action PressStarted;

        public event Action PressStopped;

        public Collider2D TapObject;

        public bool Holding;

        public float HoldTime;

        public float TimeHeld = 0;

        /// <summary>The actual amount of time that the button needs to be held, calculated from speed.</summary>
        public float ActualHoldTime;

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
            if (Stopped)
                return;

            StartHold();
        }

        protected override void OnStartMinigame()
        {
            TimeHeld = 0;
            ActualHoldTime = HoldTime / StartInfo.SpeedFactor;
        }

        private void TapGestureOnStateChanged(object sender, GestureStateChangeEventArgs e)
        {
            if (Stopped)
                return;

            if (e.State == Gesture.GestureState.Cancelled || e.State == Gesture.GestureState.Ended ||
                     e.State == Gesture.GestureState.Failed)
            {
                EndHold();
            }
        }

        private void EndHold()
        {
            Holding = false;

            if(PressStopped != null)
                PressStopped();
        }

        private void StartHold()
        {
            Holding = true;

            if (PressStarted != null)
                PressStarted();
            CoffeeMaker = SoundKit.instance.playSound(CoffeeMake);
        }

        protected override void OnUnityUpdate()
        {
            if (Holding)
                TimeHeld += Time.deltaTime;

            if (TimeHeld >= ActualHoldTime)
            {
                MarkAsSuccess();
            }
        }

        protected override void CleanUp()
        {
        }
    }
}