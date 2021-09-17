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
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("OccludesPlayer"))
        {
            Debug.Log("Exited an occludable region!");
        }
    }

}