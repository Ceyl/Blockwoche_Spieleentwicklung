using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    private Camera mainCamera;
	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
	}
	
    void Update ()
    {
        MoveCamera();
        OutOfCamera();
    }

    private void MoveCamera()
    {
        if (transform.position.y > mainCamera.transform.position.y + (mainCamera.transform.localScale.y * mainCamera.orthographicSize))
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y - (mainCamera.transform.localScale.y * mainCamera.orthographicSize), mainCamera.transform.position.z);
        }
    }

    private void OutOfCamera()
    {
        if((transform.position.y + transform.localScale.y / 2) < mainCamera.transform.position.y - (mainCamera.transform.localScale.y * mainCamera.orthographicSize))
        {
            FindObjectOfType<GameManager>().CheckForExistingPlayers(gameObject);
        }
    }


}
