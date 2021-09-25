using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFaunaOnTag : MonoBehaviour
{
    public GameObject Sniffer;
    public GameObject Hoofer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.CompareTag("Sniffer"))
        {
            //Debug.Log("This is a Sniffer");
            Sniffer.SetActive(true);
            Hoofer.SetActive(false);
            PlayerPrefs.SetString("Species", "Sniffer");
        }

        if (gameObject.CompareTag("Hoofer"))
        {
            //Debug.Log("This is a Hoofer");
            Sniffer.SetActive(false);
            Hoofer.SetActive(true);
            PlayerPrefs.SetString("Species", "Hoofer");
        }

    }
}
