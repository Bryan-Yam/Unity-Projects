using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float launch_force = 500; //allows you to change force of launch within unity 
    [SerializeField] float max_drag_distance = 5; //sets distance from blob that can be used by user 
    private Vector2 start_position;
    Rigidbody2D rigid_body_2D;
    SpriteRenderer sprite_renderer;

    void Awake()
    {
        rigid_body_2D = GetComponent<Rigidbody2D>(); //shortcut for grabbing rigidbody component 
        sprite_renderer = GetComponent<SpriteRenderer>(); //shortcut for spriterenderer comp
    }
    // Start is called before the first frame update
    void Start()
    {
        start_position = rigid_body_2D.position; //position of blob in x and y 
        rigid_body_2D.isKinematic = true; //Keeps bird up on start; physics engine doesn't apply yet  
    }

    void OnMouseDown()
    {
        sprite_renderer.color = new Color(253, 0, 255); //changes color to purple on mouse click down 
    }

    void OnMouseUp()
    {
        var current_position = rigid_body_2D.position; //position of blob in x and y
        var direction = start_position - current_position; //calculate direction blob will go
        direction.Normalize();

        rigid_body_2D.isKinematic = false; //allows blob to be launched
        rigid_body_2D.AddForce(direction * launch_force); //sets force value applied to blob when launched

        sprite_renderer.color = new Color(255, 255, 255); //changes color to white on mouse click up
    }

    void OnMouseDrag()
    {
        Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition); //takes mouse position from main camera

        Vector2 user_position = mouse_position;

        float distance = Vector2.Distance(user_position, start_position); //calculates distance from user to start positions

        if (distance > max_drag_distance) //checks if distance exceeds max drag distance 
        {
            Vector2 direction = user_position - start_position;
            direction.Normalize();
            user_position = start_position + direction * max_drag_distance; //sets position of blob to max distance in desired direction 
        }

        if (user_position.x > start_position.x) //checking if user is moving blob more right than allowed
            user_position.x = start_position.x;

        rigid_body_2D.position = user_position; //sets body to position on drag
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetWithDelay()); //beginning needed for ienumerator
    }

    IEnumerator ResetWithDelay()
    {
        yield return new WaitForSeconds(3); //adds delay on blob when hitting something; resets after each collision  
        rigid_body_2D.position = start_position; //resets blob position to start
        rigid_body_2D.isKinematic = true; //Keeps bird up on start; physics engine doesn't apply yet 
        rigid_body_2D.velocity = Vector2.zero; //resets velocity of bird 
    }
}
