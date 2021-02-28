using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Dropdown playerLives;
    public InputField playerName;
    public Slider carSpeed;
    public Text speedText;
    public Toggle musicToggle;

    public static int lives = 3;
    public static string userName = "Name...";
    public static float speed = 8f;
    public static float fpx;
    public static float fpy;
    public static List<float> cpx;
    public static List<float> cpy;
    public static List<float> crz;
    public static bool savedData = false;
    public static bool isMusic = true;

    private void Start()
    {
        playerLives.value = lives - 1;
        playerName.text = userName;
        carSpeed.value = speed;
        musicToggle.isOn = isMusic;
    }

    public void LivesChange()
    {
        lives = playerLives.value + 1;
    }

    public void NameChange()
    {
        userName = playerName.text;
    }

    public void SpeedChange()
    {
        speed = carSpeed.value;
        speedText.text = "Speed of Cars: " + carSpeed.value;
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level 1");
        Lives.livesCounter = lives;
        Score.currentScore = 0;
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            InitializeGame(save);
        }
        else
        {
            Debug.Log("No saved game detected!");
        }
    }
    public void LoadJSON()
    {
        try
        {
            Save save = JsonUtility.FromJson<Save>(Frog.json);
            InitializeGame(save);
        }
        catch
        {
            Debug.Log("No JSON file detected!");
        }
        
    }

    public void InitializeGame(Save save)
    {
            lives = save.numLives;
            Lives.livesCounter = lives;
            userName = save.userName;
            speed = save.gameSpeed;
            Score.currentScore = save.numScore;
            fpx = save.frogPositionX;
            fpy = save.frogPositionY;
            cpx = save.carPositionsX;
            cpy = save.carPositionsY;
            crz = save.carRotationsZ;
            savedData = true;
            SceneManager.LoadScene("Level 1");
    }

    public void MusicSwitch()
    {
        if (isMusic)
        {
            isMusic = false;
            Debug.Log("Music turned OFF");
        }
        else
        {
            isMusic = true;
            Debug.Log("Music turned ON");
        }
    }
}
