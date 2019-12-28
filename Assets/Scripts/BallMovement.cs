using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public int ballSpeed;
    Vector3 randomDirection;

    // Start is called before the first frame update
    void Start()
    {
        randomDirection = new Vector3(Random.value, Random.value, 0.0f).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(randomDirection * ballSpeed * Time.deltaTime);
    }
}
