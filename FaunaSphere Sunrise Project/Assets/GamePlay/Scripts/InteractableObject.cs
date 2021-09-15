using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on a object");
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject
        
        GetComponent<SpriteRenderer>().color = Color.white;
    }


}
