﻿using UnityEngine;

public class RacketMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float colorIndicator = 0.1f;

    void Update()
    {
        if (transform.position.x > 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                transform.Translate((new Vector3(0, moveSpeed, 0)) * Time.deltaTime);

            if (Input.GetKey(KeyCode.DownArrow))
                transform.Translate((new Vector3(0, -moveSpeed, 0)) * Time.deltaTime);
        }

        if (transform.position.x < 0)
        {
            if (Input.GetKey(KeyCode.W))
                transform.Translate((new Vector3(0, moveSpeed, 0)) * Time.deltaTime);

            if (Input.GetKey(KeyCode.S))
                transform.Translate((new Vector3(0, -moveSpeed, 0)) * Time.deltaTime);
        }

    }
}
