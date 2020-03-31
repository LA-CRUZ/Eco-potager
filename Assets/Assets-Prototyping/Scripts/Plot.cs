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
    private int quantiteEau;
    [SerializeField]
    private int quantiteNutrition;
    [SerializeField]
    private int quantiteLumière;   // taux de luminosité => à garder? 

    [SerializeField]
    private float azote = 0f;
    [SerializeField]
    private float phosphore = 0f;
    [SerializeField]
    private float potassium = 0f;

    [SerializeField]
    private float ph;

    public string historiquePlot = "indiquer ici si le terrain a déjà servie à la culture etc..";
    

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
    public int getQEau() { return quantiteEau; }

    public int getQNutrition() { return quantiteNutrition; }

    public float getAzote() { return azote; }

    public float getPhosphore() { return phosphore; }

    public float getPotassium() { return potassium; }

    public float getPh() { return ph; }


    //all Setters
    public void setQEau(int _quantiteEau) { quantiteEau = (_quantiteEau >= 0 && _quantiteEau <= 3) ? _quantiteEau : quantiteEau; }    // idem

    public void setQNutrition(int _quantiteNutrition) { quantiteNutrition = (_quantiteNutrition >= 0 && _quantiteNutrition <= 3) ? _quantiteNutrition : quantiteNutrition; }    // ...

    public void setQLumière(int _quantiteLumière) { quantiteLumière = (_quantiteLumière >= 0 && _quantiteLumière <= 3) ? _quantiteLumière : quantiteLumière; }    // ...

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
    public void addToQEau(int _quantiteEau)
    {
        if (_quantiteEau >= 0 && _quantiteEau <= 3)
        {
            if (quantiteEau + _quantiteEau > 3)
                quantiteEau = 3;
            else if (quantiteEau + _quantiteEau < 0)
                quantiteEau = 0;
            else quantiteEau += _quantiteEau;
        }
    }

    public void addToQNutrition(int _quantiteNutrition)
    {
        if (_quantiteNutrition >= 0f && _quantiteNutrition <= 3)
        {
            if (quantiteNutrition + _quantiteNutrition > 3)
                quantiteNutrition = 3;
            else if (quantiteNutrition + _quantiteNutrition < 0)
                quantiteNutrition = 0;
            else quantiteNutrition += _quantiteNutrition;
        }
    }

    public void addToQLumière(int _quantiteLumière)
    {
        if (_quantiteLumière >= 0 && _quantiteLumière <= 3)
        {
            if (quantiteLumière + _quantiteLumière > 3)
                quantiteLumière = 3;
            else if (quantiteLumière + _quantiteLumière < 0)
                quantiteLumière = 0;
            else quantiteLumière += _quantiteLumière;
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
            if (quantiteNutrition > 3 || quantiteNutrition < 0)
            {
                throw new Exception("invalid quantiteNutrition current in gameobject  " + name + " : " + quantiteNutrition);
            }
            if (quantiteEau > 3 || quantiteEau < 0)
            {
                throw new Exception("invalid quantiteEau current in gameobject  " + name + " : " + quantiteEau);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }
}
