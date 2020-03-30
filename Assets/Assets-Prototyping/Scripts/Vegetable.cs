using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    [SerializeField]
    private float phMin;

    [SerializeField]
    private float phMax;


    public string description = "à remplir";
    
    public float getPhMin() { return phMin; }

    public float getPhMax() { return phMax; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
