using UnityEngine;

public class LootBox : MonoBehaviour {

    private enum Content { Gun, Bomb}
    private Animator animator;
    private Camera mainCamera;
    private Content boxContent;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
        boxContent = (Content)Random.Range((int)Content.Gun, (int)Content.Bomb);
	}

    void Update()
    {
        CheckOutOfCamera();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
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
