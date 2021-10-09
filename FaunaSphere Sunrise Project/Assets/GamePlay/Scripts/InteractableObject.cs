using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Material[] material;
    Renderer rend;
    public GameObject UI;
    public GameObject Player;
    Animator PlayerAnimator;
    private bool Action;


    void Awake()
    {
        PlayerAnimator = Player.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on a object");
        PlayerAnimator.SetTrigger("Action");
    }

    //If your mouse hovers over the GameObject with the script attached
    void OnMouseOver()
    {   
        rend.sharedMaterial = material[1];
        UI.SetActive(true);
    }

    //The mouse is no longer hovering over the GameObject
    void OnMouseExit()
    {  
        rend.sharedMaterial = material[0];
        UI.SetActive(false);
    }


}
