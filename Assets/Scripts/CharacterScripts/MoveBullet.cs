using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class MoveBullet : MonoBehaviour {

    public float moveSpeed = 100;
    public bool bomb;
    public float damage;
    public float force;
    public Vector2 direction;
    public Transform destinationObject;
    public Vector3 destination;
	// Update is called once per frame
	void Update () {

        if (destinationObject == null)
        {
            //if there is no object hitted, destroy bullet after some time so it isn´t in scene forever.
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            Destroy(gameObject, 5f);
        }
        else
            transform.position = Vector2.MoveTowards(transform.position, destination, 1f);
            if(transform.position == destination)
        {
            if (bomb)
            {
                //check radius of bomb to affect it by explosion.
                Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 8f);
                foreach (Collider2D col in collider)
                {
                    float dist = Vector2.Distance(col.transform.position, transform.position);
                    float bombDamage;
                    //decrease bomb damage with size of radius.
                    if (dist < 1) bombDamage = damage;
                    else bombDamage = damage / dist;
                    if(col.gameObject.layer == LayerMask.NameToLayer("Effectable") || col.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        //Add explosion force to objects depending on radius.
                        ExplosionForce(col, col.GetComponent<Rigidbody2D>(), force, transform.position, 8f, 0.05f);
                    }
                    if (col.GetComponent<Health>() != null)
                    {
                        col.GetComponent<Health>().AddDamage(bombDamage);                       
                    }
                }
            }
            else
            {
                if (destinationObject.GetComponent<Health>() != null)
                {
                    destinationObject.GetComponent<Health>().AddDamage(damage);
                }
                else if (destinationObject.GetComponent<BuildController>() != null)
                {
                    //Player has no health so he just gets knocked back from normal hits.
                    destinationObject.GetComponent<BuildController>().HitPlayer(direction, force);
                }
            }
            Destroy(gameObject);
        }
	}
    //Method for adding a explosion force to rigid body.
    private static void ExplosionForce( Collider2D hit, Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        var dir = (hit.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir * explosionForce * wearoff;
        body.AddForce(baseForce);

        float upliftWearoff = 1 - upliftModifier / explosionRadius;
        Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
        body.AddForce(upliftForce);
    }
}
