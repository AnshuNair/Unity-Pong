using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public GameObject RestartButton;
    public GameObject GameOver;
    public int score;
    public int lives;
    public float ballSpeed;
    public GameObject ball;
    public AudioSource bounceWall;
    public AudioSource goalHit;
    public AudioSource racketBoost;
    public AudioSource racketHit;

    void Awake()
    {
        SpawnBall();
        lives = 5;
        livesText.text = lives.ToString();
    }

    public void SpawnBall()
    {
        GameObject newBall = Instantiate(ball, new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
    }

}
