using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWon : MonoBehaviour
{
    public GameObject winpopup;

    public Text clock_text;

    // Start is called before the first frame update
    void Start()
    {
        winpopup.SetActive(false);
        // clock_text.text = Clock.instance.get
    }

    private void on_board_complete()
    {
        winpopup.SetActive(true);
        // clock_text.text ...
    }
    private void OnEnable()
    {
        GameEvents.on_board_complete += on_board_complete;
    }
    private void OnDisable()
    {
        GameEvents.on_board_complete -= on_board_complete;
    }
}
