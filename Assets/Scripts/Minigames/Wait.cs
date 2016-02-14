using System;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    public class Wait : MinigameScriptBase
    {
        public Collider2D DontPressCollider;

        public Sprite failImage;

        protected override void OnUnityStart()
        {
            DontPressCollider.GetComponent<TapGesture>().Tapped += OnTapped;
        }

        private void OnTapped(object sender, EventArgs eventArgs)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = failImage;
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