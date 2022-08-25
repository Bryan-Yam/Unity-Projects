using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public enum e_game_mode
    {
        not_set,
        easy,
        medium,
        hard
    }

    public static GameSettings instance;

    private void Awake()
    {
        paused = false;
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
            Destroy(this);
    }

    private e_game_mode game_mode;

    private bool continue_previous_game = false;

    private bool exit_after_won = false;

    private bool paused = false;

    public void set_exit_after_won(bool set)
    {
        exit_after_won = set;
        continue_previous_game = false;
    }

    public bool get_exit_after_won()
    {
        return exit_after_won;
    }

    public void set_continue_prev_game(bool continue_game)
    {
        continue_previous_game = continue_game;
    }    

    public bool get_continue_prev_game()
    {
        return continue_previous_game;
    }

    public void set_pause(bool paused_bool)
    {
        paused = paused_bool;
    }

    public bool get_pause()
    {
        return paused;
    }

    private void Start()
    {
        game_mode = e_game_mode.not_set;
        continue_previous_game = false;
    }

    public void set_game_mode(e_game_mode mode)
    {
        game_mode = mode;
    }

    public void set_game_mode(string mode)
    {
        if (mode == "Easy")
            set_game_mode(e_game_mode.easy);
        else if (mode == "Medium")
            set_game_mode(e_game_mode.medium);
        else if (mode == "Hard")
            set_game_mode(e_game_mode.hard);
        else
            set_game_mode(e_game_mode.not_set);
    }

    public string get_game_mode()
    {
        switch (game_mode)
        {
            case e_game_mode.easy:
                return "Easy";
            case e_game_mode.medium:
                return "Medium";
            case e_game_mode.hard:
                return "Hard";
        }

        Debug.LogError("Error: Game Level not set");
        return " ";
    }
}
