using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

namespace Assets.Scripts
{
    public class itemTriggerScript : MonoBehaviour {

        public bool isSelected;

    	// Use this for initialization
    	void Start () 
        {
            GetComponent<PressGesture>().Pressed += OnPressed;
    	}

        private void OnPressed (object s, System.EventArgs e)
        {
            RegisterClick();
        }   

        private void RegisterClick()
        {
            isSelected = true;
            GetComponent<SpriteRenderer>().color = Color.green;
        }    
    }
}