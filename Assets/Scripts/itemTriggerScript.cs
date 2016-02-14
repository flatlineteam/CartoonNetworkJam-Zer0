using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

namespace Assets.Scripts
{
    public class itemTriggerScript : MonoBehaviour {

        public bool IsSelected;
        public AudioClip BlasterShot;

        public TapOnItems Parent { get; set; }

        public PressGesture GestureObj;

        public void Awake()
        {
            GestureObj.Pressed += OnPressed;
        }

        private void OnPressed (object s, System.EventArgs e)
        {
            if (Parent.Stopped)
                return;
            RegisterClick();
        }   

        private void RegisterClick()
        {
            SoundKit.instance.playOneShot(BlasterShot);
            IsSelected = true;
            Destroy(gameObject);
        }    
    }
}