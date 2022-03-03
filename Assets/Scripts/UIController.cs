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
            if (lifes <= 0)
            {
                gameoverScreen.gameObject.SetActive(true);
                scoreInGameoverScreen.text = score.text;
            }
        };

        GameState.instace.OnScoreChanged += score =>
        {
            this.score.text = score.ToString();
        };
    }
}
