using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSwaying : MonoBehaviour
{

    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        animator.SetTrigger("Swaying");

        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
       
        yield return new WaitForSeconds(5);

        animator.ResetTrigger("Swaying");

    }
}
