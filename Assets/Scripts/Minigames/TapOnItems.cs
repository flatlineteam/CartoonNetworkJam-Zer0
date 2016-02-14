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
            foreach (var item in ItemsToTap)
            {
                item.GetComponent<itemTriggerScript>().Parent = this;
            }
        }

        protected override void OnUnityUpdate()
        {
            foreach (var item in ItemsToTap)
            {
                if (item.GetComponent<itemTriggerScript>().isSelected == false)
                    return;
            }

            MarkAsSuccess();
        }

        protected override void CleanUp()
        {
        }
    }
}