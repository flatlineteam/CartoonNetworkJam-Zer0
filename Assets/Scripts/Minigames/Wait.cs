using System;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    public class Wait : MinigameScriptBase
    {
        public Collider2D DontPressCollider;

        public GameObject laserObject;
        public GameObject fireObject;
        public GameObject backgroundObject;
        public Sprite failImage;

        protected override void OnUnityStart()
        {
            DontPressCollider.GetComponent<TapGesture>().Tapped += OnTapped;
        }

        private void OnTapped(object sender, EventArgs eventArgs)
        {
            if (Stopped)
                return;

            backgroundObject.GetComponentInChildren<SpriteRenderer>().sprite = failImage;
            fireObject.SetActive(true);
            laserObject.SetActive(true);
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
            return (int)(Minigame.MaxPointValueForWin * percent);
        }

        protected override void OnTimeElapsed()
        {
            MarkAsSuccess();
        }

        protected override void CleanUp()
        {
        }
    }
}