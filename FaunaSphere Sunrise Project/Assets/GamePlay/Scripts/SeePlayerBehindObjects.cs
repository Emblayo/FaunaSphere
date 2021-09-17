using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePlayerBehindObjects : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("OccludesPlayer"))
        {
            Debug.Log("Entered an occludable region!");

            //SpriteRenderer spriteRenderer = collider2D.GetComponent<SpriteRenderer>();
            //if (spriteRenderer != null)
            //{
               // spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
           // }
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("OccludesPlayer"))
        {
            Debug.Log("Exited an occludable region!");

            //SpriteRenderer spriteRenderer = collider2D.GetComponent<SpriteRenderer>();
           // if (spriteRenderer != null)
            //{
               // spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            //}
        }
    }
    

}