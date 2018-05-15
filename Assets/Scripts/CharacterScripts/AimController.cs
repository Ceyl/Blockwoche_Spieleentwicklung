using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {

    public Vector3 mouseCoords;
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        GameObject crosshair = GameObject.Find("CrossHair");
        mouseCoords = Input.mousePosition;
        mouseCoords = Camera.main.ScreenToWorldPoint(mouseCoords);
        crosshair.transform.position = Vector2.Lerp(transform.position, mouseCoords,100);

    }
}
