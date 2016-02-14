using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

namespace Assets.Scripts
{
    public class itemTriggerScript : MonoBehaviour {

        public bool isSelected;
        public AudioClip BlasterShot;

        public TapOnItems Parent { get; set; }

        // Use this for initialization
        void Start () 
        {
            GetComponent<PressGesture>().Pressed += OnPressed;
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
            isSelected = true;
            GetComponent<SpriteRenderer>().color = Color.green;
        }    
    }
}