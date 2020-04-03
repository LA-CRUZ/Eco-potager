using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate 
            {
                GameObject.Find("ResolverManager").GetComponent<Resolver>().selectPlot(transform);
            });
    }
}
