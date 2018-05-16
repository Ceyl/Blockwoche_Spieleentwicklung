using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject timeText;
    [SerializeField] private GameObject winnerText;
    [SerializeField] private GameObject winnerNameText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private float timeLeft = 300f;
    [Range(2, 4)] [SerializeField] private int playerCount; 
    private bool timeColorChanged;
    private bool gameOver;
    private bool gameRunning;
    private int minutes;
    private int seconds;
    private Camera mainCamera;
    private Color[] colors = new Color[] {Color.blue, Color.red, Color.green, Color.yellow };

    // Use this for initialization
    void Start () {
        playerCount = GameStats.players;
        gameRunning = true;
        gameOver = false;
        timeColorChanged = false;
        mainCamera = Camera.main;

        // calculate distance to spawn chars in same distance to each other
        float distance = (2 * (mainCamera.orthographicSize - mainCamera.orthographicSize * 0.1f)) / playerCount;
        float tmpDistance = -(mainCamera.orthographicSize - mainCamera.orthographicSize * 0.1f) + distance / 2;

        // spawn players on map
        for (int i = 0; i < playerCount; i++)
        {
            GameObject player = Instantiate(playerPrefab, new Vector3(tmpDistance, playerPrefab.transform.position.y, 0), Quaternion.identity);
            player.name = string.Format("Player{0}", i+1);
            player.GetComponent<SpriteRenderer>().color = colors[i];
            tmpDistance += distance;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameRunning)
        {
            if (!gameOver)
                CheckTime();
            else
                GameOver();
        }
    }

    private void CheckTime()
    {
        // calculate time for time text
        timeLeft -= Time.deltaTime;
        minutes = (int)timeLeft / 60;
        seconds = (int)timeLeft % 60;

        string secondsInString = seconds < 10 ? "0" + seconds : "" + seconds;
        timeText.GetComponent<Text>().text = string.Format("{0}:{1}", minutes, secondsInString);

        // check if time is nearly over to color time text
        if (minutes == 0 && seconds < 10 && !timeColorChanged)
        {
            timeText.GetComponent<Text>().color = Color.red;
            timeColorChanged = true;
        }

        // check if time is over to calculate winner
        if (minutes == 0 && seconds == 0)
        {
            gameOver = true;
        }
    }

    private void GameOver()
    {
        // get all player objects which are still in scene
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject winner = new GameObject();
        winner.name = "Nobody";
        if (players.Length > 0)
            winner = players[0];

        // check players y position to calculate winner
        for (int i = 1; i < players.Length; i++)
        {
            winner = players[i].transform.position.y > winner.transform.position.y ? players[i] : winner;
        }
        winnerText.SetActive(true);
        winnerNameText.GetComponent<Text>().text = winner.name;
        winnerNameText.SetActive(true);
        restartButton.SetActive(true);
        gameRunning = false;
    }

    public void Restart()
    {
        // restart level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CheckForExistingPlayers(GameObject player)
    {
        // destroys player if the character is completely outside of the camera
        DestroyImmediate(player);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        gameOver = players.Length == 1 ? true : false;
    }
}
