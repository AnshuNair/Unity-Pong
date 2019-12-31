using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float ballSpeed;
    public Vector3 direction;
    public GameController gm;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(Random.Range(-30, 45), Random.Range(-30, 45), 0.0f).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * ballSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        float x = direction.x;
        float y = direction.y;
        Vector3 orthogonalVector = collision.contacts[0].point - transform.position;
        float collisionAngle = Vector3.Angle(orthogonalVector, direction);
        if (direction.y >= 0 && direction.x >= 0)
        {
            if (collision.gameObject.tag == "BounceWall")
                direction = new Vector3(Random.Range(15, 60), Random.Range(-60, -15), 0.0f).normalized;
            else if (collision.gameObject.tag == "Racket")
                direction = new Vector3(Random.Range(-60, -15), Random.Range(15, 60), 0.0f).normalized;
        }

        else if (direction.y >= 0 && direction.x <= 0)
        {
            if (collision.gameObject.tag == "BounceWall")
                direction = new Vector3(Random.Range(-60, -15), Random.Range(-60, -15), 0.0f).normalized;
            else if (collision.gameObject.tag == "Racket")
                direction = new Vector3(Random.Range(15, 60), Random.Range(15, 60), 0.0f).normalized;
        }

        else if (direction.y <= 0 && direction.x >= 0)
        {
            if (collision.gameObject.tag == "BounceWall")
                direction = new Vector3(Random.Range(15, 60), Random.Range(15, 60), 0.0f).normalized;
            else if (collision.gameObject.tag == "Racket")
                direction = new Vector3(Random.Range(-60, -15), Random.Range(-60, -15), 0.0f).normalized;
        }

        else
        {
            if (collision.gameObject.tag == "BounceWall")
                direction = new Vector3(Random.Range(-60, -15), Random.Range(15, 60), 0.0f).normalized;
            else if (collision.gameObject.tag == "Racket")
                direction = new Vector3(Random.Range(15, 60), Random.Range(-60, -15), 0.0f).normalized;
        }

        if (collision.gameObject.tag == "GoalWall")
        {
            gm.SpawnBall();
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Racket")
            ballSpeed += 0.5f;
        transform.Translate(direction * ballSpeed * Time.deltaTime);
    }
}
