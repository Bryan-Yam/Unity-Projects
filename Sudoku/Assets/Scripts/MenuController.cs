using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Levels to Load")]
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    public void newGameDialogYes(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void loadGameDialogYes()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        } 
        else 
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void load_easy_game(string name)
    {
        GameSettings.instance.set_game_mode(GameSettings.e_game_mode.easy);
        SceneManager.LoadScene(name);
    }

    public void load_medium_game(string name)
    {
        GameSettings.instance.set_game_mode(GameSettings.e_game_mode.medium);
        SceneManager.LoadScene(name);
    }

    public void load_hard_game(string name)
    {
        GameSettings.instance.set_game_mode(GameSettings.e_game_mode.hard);
        SceneManager.LoadScene(name);
    }

    public void activate_object(GameObject _object)
    {
        _object.SetActive(true);
    }

    public void deactivate_object(GameObject _object)
    {
        _object.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    public void set_pauses(bool paused)
    {
        GameSettings.instance.set_pause(paused);
    }

    public void load_prev_game(bool continue_game)
    {
        GameSettings.instance.set_continue_prev_game(continue_game);
    }
}
