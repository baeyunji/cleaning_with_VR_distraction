using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [Header("mapping control")]
    public bool weather;

    public AudioSource source;
    public AudioClip clip;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {

            source.PlayOneShot(clip);
        }
        if (weather == true)
        {

            source.PlayOneShot(clip);

            weather = false;

        }
       
    }





}
