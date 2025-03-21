﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


using static System.Net.Mime.MediaTypeNames;



public class player : MonoBehaviour
{

    public GameObject laze;
      public GameObject checkrobot;
    GameObject butllettmonter;
    GameObject butllettmonter2;
    //public UnityEngine.UI.Text textcoin;

    public GameObject gameover;

    public GameObject bullet;
    public GameObject bullet2;

    public Transform fireadd1;
    public Transform fireadd2;
    public float TimeFire = 1f;
    public float speedfire = 10;
    private float timefire;


    private bool leverupActive = false;
    private bool leverupActive2 = false;

    public GameObject bulletlvup;
    public GameObject bulletlvup2;

    public thanhmau thanhmau;
    public float nowhp;
    public float maxhp = 10;
    Animator animator;




     public AudioSource br;

    public AudioSource audioSource;
    public AudioClip Clipdie;
    public AudioClip cliplvup;



    public Camera otherCamera; // Tham chiếu đến camera trong cảnh này
    private void Start()
    {


        Time.timeScale = 1;

        float loadedSize = PlayerPrefs.GetFloat("SavedCameraSize", -1f); // -1f là giá trị mặc định nếu không được lưu
        if (loadedSize > 0)
        {
            otherCamera.orthographicSize = loadedSize; // Áp dụng kích thước đã lưu cho camera
        }


        audioSource = GetComponent<AudioSource>();


        bullet.SetActive(true);
        bullet2.SetActive(true);
        Time.timeScale = 1;
        
        // cập nhật thanh máu 
        nowhp = maxhp;
        thanhmau.capnhatthanhmau(nowhp,maxhp);

        animator = GetComponent<Animator>();

      
    }

    void Update()
    {


        if (nowhp <= 0)
        {
            gameover.SetActive(true);
            Time.timeScale = 0;
        }

        // đây là laze học thêm 
        RaycastHit2D hit = Physics2D.Raycast(laze.transform.position, Vector2.down);
        Debug.DrawRay(laze.transform.position, Vector2.down * hit.distance, Color.red);
        /// hit.distance

        if (checkrobot.activeInHierarchy)
        {
            leverupActive = false;
            leverupActive2 = false;
            bullet.SetActive(false);
            bullet2.SetActive(false);
            animator.SetBool("ROBOT", true);

        }
        else
        {
            bullet.SetActive(true);
            bullet2.SetActive(true);
            animator.SetBool("ROBOT", false);
        }




        timefire -= Time.deltaTime;

        if (Input.GetKey(KeyCode.L))
        {
            animator.SetBool("uplv", true);
            Invoke("tatuplv", 10f);
        }

        if (timefire < 0)
        {

            Firebullet();

        }

      






        //  if (Input.GetMouseButton(0))
        //  {
        // mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mouse = new Vector3(Mathf.Clamp(mouse.x, -4, 4), Mathf.Clamp(mouse.y, -3, 9), 0);

        // }
        //transform.position = Vector3.Lerp(transform.position, mouse, movespeed* Time.deltaTime);




    }

    void Firebullet()
    {
        timefire = TimeFire;
        // Sử dụng bulletlvup nếu leverup đang hoạt động, nếu không sử dụng bullet 
        GameObject bulletToFire = leverupActive ? bulletlvup : bullet  ;

        GameObject bulletToFire2 = leverupActive2 ? bulletlvup2 : bullet2;
         // tạo một viện đạn lvup
        GameObject instantiatedBullet = Instantiate(bulletToFire, fireadd1.position, Quaternion.identity);
        GameObject instantiatedBullet2 = Instantiate(bulletToFire2, fireadd2.position, Quaternion.identity);

        // lực viên đạn lvup
        Rigidbody2D rb3 = instantiatedBullet.GetComponent<Rigidbody2D>();
        rb3.AddForce(transform.up * speedfire, ForceMode2D.Impulse);

         Rigidbody2D rb4 = instantiatedBullet2.GetComponent<Rigidbody2D>();
         rb4.AddForce(transform.up * speedfire, ForceMode2D.Impulse);




        // tạo một viện đạn 
        butllettmonter = Instantiate(bullet, fireadd1.position, Quaternion.identity);
        // tạo một viện đạn 
         butllettmonter2 = Instantiate(bullet2, fireadd2.position, Quaternion.identity);
        // lực viên đạn chạy 
        Rigidbody2D rb1 = butllettmonter.GetComponent<Rigidbody2D>();

        rb1.AddForce(transform.up * speedfire, ForceMode2D.Impulse);

        // lực viên đạn chạy 
         Rigidbody2D rb2 = butllettmonter2.GetComponent<Rigidbody2D>();

         rb2.AddForce(transform.up * speedfire, ForceMode2D.Impulse);





    }


    void tatuplv()
    {
        speedfire = 22;
        animator.SetBool("uplv", false);
        leverupActive = false;
        leverupActive2 = false;
        bullet.SetActive(true);
        bullet2.SetActive(true);
    }

   



    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(  collision.gameObject.tag == "leverup")
        {
            if(speedfire < 18)
            {
                audioSource.PlayOneShot(cliplvup);
                speedfire += 2;
                TimeFire -= 0.05f;

            }
            if ( speedfire >= 18)
            {
                audioSource.PlayOneShot(cliplvup);

                speedfire = 22;
                animator.SetBool("uplv", true);
                Invoke("tatuplv", 10f);
                leverupActive = true;
                leverupActive2 = true;
                bullet.SetActive(false);
                bullet2.SetActive(false);
            }



          



        }
        if (collision.gameObject.tag == "bulletmonter" || collision.gameObject.tag == "monterA")
        {
            if (checkrobot.activeInHierarchy)
            {
            
             
            }
            else
            {
                nowhp -= 2;
                thanhmau.capnhatthanhmau(nowhp, maxhp);


            }
            if (nowhp <= 0)
            {
               audioSource.PlayOneShot(Clipdie);
                br.Pause();
          
            }


        }
        if (collision.gameObject.tag == "bulletmonterB")
        {
            if (checkrobot.activeInHierarchy)
            {


            }
            else
            {
                nowhp -= 4;
                thanhmau.capnhatthanhmau(nowhp, maxhp);
            }

            if (nowhp <= 0)
            {
                audioSource.PlayOneShot(Clipdie);
                br.Pause();
            }
        }
        if (collision.gameObject.tag == "blboss")
        {
            if (checkrobot.activeInHierarchy)
            {


            }
            else
            {
                nowhp -= 3;
                thanhmau.capnhatthanhmau(nowhp, maxhp);
            }

            if (nowhp <= 0)
            {
                audioSource.PlayOneShot(Clipdie);
                br.Pause();
            }
        }

        if (collision.gameObject.tag == "monterC")
        {
            if (checkrobot.activeInHierarchy)
            {


            }
            else
            {
                nowhp =0;
                thanhmau.capnhatthanhmau(nowhp, maxhp);
            }

            if (nowhp <= 0)
            {
                audioSource.PlayOneShot(Clipdie);
                br.Pause();
            }
        }


       
        
    }
    public void tatrobot()
    {
        animator.SetBool("ROBOT", false);

    }
    //  public void settext(string txt)
    //  {
    //     if (textcoin)
    //     {
    //     textcoin.text = txt;
    //     }
    //   }


}
