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

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameController").GetComponent<GameController>();
        direction = new Vector3(Random.Range(-30, 45), Random.Range(-30, 45), 0.0f).normalized;
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * gm.ballSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        bool isGameOver = false;
        //gm = GameObject.Find("GameController").GetComponent<GameController>();

        if (collision.gameObject.tag == "GoalWall")
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

        float x = direction.x;
        float y = direction.y;
        Vector3 orthogonalVector = collision.contacts[0].point - transform.position;
        float collisionAngle = Vector3.Angle(orthogonalVector, direction);
        if (direction.y >= 0 && direction.x >= 0)
        {
            if (collision.gameObject.tag == "BounceWall")
            {
                gm.bounceWall.Play();
                direction = new Vector3(Random.Range(15, 60), Random.Range(-60, -15), 0.0f).normalized;
            }

            else if (collision.gameObject.tag == "Racket")
            {
                gm.racketHit.Play();
                direction = new Vector3(Random.Range(-60, -15), Random.Range(15, 60), 0.0f).normalized;
            }
        }

        else if (direction.y >= 0 && direction.x <= 0)
        {
            if (collision.gameObject.tag == "BounceWall")
            {
                gm.bounceWall.Play();
                direction = new Vector3(Random.Range(-60, -15), Random.Range(-60, -15), 0.0f).normalized;
            }

            else if (collision.gameObject.tag == "Racket")
            {
                gm.racketHit.Play();
                direction = new Vector3(Random.Range(15, 60), Random.Range(15, 60), 0.0f).normalized;
            }
        }


        else if (direction.y <= 0 && direction.x >= 0)
        {
            if (collision.gameObject.tag == "BounceWall")
            {
                gm.bounceWall.Play();
                direction = new Vector3(Random.Range(15, 60), Random.Range(15, 60), 0.0f).normalized;
            }

            else if (collision.gameObject.tag == "Racket")
            {
                gm.racketHit.Play();
                direction = new Vector3(Random.Range(-60, -15), Random.Range(-60, -15), 0.0f).normalized;
            }

        }

        else
        {
            if (collision.gameObject.tag == "BounceWall")
            {
                gm.bounceWall.Play();
                direction = new Vector3(Random.Range(-60, -15), Random.Range(15, 60), 0.0f).normalized;
            }

            else if (collision.gameObject.tag == "Racket")
            {
                gm.racketHit.Play();
                direction = new Vector3(Random.Range(15, 60), Random.Range(-60, -15), 0.0f).normalized;
            }

        }

        if (collision.gameObject.tag == "Racket")
        {
            colliderCenter = col.bounds.center;
            ContactPoint contact = collision.contacts[0];
            float vertDist = collision.transform.position.y - colliderCenter.y;
            if (Mathf.Abs(vertDist) <= .15)
            {
                gm.racketBoost.Play();
                RacketMovement rm = collision.gameObject.GetComponent<RacketMovement>();
                rm.moveSpeed += 0.5f;
            }
            gm.ballSpeed += 0.5f;
            gm.scoreText.text = ((gm.ballSpeed * 10) - 30).ToString();
        }

        transform.Translate(direction * gm.ballSpeed * Time.deltaTime);
    }
}
