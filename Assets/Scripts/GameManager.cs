using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject timeText;
    [SerializeField] private GameObject winnerText;

    [SerializeField] private float timeLeft = 300f;
    private bool gameOver;
    private int minutes;
    private int seconds;

    // Use this for initialization
    void Start () {
        gameOver = false;
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
    }
}
