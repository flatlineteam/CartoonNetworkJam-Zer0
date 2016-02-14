using System;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>Simple minigame where you have to tap on multiple target objects.</summary>
    public class TapOnItems : MinigameScriptBase
    {

        public Collider2D[] ItemsToTap;

        protected override void OnUnityStart()
        {
        }           

        protected override void OnStartMinigame()
        {
        }

        protected override void OnUnityUpdate()
        {
            if (Stopped)
                return;

            foreach (var item in ItemsToTap)
            {
                if (item.GetComponent<itemTriggerScript>().isSelected == false)
                    return;
  
            }

            MarkAsSuccess();
        }

        protected override void CancelAnyCoroutines()
        {
        }
    }
}