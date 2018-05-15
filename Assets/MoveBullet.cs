using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour {

    public float moveSpeed = 100;
    public Vector3 destination;
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * Time.deltaTime* moveSpeed);
        if (destination == new Vector3(100, 100, 100))
        {
            Destroy(gameObject, 5f);
        }
        else
            if(Mathf.Abs(transform.position.x) > Mathf.Abs(destination.x) && Mathf.Abs(transform.position.y) > Mathf.Abs(destination.y))
        {
            Destroy(gameObject);
        }
	}


}
