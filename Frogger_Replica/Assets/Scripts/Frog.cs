using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Frog : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject carStuff;
    public static string json;

    void Start()
    {
        isPaused = false;
        if (GameManager.savedData)
        {
            for (int i = 0; i < GameManager.cpx.Count; i++)
            {
                Instantiate(carStuff, new Vector3(GameManager.cpx[i], GameManager.cpy[i], 0f), new Quaternion(0f, 0f, GameManager.crz[i], 0f));
            }
            gameObject.transform.position = new Vector3(GameManager.fpx, GameManager.fpy, 0f);
            GameManager.savedData = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && rb.transform.position.x != 6)
        {
            rb.MovePosition(rb.position + Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && rb.transform.position.x != -6)
        {
            rb.MovePosition(rb.position + Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.MovePosition(rb.position + Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && rb.transform.position.y != -4)
        {
            rb.MovePosition(rb.position + Vector2.down);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Car")
        {
            Debug.Log("We Lost!");
            Lives.livesCounter -= 1;
            //Score.currentScore = 0;
            if (Lives.livesCounter <= 0)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Exit");
            }
            else
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void PauseAndContinue()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseMenu.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadScene("Intro");
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("Car");
        foreach(GameObject carGameObject in objs)
        {
            save.carPositionsX.Add(carGameObject.transform.position.x);
            save.carPositionsY.Add(carGameObject.transform.position.y);
            save.carRotationsZ.Add(carGameObject.transform.rotation.z);
        }
        save.numLives = Lives.livesCounter;
        save.numScore = Score.currentScore;
        save.userName = GameManager.userName;
        save.gameSpeed = GameManager.speed;
        save.frogPositionX = gameObject.transform.position.x;
        save.frogPositionY = gameObject.transform.position.y;

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
    }
}
