using UnityEngine;

public class LootBox : MonoBehaviour {

    private int content;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
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
}
