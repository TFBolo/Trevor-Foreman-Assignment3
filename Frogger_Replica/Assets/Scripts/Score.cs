using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int currentScore = 0;

    public Text scoreText;
    public Text playerName;

    void Start()
    {
        scoreText.text = currentScore.ToString();
        playerName.text = GameManager.userName;
    }
}
