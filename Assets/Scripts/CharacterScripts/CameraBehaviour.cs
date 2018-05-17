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
        // camera shall move up when a player hits the top y border
        if (transform.position.y > mainCamera.transform.position.y + (mainCamera.transform.localScale.y * mainCamera.orthographicSize))
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, (transform.position.y - (mainCamera.transform.localScale.y * mainCamera.orthographicSize)) + 1, mainCamera.transform.position.z);
            if (mainCamera.transform.position.y + (mainCamera.transform.localScale.y * mainCamera.orthographicSize) + 5 > FindObjectOfType<GroundManager>().actualPoint)
            {
                FindObjectOfType<GroundManager>().RandomGround();
            }
        } 
    }

    private void OutOfCamera()
    {
        // check if player is below cameras min y border for removing his character
        if((transform.position.y + transform.localScale.y / 2) < mainCamera.transform.position.y - (mainCamera.transform.localScale.y * mainCamera.orthographicSize))
        {
            FindObjectOfType<GameManager>().CheckForExistingPlayers(gameObject);
        }
    }


}
