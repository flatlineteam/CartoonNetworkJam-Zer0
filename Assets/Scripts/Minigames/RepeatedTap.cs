using UnityEngine;
using TouchScript.Gestures;
using System.Collections;

namespace Assets.Scripts
{
    public class RepeatedTap : MinigameScriptBase {
        public Collider2D repeatedTapCollider;
        public int numberOfTapsToSuccess;

        private int RepeatedTapCount = 0;
        protected override void OnUnityStart()
        {            
            repeatedTapCollider.GetComponent<TapGesture>().Tapped += OnTapped;
        }

        void OnTapped (object sender, System.EventArgs e)
        {
            RepeatedTapCount++;

            if (RepeatedTapCount >= numberOfTapsToSuccess)
            {
                MarkAsSuccess();
            }
        }         

        protected override void OnStartMinigame()
        {            
        }

        protected override void OnUnityUpdate()
        {            
        }

        protected override void OnTimeElapsed()
        {
            MarkAsFailed();
        }

        protected override void CancelAnyCoroutines()
        {            
        }
    }
}
