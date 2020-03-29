using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Plants
{
    Carrot,
    Cauliflower,
    Celery,
    Cucumber,
    Lettuce,
    Melon,
    Potato,
    Tomato,
    Turnip
};

public class Vegetable : MonoBehaviour
{

    public Plants nom;
    [SerializeField]
    private float phMin;

    [SerializeField]
    private float phMax;

    [NonSerialized]
    public Caracteristique caract;

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
