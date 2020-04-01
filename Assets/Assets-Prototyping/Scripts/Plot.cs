using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Minerals
{
    Azote,
    Phosphore,
    Potassium,
    None
};

public class Plot : MonoBehaviour
{
    [SerializeField]
    private int quantiteEau;
    [SerializeField]
    private int quantiteNutrition;

    [SerializeField]
    private Minerals mineral = Minerals.None;

    [SerializeField]
    private float ph;

    public string historiquePlot = "indiquer ici si le terrain a déjà servie à la culture etc..";

    public GameObject tooltip;

    // Start is called before the first frame update
    void Start()
    {
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

    public Minerals getMineral() { return mineral; }

    public float getPh() { return ph; }


    //all Setters
    public void setQEau(int _quantiteEau) { quantiteEau = (_quantiteEau >= 0 && _quantiteEau <= 3) ? _quantiteEau : quantiteEau; }    // idem

    public void setQNutrition(int _quantiteNutrition) { quantiteNutrition = (_quantiteNutrition >= 0 && _quantiteNutrition <= 3) ? _quantiteNutrition : quantiteNutrition; }    // ...

    public void setMineral(Minerals _mineral)
    {
        mineral = _mineral;
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
