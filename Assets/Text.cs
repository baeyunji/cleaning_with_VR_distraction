using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{
    public UnityEngine.UI.Text maintext;

    [Header("mapping control")]
    public bool ar_sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ar_sound == true)
        {

            maintext.text = "게임을 끝내십시오";

        }
        if (ar_sound == false)
        {

            maintext.text = "   ";

        }
    }
}
