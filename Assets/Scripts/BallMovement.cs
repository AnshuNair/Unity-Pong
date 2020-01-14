using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    public Vector3 direction;
    GameController gm;
    Vector3 colliderCenter;
    Collider col;
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        gm = GameObject.Find("GameController").GetComponent<GameController>();
        col = GetComponent<Collider>();
        direction = new Vector3(Random.Range(-60, 60), Random.Range(-60, 60), 0.0f);
        rotateFire(direction.x, direction.y);
        direction = direction.normalized;
        if (gm.ballSpeed > 3)
            ps.transform.localScale = new Vector3(gm.ballSpeed / 6f, gm.ballSpeed / 6f, gm.ballSpeed / 6f);
    }   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * gm.ballSpeed * Time.deltaTime);
    }

    void rotateFire(float x, float y)
    {
        float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;

        if (direction.x > 0)
            ps.transform.rotation = Quaternion.Euler(angle + 90, 90, 0);
        else
            ps.transform.rotation = Quaternion.Euler(450 + angle, 90, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        bool isGameOver = false;
        //gm = GameObject.Find("GameController").GetComponent<GameController>();

        if (collision.gameObject.CompareTag("GoalWall"))
        {
            gm.goalHit.Play();
            gm.lives--;
            gm.livesText.text = gm.lives.ToString();

            if (gm.lives == 0)
            {
                isGameOver = true;
                gm.GameOver.SetActive(true);
                Text goText = gm.GameOver.GetComponent<Text>();
                goText.text = "Game Over! You scored: " + gm.scoreText.text;
                gm.RestartButton.SetActive(true);
                Destroy(this.gameObject);
            }

            if (!isGameOver)
            {
                gm.InvokeSpawnBall();
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
            }
            gm.ballSpeed += 0.5f;
            ps.transform.localScale = new Vector3(gm.ballSpeed / 5f, gm.ballSpeed / 5f, gm.ballSpeed / 5f);
            gm.scoreText.text = ((gm.ballSpeed * 10) - 30).ToString();
        }

        if (collision.gameObject.CompareTag("BounceWall"))
            gm.bounceWall.Play();

        rotateFire(direction.x, direction.y);
        direction = direction.normalized;

        transform.Translate(direction * gm.ballSpeed * Time.deltaTime);
    }
}
