using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LobbyManager : MonoBehaviour {

    [SerializeField] private GameObject dropDownPlayers;

    private void Start()
    {
        GameStats.players = 2;
    }

    public void ChangePlayerAmount()
    {
        int dropDownValue = dropDownPlayers.GetComponent<Dropdown>().value;
        GameStats.players = Int32.Parse(dropDownPlayers.GetComponent<Dropdown>().options[dropDownValue].text.Substring(0,1));
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Holger");
    }
}
