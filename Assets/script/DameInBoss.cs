using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DameInBoss : MonoBehaviour
{

    public thanhmau thanhmau;
    public float nowhp;
    public float maxhp = 2000;

    public GameObject dark;
    public GameObject again;



    public int dame;

    Animator animator;



    void Start()
    {
        dame = PlayerPrefs.GetInt("damage");

        animator = GetComponent<Animator>();

        // cập nhật thanh máu 
        nowhp = maxhp;
        thanhmau.capnhatthanhmau(nowhp, maxhp);


    }

    private void Update()
    {
        dame = PlayerPrefs.GetInt("damage");


        if (nowhp <= 1000 )
        {
            dark.SetActive(true);
        }
        if(nowhp < 0)
        {
            dark.SetActive(false);
            animator.SetBool("desboss", true);  // Set the animation parameter immediately

            Destroy(this.gameObject);  // Destroy the game object after the delay

            ADDagain();




        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bulletplayer")
        {

            nowhp -= 2;
            thanhmau.capnhatthanhmau(nowhp, maxhp);


        }

        if (collision.gameObject.tag == "bulletplayer2")
        {

            nowhp -= 4;
            thanhmau.capnhatthanhmau(nowhp, maxhp);
        }
        // player2
        else if (collision.gameObject.tag == "blplayer2")
        {
            nowhp -= dame;
            thanhmau.capnhatthanhmau(nowhp, maxhp);

            // GameObject trungdon = Instantiate(hieuungtrungdon2,
            // this.transform.position, this.transform.rotation);
            //Destroy(trungdon, 0.5f);
            //if (hp <= 0)
            // {


            //  audioSource.PlayOneShot(clipdie);
            //   StartCoroutine(DelayedDestroy());  // Start the coroutine for delayed destruction

            // }
        }
        else if (collision.gameObject.tag == "bulletROBOT2")
        {
            nowhp = 4;
           // GameObject trungdon = Instantiate(hieuungtrungdon,
          //  this.transform.position, this.transform.rotation);
           // Destroy(trungdon, 0.5f);
           // if (hp <= 0)
           // {
            //    audioSource.PlayOneShot(clipdie);
            //    StartCoroutine(DelayedDestroy());  // Start the coroutine for delayed destruction
           // }
        }




    }
    public void ADDagain()
    {
        if (again)
        {
            Vector2 monsterAdd = new Vector3(8.613373f, 10.04835f, 180);
            Instantiate(again, monsterAdd, Quaternion.identity);
        }
    }

}
