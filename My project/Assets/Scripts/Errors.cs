using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Errors : MonoBehaviour
{
    public List<GameObject> error_ims;
    public GameObject game_over_popup;

    int lives = 0;
    int error_num = 0;

    public static Errors instance;
    private void Awake()
    {
        if (instance)
            Destroy(instance);

        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        lives = error_ims.Count;
        error_num = 0;
    }

    public int get_error_num()
    {
        return error_num;
    }
    private void wrong_num()
    {
        if (error_num < error_ims.Count)
        {
            error_ims[error_num].SetActive(true);
            error_num++;
            lives--;
        }
        check_game_over();
    }

    private void check_game_over()
    {
        if (lives <= 0)
        {
            GameEvents.on_game_over_method();
            game_over_popup.SetActive(true);
        }
    }

    private void OnEnable()
    {
        GameEvents.on_wrong_num += wrong_num;
    }

    private void OnDisable()
    {
        GameEvents.on_wrong_num -= wrong_num;
    }
}
