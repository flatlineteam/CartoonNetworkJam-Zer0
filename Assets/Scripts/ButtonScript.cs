using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TouchScript.Gestures;

public class ButtonScript : MonoBehaviour {
    public string sceneName;

	// Use this for initialization
	void Start () {
        GetComponent<TapGesture>().Tapped += (sender, e) => SceneManager.LoadScene(sceneName);
	}
}
