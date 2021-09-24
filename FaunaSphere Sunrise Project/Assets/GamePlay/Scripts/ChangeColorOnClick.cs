using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorOnClick : MonoBehaviour
{
    
    public GameObject Fauna;
    public Color colorPicker;

    // Start is called before the first frame update
    public void ChangeColor()
    {

        GameObject[] colorChangers = GameObject.FindGameObjectsWithTag("Recolor");

        foreach (var sprite in colorChangers)
        {
            if (sprite.GetComponent<SpriteRenderer>())
            {
                var renderer = sprite.GetComponent<SpriteRenderer>();
                renderer.color = colorPicker;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

