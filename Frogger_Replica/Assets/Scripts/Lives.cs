using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public static int livesCounter = GameManager.lives;

    public Text livesText;

    void Start()
    {
        livesText.text = "Lives: " + livesCounter;
    }
}
