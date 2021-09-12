using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    public Animator animator;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //gets the input in x and y position    
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        //Reset move Delta
        moveDelta = new Vector3(x,y,0f);

        //logic for determining hitboxes and whether a character and move in a tile
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y *Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null)
        {
            transform.Translate(0,moveDelta.y * Time.deltaTime, 0);
        }
            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x *Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

        //sets the state of the animator and determins which animation to play
        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Vertical", y);
        animator.SetFloat("Magnitude", moveDelta.magnitude);

    }
}
