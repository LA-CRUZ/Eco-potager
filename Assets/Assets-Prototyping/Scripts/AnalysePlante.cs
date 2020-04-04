using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnalysePlante : MonoBehaviour
{
    public Plant plant;
    public string departConseil;
    public string finConseil;
    public string conseil;

    public int maitrisePh;
    public int maitriseQEau;
    public int maitriseQNut;
    public int maitriseMin;

    public int scoreTot;    // augmente 1 à chaque erreur
    public int nbPlanter;

    public void genCommentaire()   // génération des commentaires
    {
        departConseil = "concernant les <b>" + translate(plant) + "</b> ";
        finConseil = "";
        //Plant p = (Plant)AssetDatabase.LoadAssetAtPath("Assets/_Data/Plantes/" + plant + ".asset", typeof(Plant));
        if (maitrisePh / nbPlanter < 0.5f)
        {
            genComPh(plant);
        }
        if (maitriseQEau / nbPlanter < 0.5f) { 
            genComQEau(plant);
        }if (maitriseQNut / nbPlanter< 0.5f) { 
                genComQNut(plant);
        }if (maitriseMin / nbPlanter < 0.5f){
            genComMin(plant);
        }
        conseil = departConseil + finConseil;
    }

    private void genComMin(Plant p)
    {
        if (finConseil == "")
        {
            finConseil += "il vaut mieux les planter sur un sol riche en " + p.mineral.ToString().ToLower() + ".\n";
        }
        else finConseil += "Il vaut mieux les planter sur un sol riche en " + p.mineral.ToString().ToLower() + ".\n";

        scoreTot++;
    }

    private void genComQNut(Plant p)
    {
        switch(p.quantiteNutrition)
        {
            case 1:
                if (finConseil == "")
                {
                    finConseil += "une parcelle pauvre en nutriment lui suffit.\n";
                }else finConseil += "Une parcelle pauvre en nutriment lui suffit.\n";

                break;
            case 2:
                if (finConseil == "")
                {
                    finConseil += "une parcelle avec un apport de nutriment moyen lui suffit.\n";
                } else finConseil += "Une parcelle avec un apport de nutriment moyen lui suffit.\n";
                break;
            case 3:
                if (finConseil == "")
                {
                    finConseil += "une parcelle riche en nutriment pour pousser.\n";
                } else finConseil += "Une parcelle riche en nutriment pour pousser.\n";
                break;
            default:
                if (finConseil == "")
                {
                    finConseil += "une parcelle même pauvre en nutriment lui suffit pour grandir.\n";
                } else finConseil += "Une parcelle même pauvre en nutriment lui suffit pour grandir.\n";
                break;
        }
        scoreTot++;
    }

    private void genComQEau(Plant p)
    {
        switch (p.quantiteEau)
        {
            case 1:
                if (finConseil == "")
                {
                    finConseil += "une parcelle assez sec suffirait pour son apport en eau.\n";
                } else finConseil += "Une parcelle assez sec suffirait pour son apport en eau.\n";
                break;
            case 2:
                if (finConseil == "")
                {
                    finConseil += "une parcelle assez humide est nécessaire pour son apport en eau.\n";
                }
                else finConseil += "Une parcelle assez humide est nécessaire pour son apport en eau.\n";

                break;
            case 3:
                if (finConseil == "")
                {
                    finConseil += "une parcelle très humide est indispensable pour sa croissance.\n";
                }else finConseil += "Une parcelle très humide est indispensable pour sa croissance.\n";
                break;
            default:
                if (finConseil == "")
                {
                    finConseil += "un sol très sec lui suffirait pour pousser.\n";
                } else finConseil += "Un sol très sec lui suffirait pour pousser.\n";
                break;
        }
        scoreTot++;
    }

    private void genComPh(Plant p)
    {
        if (p.phMax >= 7)
        {
            if(finConseil == "")
                finConseil += "une terre avec un ph neutre (c'est-à-dire proche de 7) est nécessaire.\n";
            else finConseil += "Une terre avec un ph neutre (c'est-à-dire proche de 7) est nécessaire.\n";

        }
        else
        {
            if(finConseil == "")
                finConseil += "une terre avec un ph acide (vers 6) est idéale pour cette plante.\n";
            else finConseil += "Une terre avec un ph acide (vers 6) est idéale pour cette plante.\n";
        }
        scoreTot++;
    }

    public string translate(Plant plante)
    {
        string nomAuPluriel = "";

        if (plante.nom == "Poireau")
            nomAuPluriel = "poireaux";
        else if (plante.nom == "Pomme de terre")
            nomAuPluriel = "pommes de terre";
        else if (plante.nom == "Chou-fleur")
            nomAuPluriel = "choux-fleurs";
        else if (plante.nom == "Radis")
            nomAuPluriel = "radis";
        else nomAuPluriel = plante.nom.ToLower() + "s";

        return nomAuPluriel;
    }
}
