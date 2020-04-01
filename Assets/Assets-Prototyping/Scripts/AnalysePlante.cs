using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnalysePlante : MonoBehaviour
{
    public Plants plant;

    public string conseil;

    public int maitrisePh;
    public int maitriseQEau;
    public int maitriseQNut;
    public int maitriseMin;

    public int scoreTot;    // augmente 1 à chaque erreur
    public int nbPlanter;

    public void genCommentaire()   // génération des commentaires
    {
        conseil = "Conseils sur les " + plant + ":\n";
        Plant p = (Plant)AssetDatabase.LoadAssetAtPath("Assets/_Data/Plantes/" + plant + ".asset", typeof(Plant));
        if (maitrisePh / nbPlanter < 0.5f)
            genComPh(p);
        if (maitriseQEau / nbPlanter < 0.5f)
            genComQEau(p);
        if (maitriseQNut / nbPlanter < 0.5f)
            genComQNut(p);
        if (maitriseMin / nbPlanter < 0.5f)
            genComMin(p);
    }

    private void genComMin(Plant p)
    {
        conseil += "celle-ci préfère les parcelles richent en " + p.mineral + ".\n";
        scoreTot++;
    }

    private void genComQNut(Plant p)
    {
        switch(p.quantiteNutrition)
        {
            case 1:
                conseil += "cette plante peut très bien être cultivé sur un sol pauvre en nutriment.\n";
                break;
            case 2:
                conseil += "cette plante a besoin d'être cultivé sur un sol avec un apport en nutriment dans la moyenne.\n";
                break;
            case 3:
                conseil += "cette plante a besoin d'être cultivé sur un sol riche en nutriment.\n";
                break;
            default:
                conseil += "une parcelle même très pauvre en nutrient lui suffit pour pousser.\n";
                break;
        }
        scoreTot++;
    }

    private void genComQEau(Plant p)
    {
        switch (p.quantiteEau)
        {
            case 1:
                conseil += "cette plante peut très bien être cultivé sur un sol très sec.\n";
                break;
            case 2:
                conseil += "cette plante a besoin d'être cultivé sur un sol un minimum hydraté.\n";
                break;
            case 3:
                conseil += "les " + p.nom + " ont besoin de beaucoup d'eau pour pousser.\n";
                break;
            default:
                conseil += "une parcelle aride lui conviendrait, cette plante est parfaite en temps de secheresse.\n";
                break;
        }
        scoreTot++;
    }

    private void genComPh(Plant p)
    {
        if (p.phMax >= 7)
            conseil += "une parcelle classique avec un ph neutre lui conviendrait.\n";
        else conseil += "une terre avec un ph acide est ce qui convient le mieux aux" + p.nom + ".\n";
        scoreTot++;
    }
}
