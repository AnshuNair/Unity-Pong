using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public int ballSpeed;
    Vector3 direction;

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
        Vector3 orthogonalVector = collision.contacts[0].point - transform.position;
        float collisionAngle = Vector3.Angle(orthogonalVector, direction);
        Debug.Log("OLD Direction: " + direction);
        if (direction.y >= 0 && direction.x >= 0)
        {
            direction = new Vector3(Random.Range(30, 45) + x, Random.Range(-45, -30), 0.0f).normalized;
            Debug.Log("+,+");
        }

        else if (direction.y >= 0 && direction.x <= 0)
        {
            direction = new Vector3(Random.Range(-45, -30) - x, Random.Range(-45, -30), 0.0f).normalized;
            Debug.Log("-,+");
        }

        else if (direction.y <= 0 && direction.x >= 0)
        {
            direction = new Vector3(Random.Range(30, 45) + x, Random.Range(30, 45), 0.0f).normalized;
            Debug.Log("+,-");
        }

        else
        {
            direction = new Vector3(Random.Range(-45, -30) - x, Random.Range(30, 45), 0.0f).normalized;
            Debug.Log("-,-");
        }

        Debug.Log("NEW Direction: " + direction);
        transform.Translate(direction * ballSpeed * Time.deltaTime);
    }
}
