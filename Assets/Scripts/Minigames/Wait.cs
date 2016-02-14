using System;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    public class Wait : MinigameScriptBase
    {
        public Collider2D DontPressCollider;

        protected override void OnUnityStart()
        {
            DontPressCollider.GetComponent<TapGesture>().Tapped += OnTapped;
        }

        private void OnTapped(object sender, EventArgs eventArgs)
        {
            MarkAsFailed();
        }

        protected override void OnStartMinigame()
        {
        }

        protected override void OnUnityUpdate()
        {
        }

        public override int CalculateScore(int baseScore)
        {
            var percent = 1 - (TimeRemaining / TimeForMinigame);
            return (int)(Minigame.maxPointValueForWin * percent);
        }

        protected override void OnTimeElapsed()
        {
            MarkAsSuccess();
        }

        protected override void CancelAnyCoroutines()
        {
        }
    }
}