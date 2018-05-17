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
        CheckOutOfCamera();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isHit)
        {
            isHit = true;
            collision.gameObject.GetComponent<AimController>().AddBomb();
            animator.Play("OpenBox");
            Invoke("RemoveChest", 0.5f);
        }
    }

    private void RemoveChest()
    {
        DestroyImmediate(gameObject);
    }

    private void CheckOutOfCamera()
    {
        if ((transform.position.y + transform.localScale.y / 2) < mainCamera.transform.position.y - (mainCamera.transform.localScale.y * mainCamera.orthographicSize))
        {
            RemoveChest();
        }
    }
}
