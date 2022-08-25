using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HaHaMeme : MonoBehaviour
{
    public string go_back;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void back_button()
    {
        SceneManager.LoadScene(go_back);
    }
}
