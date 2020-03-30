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

public class Plot : MonoBehaviour
{

    public Nature sol;

    [SerializeField]
    private float tauxHum;
    [SerializeField]
    private float tauxNut;
    [SerializeField]
    private float tauxLum;   // taux de luminosité => à garder? 

    [SerializeField]
    private float azote = 0f;
    [SerializeField]
    private float phosphore = 0f;
    [SerializeField]
    private float potassium = 0f;

    [SerializeField]
    private float ph;


    

    // Start is called before the first frame update
    void Start()
    {
        float somme = azote + phosphore + potassium;
        if (somme != 100)
        {
            Debug.Log("WARNING : sum of all minerals (" + azote + "," + phosphore + "," + potassium + ") doesn't equal to 100 in gameObject :" + name);
        }
        errorManagement();
    }

    // Update is called once per frame
    void Update()
    {
        errorManagement();
    }

    // all Getters
    public float getTauxHum() { return tauxHum; }

    public float getTauxNut() { return tauxNut; }

    public float getAzote() { return azote; }

    public float getPhosphore() { return phosphore; }

    public float getPotassium() { return potassium; }

    public float getPh() { return ph; }


    //all Setters
    public void setTauxHum(float _tauxHum) { tauxHum = (_tauxHum >= 0f && _tauxHum <= 100f) ? _tauxHum : tauxHum; }    // idem

    public void setTauxNut(float _tauxNut) { tauxNut = (_tauxNut >= 0f && _tauxNut <= 100f) ? _tauxNut : tauxNut; }    // ...

    public void setTauxLum(float _tauxLum) { tauxLum = (_tauxLum >= 0f && _tauxLum <= 100f) ? _tauxLum : tauxLum; }    // ...

    public void setAzote(float _azote)
    {
        azote += _azote;
        phosphore -= _azote / 2;
        potassium -= _azote / 2;

    }

    public void setPhosphore(float _phosphore)
    {
        azote -= _phosphore / 2;
        phosphore += _phosphore;
        potassium -= _phosphore / 2;

    }

    public void setPotassium(float _potassium)
    {
        azote -= _potassium / 2;
        phosphore -= _potassium / 2;
        potassium += _potassium;

    }

    public void setPh(float _ph) { ph = (_ph >= 0f && _ph <= 14f) ? _ph : ph; }   // si le ph passé en param est incompatible on ne fait rien 

    // fct d'ajout 
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

    public void addToPh(float _ph)
    {
        if (_ph >= 0f && _ph <= 14f)
        {
            if (ph + _ph > 14f)
                ph = 14f;
            else if (ph + _ph < 0f)
                ph = 0f;
            else ph += _ph;
        }
    }

    // gestion des erreurs
    private void errorManagement()
    {
        try
        {
            if (tauxNut > 100f || tauxNut < 0f)
            {
                throw new Exception("invalid tauxNut current in gameobject  " + name + " : " + tauxNut);
            }
            if (tauxHum > 100f || tauxHum < 0f)
            {
                throw new Exception("invalid tauxHum current in gameobject  " + name + " : " + tauxHum);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }
}
