using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int numLives;
    public int numScore;
    public string userName;
    public float gameSpeed;

    public List<float> carPositionsX = new List<float>();
    public List<float> carPositionsY = new List<float>();
    public List<float> carRotationsZ = new List<float>();
    
    public float frogPositionX;
    public float frogPositionY;
}
