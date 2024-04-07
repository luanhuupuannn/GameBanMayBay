using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class player2 : MonoBehaviour
{


    // bl
    GameObject butllettmonter;
    //blup2
    GameObject bulletlvup;
    GameObject bulletlvup2;

    public Transform fireaddlvup;
    bool lvup;
    public Transform fireaddlvup2;



    public GameObject bullet;
    public Transform fireadd1;

    public float TimeFire = 3f;
    public float speedfire = 10;
    private float timefire;

  
    // thanh masu 
    public thanhmau thanhmau;
    public float nowhp;
    public float maxhp = 10;
    Animator animator;
    //over
    Rigidbody2D rb1;
    public GameObject gameover;
    // dame hp
    public int damage;
    // âm thanh
    public AudioSource br;
    public AudioSource audioSource;
    public AudioClip Clipdie;
    public AudioClip cliplvup;
    // ani
    Animator ni;
    int random;

    public GameObject checkrobot;

    // Start is called before the first frame update
    void Start()
    {

       

        Time.timeScale = 1;
        ni = GetComponent<Animator>();
        damage = 10;
        PlayerPrefs.SetInt("damage", 10);
        PlayerPrefs.Save();

        // cập nhật thanh máu 
        nowhp = maxhp;
        thanhmau.capnhatthanhmau(nowhp, maxhp);


    }

    // Update is called once per frame
    void Update()
    {

        if (checkrobot.activeInHierarchy)
        {
            thanhmau.capnhatthanhmau(nowhp, maxhp);
            ni.SetBool("ROBOT2", true);

        }
        else
        {
            thanhmau.capnhatthanhmau(nowhp, maxhp);

            ni.SetBool("ROBOT2", false);


        }


        // thời gian bắn đạn
        timefire -= Time.deltaTime;

        if (timefire < 0)
        {
            firebullet();
        }
       

    }

     void firebullet()
    {
        timefire = TimeFire;
        // tạo một viện đạn 
        butllettmonter = Instantiate(bullet, fireadd1.position, Quaternion.identity);
        // lực viên đạn chạy 
          rb1 = butllettmonter.GetComponent<Rigidbody2D>();
          rb1.AddForce(transform.up * speedfire, ForceMode2D.Impulse);
        if (lvup) {
            bulletlvup = Instantiate(bullet, fireaddlvup.position, Quaternion.identity);
            // lực đạn
             rb1 = bulletlvup.GetComponent<Rigidbody2D>();
             rb1.AddForce(transform.up * speedfire, ForceMode2D.Impulse);


            bulletlvup2 = Instantiate(bullet, fireaddlvup2.position, Quaternion.identity);
            // lực đạn
            rb1 = bulletlvup2.GetComponent<Rigidbody2D>();
            rb1.AddForce(transform.up * speedfire, ForceMode2D.Impulse);
        }      
    }


     void tatlvup()
    {
        ni.SetBool("uplvpl2", false);

        lvup = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "leverup")
        {
            audioSource.PlayOneShot(cliplvup);
            if (damage < 30)
            {
                damage += 5; 

                PlayerPrefs.SetInt("damage",damage);
                PlayerPrefs.Save();
            }
            if (damage >= 30) {

                lvup = true;
                ni.SetBool("uplvpl2", true);
                Invoke("tatlvup", 9f);
            }
        }





        if (collision.gameObject.tag == "bulletmonter" || collision.gameObject.tag == "monterA")
        {
             if (checkrobot.activeInHierarchy)
                    {
                rand();

                     }
              else
                {
            nowhp -= 2;
            thanhmau.capnhatthanhmau(nowhp, maxhp);
                }

                 }
            if (nowhp <= 0)
               {
            Gover();

        }
        




                       
              if (collision.gameObject.tag == "bulletmonterB")
                    {
                          if (checkrobot.activeInHierarchy)
                            {
                        rand();

                             }
                          else
                          {
                        nowhp -= 4;
                        thanhmau.capnhatthanhmau(nowhp, maxhp);
                           }

                        if (nowhp <= 0)
                        {
                         Gover();
                        }
                    }


          if (collision.gameObject.tag == "blboss")
          {
            if (checkrobot.activeInHierarchy)
            {
                rand();

            }
            else
            {
                nowhp -= 3;
                thanhmau.capnhatthanhmau(nowhp, maxhp);
            }

            if (nowhp <= 0)
            {
                Gover();
            }
        }


        if (collision.gameObject.tag == "monterC")
                    {
                         if (checkrobot.activeInHierarchy)
                          {
                              rand();

                     }
                          else
                           {
                            nowhp = 0;
                          thanhmau.capnhatthanhmau(nowhp, maxhp);
                          }

                        if (nowhp <= 0)
                        {
                         Gover();
                        }
                    }


                   
                }

    void rand()
    {
        random = Random.Range(1, 10);
        if(random <= 5)
        {
            nowhp += 0.5f;
        }
        if (random > 5)
        {
            
        }
    }


    void Gover()
    {
        gameover.SetActive(true);
        Time.timeScale = 0;
        audioSource.PlayOneShot(Clipdie);
        br.Pause();
    }
            }
        
