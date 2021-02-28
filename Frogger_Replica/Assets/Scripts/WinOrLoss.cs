using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinOrLoss : MonoBehaviour
{
    public Text win;
    public Text loss;
    public Text scoreAndLives;

    void Start()
    {
        if (Score.currentScore >= 500)
        {
            win.enabled = true;
            loss.enabled = false;
        }
        else
        {
            win.enabled = false;
            loss.enabled = true;
        }
        scoreAndLives.text = "Using " + GameManager.lives + " lives, you got a score of " + Score.currentScore;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
