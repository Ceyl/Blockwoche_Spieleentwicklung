using UnityEngine;

public class LootBoxManager : MonoBehaviour {

    [SerializeField] private GameObject LootBoxPrefab;
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

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            SpawnLootBox();
            timeLeft = Random.Range(minLootBoxTimer, maxLootBoxTimer);
        }
    }

    private void SpawnLootBox()
    {
        float minYValue = -(mainCamera.transform.position.y + (mainCamera.transform.localScale.y * mainCamera.orthographicSize));
        float maxYValue = -minYValue;

        float minXValue = -(((mainCamera.transform.localScale.x * mainCamera.orthographicSize) - (mainCamera.transform.localScale.x * mainCamera.orthographicSize) * 0.1f) * 2);
        float maxXValue = -minXValue;

        float yValue = Random.Range(minYValue, maxYValue);
        float xValue;

        Collider2D collider = null;
        int chances = 50;

        while (chances > 0)
        {
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
