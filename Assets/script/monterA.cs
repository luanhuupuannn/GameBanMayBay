﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class monterA : MonoBehaviour
{
    public GameObject hieuungtrungdon;
    public GameObject hieuungtrungdon2;


    public GameObject enegry;
    public GameObject coin;
    public Transform addenegry;
    public int raceenegry;
    
    Animator animator;

    public GameObject bullet;
    public Transform fireadd;
    public float speedfire = 10;

    private Vector2 targetPosition;
    public float moveSpeed;
    public int hp = 10;
    private bool hasFired; // Flag to track if bullets have been fired




    public int dame;


   // public float Timehp = 15;


    float timebullet = 10f;


   public AudioSource audioSource;
   public AudioClip clipdie;
    public AudioClip clipfiremonter;


    private void Start()
    {

        dame = PlayerPrefs.GetInt("damage");
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        targetPosition = new Vector2(Random.Range(-2.48f, 2.45f), Random.Range(8.67f, 5.5f));
        // ram dome tỷ lệ ra êngry
       raceenegry = Random.Range(1, 5);



        hasFired = false; // Initialize flag
    }








    void Update()
    {
        dame = PlayerPrefs.GetInt("damage");

        //Timehp -= Time.deltaTime;
        // if (Timehp < 0)
        // {
        //     hp += 5;
        //  Timehp = 10f;
        //  }
        timebullet -= Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f && !hasFired)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
                Firebullet();
               timebullet = 5f;
            
            hasFired = true; // Set flag to prevent further firing
        }


        if (timebullet < 0 )
        {
            exit();

        }



    }







    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bulletplayer")
        {
            hp -= 2;
            GameObject trungdon = Instantiate(hieuungtrungdon,
            this.transform.position, this.transform.rotation);
            Destroy(trungdon, 0.5f);
            if (hp <= 0)
            {


                audioSource.PlayOneShot(clipdie);
                StartCoroutine(DelayedDestroy());  // Start the coroutine for delayed destruction


            }
        }
        else if (collision.gameObject.tag == "bulletplayer2")
        {
            hp -= 4;
            GameObject trungdon = Instantiate(hieuungtrungdon,
            this.transform.position, this.transform.rotation);
            Destroy(trungdon, 0.5f);
            if (hp <= 0)
            {


                audioSource.PlayOneShot(clipdie);
                StartCoroutine(DelayedDestroy());  // Start the coroutine for delayed destruction


            }
        }
        // player2
        else if (collision.gameObject.tag == "blplayer2")
        {
            hp -= dame;
            GameObject trungdon = Instantiate(hieuungtrungdon2,
            this.transform.position, this.transform.rotation);
            Destroy(trungdon, 0.5f);
            if (hp <= 0)
            {


                audioSource.PlayOneShot(clipdie);
                StartCoroutine(DelayedDestroy());  // Start the coroutine for delayed destruction


            }
        }


        else if (collision.gameObject.tag == "bulletROBOT")
        {
            hp = 0;
            GameObject trungdon = Instantiate(hieuungtrungdon,
            this.transform.position, this.transform.rotation);
            Destroy(trungdon, 0.5f);
            if (hp <= 0)
            {


                audioSource.PlayOneShot(clipdie);
                StartCoroutine(DelayedDestroy());  // Start the coroutine for delayed destruction


            }
        }


        else if (collision.gameObject.tag == "bulletROBOT2")
        {
            hp = 4;
            GameObject trungdon = Instantiate(hieuungtrungdon,
            this.transform.position, this.transform.rotation);
            Destroy(trungdon, 0.5f);
            if (hp <= 0)
            {
                audioSource.PlayOneShot(clipdie);
                StartCoroutine(DelayedDestroy());  // Start the coroutine for delayed destruction
            }
        }



    }
    //độ trễ của destroy
    IEnumerator DelayedDestroy()
    {  
        animator.SetBool("desm", true);  // Set the animation parameter immediately
        yield return new WaitForSeconds(0.8f);  // Wait for 1 second
        if (raceenegry <= 3)
        {
            GameObject bulletInstance = Instantiate(coin, addenegry.position, addenegry.rotation);
        } 
        // chọn tí lệ mong muốn 
        if (raceenegry >= 4)
        {
            //  tạo ra enegry
            GameObject bulletInstance = Instantiate(enegry, addenegry.position, addenegry.rotation);
        }
        Destroy(this.gameObject);  // Destroy the game object after the delay
    }

  


    void Firebullet()
    {
        audioSource.PlayOneShot(clipfiremonter);
        // Create a bullet instance
        GameObject bulletInstance = Instantiate(bullet, fireadd.position, fireadd.rotation);

        // Set initial bullet velocity
        Rigidbody2D bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = Vector2.down * speedfire; // Fire downwards
    }

    public void exit()
    {
        targetPosition = new Vector2(0,-10);

       if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(this.gameObject);

        }


    }


}
