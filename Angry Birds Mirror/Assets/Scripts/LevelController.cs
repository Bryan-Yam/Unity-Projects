using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    Monster[] monsters; //holds collection of monsters

    [SerializeField] string next_level_name;

    void OnEnable()
    {
        monsters = FindObjectsOfType<Monster>(); //obtains all monster objects 
    }
    // Update is called once per frame
    void Update()
    {
        if (monsters_all_dead())
            next_level();
    }

    void next_level()
    {
        Debug.Log("Go to level" + next_level_name);
        SceneManager.LoadScene(next_level_name); 
    }

    bool monsters_all_dead()
    {
        foreach (var monster in monsters)
        {
            if (monster.gameObject.activeSelf) //check if any monsters are still active, if so, return false
                return false;
        }
        return true; //all monsters are dead
    }
}
