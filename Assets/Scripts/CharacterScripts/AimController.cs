using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AimController : MonoBehaviour {
    public Transform bullet;
    public Vector3 mouseCoords;
    public int fireRate = 2;
    float timeToFire = 0;
    private bool isFiring;
    public float maxDistance = 10;
    private Transform crosshair;
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        isFiring = false;
        crosshair = gameObject.transform.GetChild(3);
        crosshair.position = transform.position;
        //mouseCoords = Input.mousePosition;
    }
	
	// Update is called once per frame
	void Update () {
        
        float xCoord = Input.GetAxis(gameObject.name + " Mouse X");
        float yCoord = Input.GetAxis(gameObject.name + " Mouse Y");

            Vector3 movement = new Vector3(xCoord, yCoord, 0);
        if (Vector3.Distance(crosshair.transform.position + movement, transform.position) < maxDistance)
        {
            crosshair.transform.position = Vector2.MoveTowards(crosshair.transform.position, crosshair.transform.position + movement, 1f);
        }
        mouseCoords = crosshair.transform.position;
        print(mouseCoords);
        print("x:" + xCoord + "y: " + yCoord);
        
        if ((Input.GetButtonUp(gameObject.name + " Fire1") || Input.GetAxisRaw(gameObject.name + " Fire1") > 0) && !isFiring)
        {
            isFiring = true;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mouseCoords - transform.position);
            gameObject.layer = LayerMask.NameToLayer("Player");
            Shoot(hit);
            if (hit.transform != null)
            {

            }
            Debug.DrawLine(transform.position, (mouseCoords-transform.position) *100);
        }
        if (Input.GetAxisRaw(gameObject.name + " Fire1") <= 0) isFiring = false;
    }

    void Shoot(RaycastHit2D hit)
    {
        Vector3 destPoint;
        Transform destObject;
        if (hit.transform != null )
        {
            destPoint = hit.point;
            destObject = hit.transform;
        }
        else
        {
            destPoint = new Vector3(100, 100, 100);
            destObject = null;
        }
        float angle = Vector3.Angle(mouseCoords - transform.position, transform.right);
        if (mouseCoords.y < transform.position.y) angle *= -1;
        
        Transform bulletTrans = Instantiate(bullet, transform.position, Quaternion.AngleAxis(angle, new Vector3(0,0,1)));
        bulletTrans.gameObject.GetComponent<MoveBullet>().destination = destPoint;
        bulletTrans.gameObject.GetComponent<MoveBullet>().destinationObject = destObject;
    }
}
