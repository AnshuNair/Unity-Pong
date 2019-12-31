using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text playerOneScore;
    public Text playerTwoScore;
    public GameObject GameOver;
    public int scoreOne;
    public int scoreTwo;
    public float ballSpeed;
    public GameObject ball;

    void Awake()
    {
        SpawnBall();
    }

    void Start()
    {
        scoreOne = 0;
        scoreTwo = 0;
        playerOneScore.text = scoreOne.ToString();
        playerTwoScore.text = scoreTwo.ToString();
        ballSpeed = 3.0f;
        Text goText = GameOver.GetComponent<Text>();
        goText.text = "";
        GameOver.SetActive(false);
    }

    public void SpawnBall()
    {
        GameObject newBall = Instantiate(ball, new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
        BallMovement bm = newBall.GetComponent<BallMovement>();
        bm.direction = new Vector3(Random.Range(-30, 45), Random.Range(-30, 45), 0.0f).normalized;
        bm.transform.Translate(bm.direction * ballSpeed * Time.deltaTime);
    }

    public IEnumerator restartGame(KeyCode keyCode)
    {
        while (!Input.GetKeyDown(keyCode))
        {
            Debug.Log("Inside the restart coroutine");
            yield return null;
        }

        Debug.Log("User Pressed R!");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
