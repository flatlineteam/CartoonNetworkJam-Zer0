using UnityEngine;
using System.Collections;

public class ScreenSnapper : MonoBehaviour {

    public bool xNeg = false;
    public bool xPos = false;

    public bool yNeg = false;
    public bool yPos = false;

    private float HalfScreenWidth;
    private float HalfScreenHeight;

	// Use this for initialization
	void Start () {
        HalfScreenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        HalfScreenHeight = HalfScreenWidth * ((float)Screen.height / Screen.width);

        var horzSnap = 0;
        var vertSnap = 0;

        if(xNeg) horzSnap = -1;
        if(xPos) horzSnap = 1;

        if(yNeg) vertSnap = -1;
        if(yPos) vertSnap = 1;

        var newPosition = transform.position;

        if(horzSnap != 0)
            newPosition.x = horzSnap * HalfScreenWidth;
        if(vertSnap != 0)
            newPosition.y = vertSnap * HalfScreenHeight;

        transform.position = newPosition;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
