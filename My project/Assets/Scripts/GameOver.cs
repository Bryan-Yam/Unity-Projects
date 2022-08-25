using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameOver : MonoBehaviour
{
    public Text clock_text;
    private void Start()
    {
        clock_text.text = Clock.instance.get_current_time_text().text;
    }
}
