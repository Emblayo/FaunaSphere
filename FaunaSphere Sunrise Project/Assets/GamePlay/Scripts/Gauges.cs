using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauges : MonoBehaviour
{
    public Image energy;
    public Image happiness;
    
    // Start is called before the first frame update
    void Start()
    {
        energy.fillAmount = 1;
        happiness.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
      //energy bar depletes over 20 mins
      energy.fillAmount -= 1.0f / 1200f * Time.deltaTime;
    }
}
