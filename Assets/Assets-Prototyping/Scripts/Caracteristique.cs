using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Nature
{
    Alcalin,    // terrain classique pour un potager
    Argileux,   // difficilement exploitable mais riche en Potassium
    None        // WARNING : cet enum sert aux plantes qui n'ont pas de préférence au niveau du type de sol
};

public class Caracteristique : MonoBehaviour
{

    public Nature sol;

    [SerializeField]
    private float tauxHum;
    [SerializeField]
    private float tauxNut;
    [SerializeField]
    private float tauxLum;   // taux de luminosité => à garder? 

    [NonSerialized]
    public Mineraux minerals;    // proportion des mineraux pour le plot, la somme des 3 attributs de Minéraux doit faire 100 (ou 100.0 ...)
                                 // bien attaché le script Mineraux à l'objet pour initialiser cet attribut

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            minerals = gameObject.GetComponent<Mineraux>();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
        errorManagement();
    }

    // Update is called once per frame
    void Update()
    {
        errorManagement();
    }

    public float getTauxHum() { return tauxHum; }

    public float getTauxNut() { return tauxNut; }

    public void setTauxHum(float _tauxHum) { tauxHum = (_tauxHum >= 0f && _tauxHum <= 100f) ? _tauxHum : tauxHum; }    // idem

    public void setTauxNut(float _tauxNut) { tauxNut = (_tauxNut >= 0f && _tauxNut <= 100f) ? _tauxNut : tauxNut; }    // ...

    public void setTauxLum(float _tauxLum) { tauxLum = (_tauxLum >= 0f && _tauxLum <= 100f) ? _tauxLum : tauxLum; }    // ...

    public void addToTauxHum(float _tauxHum)
    {
        if (_tauxHum >= 0f && _tauxHum <= 100f)
        {
            if (tauxHum + _tauxHum > 100f)
                tauxHum = 100f;
            else if (tauxHum + _tauxHum < 0f)
                tauxHum = 0f;
            else tauxHum += _tauxHum;
        }
    }

    public void addToTauxNut(float _tauxNut)
    {
        if (_tauxNut >= 0f && _tauxNut <= 100f)
        {
            if (tauxNut + _tauxNut > 100f)
                tauxNut = 100f;
            else if (tauxNut + _tauxNut < 0f)
                tauxNut = 0f;
            else tauxNut += _tauxNut;
        }
    }

    public void addToTauxLum(float _tauxLum)
    {
        if (_tauxLum >= 0f && _tauxLum <= 100f)
        {
            if (tauxLum + _tauxLum > 100f)
                tauxLum = 100f;
            else if (tauxLum + _tauxLum < 0f)
                tauxLum = 0f;
            else tauxLum += _tauxLum;
        }
    }


    private void errorManagement()
    {
        try
        {
            if (tauxNut > 100f || tauxNut < 0f)
            {
                throw new Exception("tauxNut current in gameobject  " + name + " : " + tauxNut);
            }
            if (tauxHum > 100f || tauxHum < 0f)
            {
                throw new Exception("tauxHum current in gameobject  " + name + " : " + tauxHum);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }
}
