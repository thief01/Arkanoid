using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private const string LIFES = "Lifes: ";
    [SerializeField]
    private TextMeshProUGUI lifes;
    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField]
    private TextMeshProUGUI scoreInGameoverScreen;
    [SerializeField]
    private GameObject gameoverScreen;

    void Start()
    {
        GameState.instace.OnLifeChanged += lifes =>
        {
            this.lifes.text = LIFES + lifes.ToString();
        };

        GameState.instace.OnEndGame += () =>
        {
            gameoverScreen.gameObject.SetActive(true);
            scoreInGameoverScreen.text = score.text;
        };

        GameState.instace.OnStartGame += () =>
        {
            gameoverScreen.gameObject.SetActive(false);
        };

        GameState.instace.OnScoreChanged += score =>
        {
            this.score.text = score.ToString();
        };
    }
}
