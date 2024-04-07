using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class loadCoinMenu : MonoBehaviour
{

    public TextMeshProUGUI loadcoin;

    int coinmenu;

    // Start is called before the first frame update
    void Start()
    {
        coinmenu = PlayerPrefs.GetInt("savecoin");
        loadcoin.text = "X" + coinmenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
