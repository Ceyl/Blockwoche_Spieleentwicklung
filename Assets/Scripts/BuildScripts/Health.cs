using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    private float health;
	// Use this for initialization
	void Start () {

	}
    private void Awake()
    {
        float size = transform.localScale.x * transform.localScale.y;

if(size == 2)
        {
            health = 50;
        }else if(size == 4)
        {
            health = 100;
        }else if(size == 8)
        {
            health = 150;
        }else
        {
            health = 100;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void AddDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}
