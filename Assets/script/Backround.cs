﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backround : MonoBehaviour
{


    public float speed = 1.0f; // Tốc độ di chuyển background


    private void Start()
    {
        Time.timeScale = 1;

    }

    private void Update()
    {
        // Di chuyển background xuống
        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);

        // Reset vị trí background khi di chuyển ra khỏi màn hình
        if (transform.position.y < -10.34)
        {
            transform.position = new Vector3(-0.026f, 29.2498f);
        }
    }

}
