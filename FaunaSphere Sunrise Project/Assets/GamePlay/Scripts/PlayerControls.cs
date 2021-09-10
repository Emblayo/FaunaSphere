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