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
        direction = new Vector3(Random.value, Random.value, 0.0f).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * ballSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 orthogonalVector = collision.contacts[0].point - transform.position;
        float collisionAngle = Vector3.Angle(orthogonalVector, direction);
        Debug.Log("Collision Angle: " + collisionAngle);
    }
}
