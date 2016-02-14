using System;
using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

namespace Assets.Scripts
{
    public class Swipe : MinigameScriptBase 
    {
        public AudioClip ScreenSwipe;
        private SoundKit.SKSound FingerSwipe;
        public Collider2D FlickCollider;
        protected override void OnUnityStart()
        {            
            FlickCollider.GetComponent<FlickGesture>().Flicked += OnFlicked;
        }

        private void OnFlicked(object sender, EventArgs eventArgs)
        {
            if (Stopped)
                return;

            FingerSwipe = SoundKit.instance.playSound(ScreenSwipe);

            MarkAsSuccess();
        }

        protected override void OnStartMinigame()
        {            
        }

        protected override void OnUnityUpdate()
        {            
        }

        protected override void CleanUp()
        {            
        }
    }
}
