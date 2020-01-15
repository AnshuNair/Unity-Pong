using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public Text highScoreText;
    public GameObject GameOverUI;
    public GameObject GameOverText;
    public int lives;
    public float ballSpeed;
    public GameObject ball;
    public AudioSource bounceWall;
    public AudioSource goalHit;
    public AudioSource racketBoost;
    public AudioSource racketHit;
    bool isPaused = false;
    public bool isGameOver = true;

    void Awake()
    {
        SpawnBall();
        lives = 5;
        livesText.text = lives.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            
            if (isPaused)
            {
                Text goText = GameOverText.GetComponent<Text>();
                goText.text = "";
                GameOverUI.SetActive(false);
                Time.timeScale = 1;
            }

            else
            {
                Time.timeScale = 0;
                GameOverUI.SetActive(true);
                Text goText = GameOverText.GetComponent<Text>();
                goText.text = "Paused";
            }

            isPaused = !isPaused;
        }
    }

    public void SpawnBall()
    {
        Instantiate(ball, new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
    }

    public void GoToLevel(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
