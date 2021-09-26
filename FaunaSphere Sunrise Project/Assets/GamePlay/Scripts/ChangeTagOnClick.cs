using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTagOnClick : MonoBehaviour
{

    public GameObject Fauna;
    public string Tag;

    public void ChangeTag()
    {
       Fauna.tag = Tag;
        PlayerPrefs.SetString("Species", Tag);
        Debug.Log("Changing the Tag");
    }

}
