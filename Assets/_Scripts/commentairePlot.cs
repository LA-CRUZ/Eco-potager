using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commentairePlot : MonoBehaviour
{
    string Legume = "";
    string Saison = "";
    string Hydratation = "";
    string Engrais = "";
    string Traitement = "";
    string PH = "";

    public string getLegume()
    {
        return Legume;
    }

    public string getSaison()
    {
        return Saison;
    }

    public string getHydratation()
    {
        return Hydratation;
    }

    public string getEngrais()
    {
        return Engrais;
    }

    public string getTraitement()
    {
        return Traitement;
    }

    public string getPH()
    {
        return PH;
    }

    public void setLegume(string _Legume)
    {
        Legume = _Legume;
    }

    public void setSaison(string _Saison)
    {
        Saison = _Saison;
    }

    public void setHydratation(string _Hydratation)
    {
        Hydratation = _Hydratation;
    }

    public void setEngrais(string _Engrais)
    {
        Engrais = _Engrais;
    }

    public void setTraitement(string _Traitement)
    {
        Traitement = _Traitement;
    }

    public void setPH(string _PH)
    {
        PH = _PH;
    }
}
