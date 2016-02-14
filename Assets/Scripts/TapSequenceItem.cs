using System;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    public class TapSequenceItem : MonoBehaviour
    {
        public PressGesture PressGesture;

        public event Action Tapped;

        public void Start()
        {
            PressGesture.Pressed += PressGestureOnPressed;            
        }

        private void PressGestureOnPressed(object sender, EventArgs eventArgs)
        {
            if (Tapped != null)
                Tapped();
        }
    }
}