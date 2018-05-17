using UnityEngine;

public class LootBoxManager : MonoBehaviour {

    [SerializeField] private GameObject LootBoxPrefab;

    // timer for lootbox spawns
    [SerializeField] private float minLootBoxTimer = 10;
    [SerializeField] private float maxLootBoxTimer = 15;
    private float timeLeft;

    private Camera mainCamera;
    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
        timeLeft = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

        // counter for next lootbox spawn
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            SpawnLootBox();
            timeLeft = Random.Range(minLootBoxTimer, maxLootBoxTimer);
        }
    }

    private void SpawnLootBox()
    {
        // get border values of camera
        float minYValue = -(mainCamera.transform.position.y + (mainCamera.transform.localScale.y * mainCamera.orthographicSize));
        float maxYValue = -minYValue;

        float minXValue = -(((mainCamera.transform.localScale.x * mainCamera.orthographicSize) - (mainCamera.transform.localScale.x * mainCamera.orthographicSize) * 0.1f) * 2);
        float maxXValue = -minXValue;

        float yValue = Random.Range(minYValue, maxYValue);
        float xValue;

        // collider for checking if there is no other object at the actual position
        Collider2D collider = null;

        // chances to cancel the spawntries if there is no possibility to spawn a lootbox at this y value
        int chances = 50;

        while (chances > 0)
        {
            // check if position is free
            xValue = Random.Range(minXValue, maxXValue);
            collider = Physics2D.OverlapBox(new Vector2(xValue, yValue), new Vector2(LootBoxPrefab.transform.localScale.x - 1, LootBoxPrefab.transform.localScale.y - 1), 0);
            if (collider == null)
            {
                Instantiate(LootBoxPrefab, new Vector3(xValue, yValue, 0), Quaternion.identity);
                break;
            }
            else
                chances--;
        }
    }


}
