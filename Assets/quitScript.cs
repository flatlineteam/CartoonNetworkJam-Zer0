using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class quitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<TapGesture>().Tapped += (sender, e) => Application.Quit();
	}
}
