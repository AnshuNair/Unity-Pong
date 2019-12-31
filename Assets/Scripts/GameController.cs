using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject ball;

    public void SpawnBall()
    {
        GameObject newBall = Instantiate(ball, new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
        BallMovement bm = newBall.GetComponent<BallMovement>();
        bm.direction = new Vector3(Random.Range(-30, 45), Random.Range(-30, 45), 0.0f).normalized;
        bm.ballSpeed = 3;
        bm.transform.Translate(bm.direction * bm.ballSpeed * Time.deltaTime);
    }
}
