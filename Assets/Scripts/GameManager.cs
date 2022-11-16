using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] inventory player;
    [SerializeField] TMP_Text coinText;
    [SerializeField] GameObject tutupEnd;


    private void Start()
    {
        gameOverPanel.SetActive(false);
        tutupEnd.SetActive(true);
    }

    private void Update()
    {
        coinText.text = "Coin : " + player.coin + " / 3";


        if (player.coin >= 3)
        {
            tutupEnd.SetActive(false);
        }
    }

    public void BackToMainMenu()
    {
        SceneLoader.Load("MainMenu");
    }

    public void Replay()
    {
        SceneLoader.ReloadLevel();
    }

    public void PlayNext()
    {
        SceneLoader.LoadNextLevel();
    }
}
