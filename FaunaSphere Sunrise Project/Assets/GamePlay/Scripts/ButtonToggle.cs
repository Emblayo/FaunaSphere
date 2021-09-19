using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggle : MonoBehaviour
{
    public GameObject Screen;
    bool active;


    public void OpenAndClose()
    {
        if (active == false)
        {
            Screen.SetActive(true);
            active = true;
        }
        else
        {
            Screen.SetActive(false);
            active = false;
        }

    }

}
