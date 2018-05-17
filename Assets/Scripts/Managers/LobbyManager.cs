using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour {

    [SerializeField] private GameObject dropDownPlayers;

    private void Start()
    {
        GameStats.players = 2;
    }

    public void ChangePlayerAmount()
    {
        // get input for player numbers
        int dropDownValue = dropDownPlayers.GetComponent<Dropdown>().value;
        GameStats.players = Int32.Parse(dropDownPlayers.GetComponent<Dropdown>().options[dropDownValue].text.Substring(0,1));
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
