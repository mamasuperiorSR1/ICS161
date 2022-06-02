using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public int count = 0;
    public bool soundTrigger = false;
    public AudioSource ping;
    public AudioSource ping2;
    public Text scoreColor;

    void Update()
    {
        if (soundTrigger)
        {
            if (count < 10)
            {
                ping.Play();
            }
            else
            {
                ping2.Play();
                scoreColor.color = Color.yellow;
            }
            soundTrigger = false;
        }
    }

}
