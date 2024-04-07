using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class coinandcore : MonoBehaviour
{
    public TextMeshProUGUI textcoin;
    public TextMeshProUGUI textscore;
    int score;
    int coin;

    public float timescore = 1f;

    public AudioSource audioSource;
    public AudioClip clipcoin;


    int savecoin;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        timescore -= Time.deltaTime;
        if (timescore <0 ) {
            timescore = 1f;
            score += 1;
            textscore.text = ""+score;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {

            audioSource.PlayOneShot(clipcoin);
            coin += 1;
            textcoin.text = "X" + coin;
            PlayerPrefs.SetInt("savecoin", coin); // -1f là giá trị mặc định nếu không được lưu
            PlayerPrefs.Save();

        }
    }
}
