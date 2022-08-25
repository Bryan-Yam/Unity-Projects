using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadGame : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (Config.game_data_exist() == false)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            
        }
    }

    public void set_game_data()
    {
        GameSettings.instance.set_game_mode(Config.read_board_level()); //set board level from save data
    }


}
