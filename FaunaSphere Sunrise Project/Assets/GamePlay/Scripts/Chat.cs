using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Chat : MonoBehaviour
{

    public Transform chatLayout;
    public GameObject chatBubble;
    public TextMeshProUGUI ïnputText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void CreateBubble()
    {
        Debug.Log("clicked Send");
        Instantiate(chatBubble, chatLayout);
    }
}
