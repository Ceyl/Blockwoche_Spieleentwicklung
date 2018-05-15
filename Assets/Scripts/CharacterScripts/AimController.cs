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
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        isFiring = false;
        //mouseCoords = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
        Transform crosshair = gameObject.transform.GetChild(3);
        float xCoord = Input.GetAxis(gameObject.name + " Mouse X");
        float yCoord = Input.GetAxis(gameObject.name + " Mouse Y");

            Vector3 movement = new Vector3(xCoord, yCoord, 0);
            
            crosshair.transform.position = Vector2.MoveTowards(crosshair.transform.position, crosshair.transform.position + movement, 0.5f);
        mouseCoords = crosshair.transform.position;
        print(mouseCoords);
        print("x:" + xCoord + "y: " + yCoord);
        
        if ((Input.GetButtonUp(gameObject.name + " Fire1") || Input.GetAxisRaw(gameObject.name + " Fire1") > 0) && !isFiring)
        {
            isFiring = true;
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mouseCoords - transform.position);
            
            Shoot(hit);
            if (hit.transform != null)
            {
                if(hit.transform.GetComponent<Health>() != null)hit.transform.GetComponent<Health>().AddDamage(50);
            }
            Debug.DrawLine(transform.position, (mouseCoords-transform.position) *100);
        }
        if (Input.GetAxisRaw(gameObject.name + " Fire1") <= 0) isFiring = false;
    }

    void Shoot(RaycastHit2D hit)
    {
        Vector3 destPoint;
        if (hit.transform != null)
        {
            destPoint = hit.transform.position;
        } else destPoint = new Vector3(100,100,100);
        float angle = Vector3.Angle(mouseCoords - transform.position, transform.right);
        if (mouseCoords.y < transform.position.y) angle *= -1;
        
        Transform bulletTrans = Instantiate(bullet, transform.position, Quaternion.AngleAxis(angle, new Vector3(0,0,1)));
        bulletTrans.gameObject.GetComponent<MoveBullet>().destination = destPoint;
    }
}
