using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Clock : MonoBehaviour
{
    int hour = 0;
    int minute = 0;
    int second = 0;

    private Text clock_text;

    float delta_time;

    private bool stop_clock = false;

    public static Clock instance;

    private void Awake()
    {
        if (instance)
            Destroy(instance);
        instance = this;
        clock_text = GetComponent<Text>();
        delta_time = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        stop_clock = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stop_clock == false)
        {
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);

            string hour = leading_zero(span.Hours);
            string minute = leading_zero(span.Minutes);
            string second = leading_zero(span.Seconds);

            clock_text.text = hour + ":" + minute + ":" + second; //sets gameobject text to timer 
        }
    }

    string leading_zero(int num)
    {
        return num.ToString().PadLeft(2, '0');
    }

    public void on_game_over()
    {
        stop_clock = true;
    }

    private void OnEnable()
    {
        GameEvents.on_game_over += on_game_over;
    }

    private void OnDisable()
    {
        GameEvents.on_game_over -= on_game_over;
    }

    public static string get_current_time()
    {
        return instance.delta_time.ToString();
    }

    public Text get_current_time_text()
    {
        return clock_text;
    }
        
}
