using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text playerOneScore;
    public Text playerTwoScore;
    public GameObject RestartButton;
    public GameObject GameOver;
    public int scoreOne;
    public int scoreTwo;
    public float ballSpeed;
    public GameObject ball;

    void Awake()
    {
        SpawnBall();
    }

    public void SpawnBall()
    {
        GameObject newBall = Instantiate(ball, new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
    }

}
