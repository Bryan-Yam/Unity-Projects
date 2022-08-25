using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] //selects object at the top of heiarchy 
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite dead_sprite;
    [SerializeField] ParticleSystem particle_system;

    bool has_died;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die()); //needed when using IEnumerator 
        }           
    }

     bool ShouldDieFromCollision(Collision2D collision)
    {
        if (has_died) //check if goon is already dead 
            return false;

        Bird bird = collision.gameObject.GetComponent<Bird>(); //checks if object hitting goon is the bird or not, if yes, returns valid value, if not, null
        //collision also gives contact data -> used later 
        if (bird != null) //if hit by bird, dies
            return true;

        if (collision.contacts[0].normal.y < -0.5) //reads first contact point in contacts array; -0.5 checks for collision above 
            return true; 

        return false;
    }

    IEnumerator Die()
    {
        has_died = true;
        GetComponent<SpriteRenderer>().sprite = dead_sprite;
        particle_system.Play(); //plays death particles 
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false); //sets goon to false when hit by bird; it dies
    }
}
