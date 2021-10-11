 using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 1;
    private Vector3 target;
    private Animator animator;
    private bool IsMoving;
    public GameObject PlayerUI;

    void Start()
    {
        target = transform.position;
        animator = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        Movement();

    }

    void FixedUpdate()
    {
        
    }

    //Stops the movement on collision
    void OnCollisionEnter2D(Collision2D col)
    {
        target = transform.position;
    }

    private void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Checking if the mouse is over UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Its over UI elements");
                return;
            }
            else
            {
                Debug.Log("Its NOT over UI elements");
            }

            //Movement
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;

            //Flips the character if they aren't facing the right way
            //and flips the UI so it always faces to the right
            if (target.x < transform.position.x && gameObject.transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                PlayerUI.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else if (target.x > transform.position.x && gameObject.transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                PlayerUI.transform.rotation = Quaternion.Euler(0, 0f, 0);
            }

        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //Animation states for running
        if (transform.position == target) { IsMoving = false; }
        if (transform.position != target) { IsMoving = true; }

        if (IsMoving == true) animator.SetBool("IsMoving", true);
        if (IsMoving == false) animator.SetBool("IsMoving", false);
    }
    

}
