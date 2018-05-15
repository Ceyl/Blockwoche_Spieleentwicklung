using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject timeText;
    [SerializeField] private GameObject winnerText;
    [SerializeField] private GameObject winnerNameText;
    [SerializeField] private GameObject restartButton;

    [SerializeField] private float timeLeft = 300f;
    private bool timeColorChanged;
    private bool gameOver;
    private int minutes;
    private int seconds;

    // Use this for initialization
    void Start () {
        gameOver = false;
        timeColorChanged = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(!gameOver)
            CheckTime();

    }

    private void CheckTime()
    {
        timeLeft -= Time.deltaTime;
        minutes = (int)timeLeft / 60;
        seconds = (int)timeLeft % 60;

        string secondsInString = seconds < 10 ? "0" + seconds : "" + seconds;

        timeText.GetComponent<Text>().text = string.Format("{0}:{1}", minutes, secondsInString);
        if (minutes == 0 && seconds < 10 && !timeColorChanged)
        {
            timeText.GetComponent<Text>().color = Color.red;
            timeColorChanged = true;
        }
        if (minutes == 0 && seconds == 0)
        {
            gameOver = true;
            GameOver();
        }
    }

    private void GameOver()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject winner = players[0];
        for (int i = 1; i < players.Length; i++)
        {
            winner = players[i].transform.position.y > winner.transform.position.y ? players[i] : winner;
        }
        winnerText.SetActive(true);
        winnerNameText.GetComponent<Text>().text = winner.name;
        winnerNameText.SetActive(true);
        restartButton.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
