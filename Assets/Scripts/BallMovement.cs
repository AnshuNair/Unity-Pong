using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BallMovement : MonoBehaviour
{
    public Vector3 direction;
    GameController gm;
    Vector3 colliderCenter;
    Collider col;
    ParticleSystem ps;
    bool movable = false;
    int highScore;
    string path;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "\\highScore.txt";
        ps = GetComponentInChildren<ParticleSystem>();
        gm = GameObject.Find("GameController").GetComponent<GameController>();
        col = GetComponent<Collider>();
        direction = new Vector3(Random.Range(-60, 60), Random.Range(-60, 60), 0.0f);
        RotateFire(direction.x, direction.y);
        direction = direction.normalized;
        if (gm.ballSpeed > 3)
            ps.transform.localScale = new Vector3(gm.ballSpeed / 6f, gm.ballSpeed / 6f, gm.ballSpeed / 6f);
        StartCoroutine("WaitAfterSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (movable)
            transform.Translate(direction * gm.ballSpeed * Time.deltaTime);
    }

    void RotateFire(float x, float y)
    {
        float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;

        if (direction.x > 0)
            ps.transform.rotation = Quaternion.Euler(angle + 90, 90, 0);
        else
            ps.transform.rotation = Quaternion.Euler(450 + angle, 90, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GoalWall"))
        {
            gm.goalHit.Play();
            gm.lives--;
            gm.livesText.text = gm.lives.ToString();

            if (gm.lives == 0)
            {
                gm.isGameOver = true;
                gm.GameOverUI.SetActive(true);
                Text goText = gm.GameOverText.GetComponent<Text>();
                goText.text = "Game Over! You scored: " + gm.scoreText.text;
                
                if (File.Exists(path))
                {
                    StreamReader reader = new StreamReader(path);
                    string line = reader.ReadLine();
                    string[] strArray = line.Split(',');
                    highScore = int.Parse(strArray[1]);
                    reader.Close();
                    if (((int)(gm.ballSpeed * 10) - 30) > highScore)
                    {
                        highScore = ((int)(gm.ballSpeed * 10) - 30);
                        string serializedData = "HighScore," + highScore.ToString();
                        var file = File.Create(path);
                        file.Close();
                        StreamWriter writer = new StreamWriter(path, true);
                        writer.Write(serializedData);
                        writer.Close();
                    }
                }
                else
                {
                    highScore = ((int)(gm.ballSpeed * 10) - 30);
                    var file = File.Create(path);
                    file.Close();
                    string serializedData = "HighScore," + highScore.ToString();
                    StreamWriter writer = new StreamWriter(path, true);
                    writer.Write(serializedData);
                    writer.Close();
                }

                Text hSText = gm.highScoreText.GetComponent<Text>();
                hSText.text = "HighScore: " + highScore;
                Time.timeScale = 0;
                Destroy(this.gameObject);
            }

            if (!gm.isGameOver)
            {
                gm.SpawnBall();
                Destroy(this.gameObject);
            }
        }

        if (direction.y >= 0 && direction.x >= 0)
        {
            if (collision.gameObject.CompareTag("BounceWall"))
                direction = new Vector3(Random.Range(15, 60), Random.Range(-60, -15), 0.0f);
            else if (collision.gameObject.CompareTag("Racket"))
                direction = new Vector3(Random.Range(-60, -15), Random.Range(15, 60), 0.0f);
        }

        else if (direction.y >= 0 && direction.x <= 0)
        {
            if (collision.gameObject.CompareTag("BounceWall"))
                direction = new Vector3(Random.Range(-60, -15), Random.Range(-60, -15), 0.0f);
            else if (collision.gameObject.CompareTag("Racket"))
                direction = new Vector3(Random.Range(15, 60), Random.Range(15, 60), 0.0f);
        }


        else if (direction.y <= 0 && direction.x >= 0)
        {
            if (collision.gameObject.CompareTag("BounceWall"))
                direction = new Vector3(Random.Range(15, 60), Random.Range(15, 60), 0.0f);
            else if (collision.gameObject.CompareTag("Racket"))
                direction = new Vector3(Random.Range(-60, -15), Random.Range(-60, -15), 0.0f);
        }

        else
        {
            if (collision.gameObject.CompareTag("BounceWall"))
                direction = new Vector3(Random.Range(-60, -15), Random.Range(15, 60), 0.0f);
            else if (collision.gameObject.CompareTag("Racket"))
                direction = new Vector3(Random.Range(15, 60), Random.Range(-60, -15), 0.0f);
        }

        if (collision.gameObject.CompareTag("Racket"))
        {
            gm.racketHit.Play();
            colliderCenter = col.bounds.center;
            float vertDist = collision.transform.position.y - colliderCenter.y;
            if (Mathf.Abs(vertDist) <= .15)
            {
                gm.racketBoost.Play();
                collision.transform.GetChild(1).gameObject.SetActive(true);
                collision.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                RacketMovement rm = collision.gameObject.GetComponent<RacketMovement>();
                rm.moveSpeed += 0.5f;
                rm.colorIndicator += 0.2f;
                rm.gameObject.GetComponent<Renderer>().material.color = Color.blue * rm.colorIndicator;

            }
            gm.ballSpeed += 0.5f;
            if (gm.ballSpeed < 12)
                ps.transform.localScale = new Vector3(gm.ballSpeed / 5f, gm.ballSpeed / 5f, gm.ballSpeed / 5f);
            gm.scoreText.text = ((gm.ballSpeed * 10) - 30).ToString();
        }

        if (collision.gameObject.CompareTag("BounceWall"))
            gm.bounceWall.Play();

        RotateFire(direction.x, direction.y);
        direction = direction.normalized;

        transform.Translate(direction * gm.ballSpeed * Time.deltaTime);
    }

    IEnumerator WaitAfterSpawn()
    {
        yield return new WaitForSeconds(2);
        movable = true;
    }
}
