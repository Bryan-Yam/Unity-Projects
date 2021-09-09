using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string new_game_scene;
    public string instructions;
    public string do_not;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start_game()
    {
        SceneManager.LoadScene(new_game_scene);
    }

    public void instruction_button()
    {
        SceneManager.LoadScene(instructions);
    }

    public void do_not_button()
    {
        SceneManager.LoadScene(do_not);
    }

    public void quit_game()
    {
        Application.Quit();
    }

}
