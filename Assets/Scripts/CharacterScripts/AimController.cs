using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class AimController : MonoBehaviour {
    public Transform bulletPrefab;
    public Transform bombPrefab;
    public Transform projectile;
    public Vector3 mouseCoords;
    public int fireRate = 2;
    private bool isFiring;
    public float maxDistance = 2f;
    private Transform crosshair;
    private byte bombCounter;
    private Transform bombText;
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        isFiring = false;
        crosshair = transform.GetChild(3);
        crosshair.position = transform.position;
        projectile = bulletPrefab;
        bombCounter = 0;
        bombText = transform.GetChild(4).GetChild(0);
    }
	
	// Update is called once per frame
	void Update () {

        if (bombCounter == 0)
            bombText.gameObject.SetActive(false);
        bombText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 1, 0));
        //Get movement of x- and y-axis.
        float xCoord = Input.GetAxis(gameObject.name + " Mouse X");
        float yCoord = Input.GetAxis(gameObject.name + " Mouse Y");

        Vector3 movement = new Vector3(xCoord, yCoord, 0);

        crosshair.transform.position = Vector2.MoveTowards(crosshair.transform.position, crosshair.transform.position + movement, 0.5f);
        crosshair.transform.localPosition = Vector2.ClampMagnitude(crosshair.transform.localPosition, maxDistance);

        mouseCoords = crosshair.transform.position;
        print(mouseCoords);
        print("x:" + xCoord + "y: " + yCoord);
        
        //Fire raycast if fire button is pressed.
        if ((Input.GetButtonUp(gameObject.name + " Fire1") || Input.GetAxisRaw(gameObject.name + " Fire1") > 0) && !isFiring)
        {
            isFiring = true;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            Vector2 direction = mouseCoords - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mouseCoords - transform.position);
            gameObject.layer = LayerMask.NameToLayer("Player");
            //Shoot the bullet sprite to hitted object.
            Shoot(hit, direction);
        }
        if (Input.GetAxisRaw(gameObject.name + " Fire1") <= 0) isFiring = false;
    }

    //Generate the projectile to shoot in direction of hitted object.
    void Shoot(RaycastHit2D hit, Vector2 direction)
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
        //Get the angle to rotate the projectile.
        float angle = Vector3.Angle(mouseCoords - transform.position, transform.right);
        if (mouseCoords.y < transform.position.y) angle *= -1;

        if (bombCounter > 0)
        {
            projectile = bombPrefab;
            bombCounter--;
            bombText.GetComponent<Text>().text = "Bomb: " + bombCounter;
        }
        else
        {
            projectile = bulletPrefab;
            bombText.gameObject.SetActive(false);
        }

        Transform bulletTrans = Instantiate(projectile, transform.position, Quaternion.AngleAxis(angle, new Vector3(0,0,1)));
        bulletTrans.gameObject.GetComponent<MoveBullet>().destination = destPoint;
        bulletTrans.gameObject.GetComponent<MoveBullet>().destinationObject = destObject;
        bulletTrans.gameObject.GetComponent<MoveBullet>().direction = direction;
        
    }

    public void AddBomb()
    {
        bombCounter += 1;
        bombText.gameObject.SetActive(true);
        bombText.GetComponent<Text>().text = "Bomb" + bombCounter;
    }
}
