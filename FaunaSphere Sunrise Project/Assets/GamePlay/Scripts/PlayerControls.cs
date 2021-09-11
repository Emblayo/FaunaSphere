using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 1;
    private Vector3 target;
    public Animator animator;
    private bool IsMoving;

    void Start()
    {
        target = transform.position;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            
            //Flips the character if they aren't facing the right way:
            if (target.x < transform.position.x && gameObject.transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (target.x > transform.position.x && gameObject.transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position == target) { IsMoving = false; }
        if (transform.position != target) { IsMoving = true; }

        if (IsMoving == true) animator.SetBool("IsMoving", true);
        if (IsMoving == false) animator.SetBool("IsMoving", false);
    }

    void FixedUpdate()
    {
        

    }

}
