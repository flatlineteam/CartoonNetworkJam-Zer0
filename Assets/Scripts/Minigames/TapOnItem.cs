using System;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>Simple minigame where you have to tap on the target object.</summary>
    public class TapOnItem : MinigameScriptBase
    {
        public Collider2D ItemToTap;
        
        protected override void OnUnityStart()
        {
            ItemToTap.GetComponent<TapGesture>().Tapped += OnTapped;
        }

        private void OnTapped(object sender, EventArgs eventArgs)
        {
            if (Stopped)
                return;

            MarkAsCompleted();
        }

        protected override void OnStartMinigame()
        {
        }

        protected override void OnUnityUpdate()
        {
        }

        protected override void CancelAnyCoroutines()
        {
        }
    }
}