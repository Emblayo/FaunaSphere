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
    public bool Pollution;
    public Vector3 PolPos;


    void Awake()
    {

        //PlayerAnimator = Player.GetComponent<Animator>();
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
        if (PlayerPrefs.GetString("FaunaLoaded") == "true")
        {

            GameObject fauna = GameObject.FindGameObjectWithTag("Fauna");
            PlayerAnimator = fauna.GetComponent<Animator>();
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on a object");
        PlayerAnimator.SetTrigger("Action");

        
        if (Pollution == true)
        {
            PolPos = transform.position;
            print("Pollution clicked");
            //call "OnPollutionClicked" from PlayerControls script
            PlayerControls pollutionClicked = FindObjectOfType<PlayerControls>();
            pollutionClicked.OnPollutionClicked(PolPos);//input the coordinates of the pollution?
        }
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

    public void Zap(Vector3 PlayerPos, Vector3 PolPos)
    {
        PlayerPos.x = PlayerPos.x*-1;
        if (PlayerPos.x > 0)
        {
            PlayerPos.y = PlayerPos.y * -1;
        }
        else if (PlayerPos.x < 0)
        {
            PlayerPos = PlayerPos * -1;
        }
        //PlayerPos.y = 0;
        //PlayerPos.z = 0;
        GameObject line = GameObject.Find("Line");
        
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.enabled= true;
        Vector3[] linePos = {PolPos, PlayerPos};
        lineRenderer.SetPositions(linePos);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.25f;
        StartCoroutine(RemoveAfterSeconds(1, lineRenderer));

        //line.SetActive(false);

    }



IEnumerator RemoveAfterSeconds(int seconds, LineRenderer obj)
{
    yield return new WaitForSeconds(seconds);
    obj.enabled = false;
}

}
