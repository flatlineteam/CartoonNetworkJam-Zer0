using System;
using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

namespace Assets.Scripts
{
    public class Swipe : MinigameScriptBase 
    {
        public Collider2D FlickCollider;
        protected override void OnUnityStart()
        {            
            FlickCollider.GetComponent<FlickGesture>().Flicked += OnFlicked;
        }

        private void OnFlicked(object sender, EventArgs eventArgs)
        {
            if (Stopped)
                return;
            
            MarkAsSuccess();
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
