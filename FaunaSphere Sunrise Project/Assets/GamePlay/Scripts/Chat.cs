using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Chat : MonoBehaviour
{

    public Transform chatLayout;
    public GameObject chatLayoutGroup;
    public GameObject chatBubble;
    public TMP_InputField inputField;
    public string chatText;
    private TextMeshProUGUI bubbleText;
    public Button sendButton;

    void Update()
    {

    }


    public void CreateBubble()
    {
        Debug.Log("clicked Send");

        //Creating the bubble
        Instantiate(chatBubble, chatLayout);

        
        bubbleText = chatLayoutGroup.GetComponentInChildren<TextMeshProUGUI>();

        bubbleText.text = chatText;

        inputField.text = "";

        sendButton.interactable = false;

    }

    public void ReadText()
    {
        //reading the text and applying it to this public string
        Debug.Log("you changed the text");
        chatText = inputField.text;
        sendButton.interactable = true;
    }
}
