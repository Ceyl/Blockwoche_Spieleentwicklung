using UnityEngine;

public class LootBox : MonoBehaviour {

    private Animator animator;
    private Camera mainCamera;
    private bool isHit;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
        isHit = false;
	}

    void Update()
    {
        // Check if lootbox has fallen out of camera
        CheckOutOfCamera();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // checking if collison object is player to only interact with player objects
        if (collision.gameObject.tag == "Player" && !isHit)
        {
            isHit = true;
            collision.gameObject.GetComponent<AimController>().AddBomb();
            animator.Play("OpenBox");
            // remove chest after bomb has added properly to player inventory and complete animation has been shown
            Invoke("RemoveChest", 0.5f);
        }
    }

    private void RemoveChest()
    {
        DestroyImmediate(gameObject);
    }

    private void CheckOutOfCamera()
    {
        // remove chest after it has fallen out of cameras y position
        if ((transform.position.y + transform.localScale.y / 2) < mainCamera.transform.position.y - (mainCamera.transform.localScale.y * mainCamera.orthographicSize))
        {
            RemoveChest();
        }
    }
}
