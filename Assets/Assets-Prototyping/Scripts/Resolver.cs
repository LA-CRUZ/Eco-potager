using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum Plants
{
    Carotte,
    ChouFleur,
    Celerie,
    Cocombre,
    Laitue,
    Melon,
    PommeDeTerre,
    Tomate,
    Navet
};

public class Resolver : MonoBehaviour
{
    private bool endStage = false;

    [SerializeField]
    private Plants[] plantes;
    public List<Plant> listPlantes = new List<Plant>(); 
    private List<string> commentairePlantes = new List<string>();

    private List<GameObject> listPlots = new List<GameObject>();     // on stock des gameObject et non des Plots pour pouvoir savoir pour chaque élément si il a un fils avec getChild
    private List<string> commentairePlots = new List<string>();

    public int objectifScore; // de 0 à nb parcelles
    private int score = 0;
    private string commentaireScore;

    void Start()
    {   
        // initialisation de tous ce qui concerne les plantes
        if(listPlantes.Capacity == 0)
        {
            foreach (Plants p in plantes)
            {
                listPlantes.Add((Plant)AssetDatabase.LoadAssetAtPath("Assets/_Data/Plant/" + p + ".asset", typeof(Plant)));
                commentairePlantes.Add("Pour les " + p + " : ");
            }
        }

        // init de tous ce qui concerne les plots
        foreach (GameObject plot in GameObject.FindGameObjectsWithTag("Plot"))
        {
            listPlots.Add(plot);
            commentairePlots.Add( "Pour  " + plot.name + " : ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(endStage)
        {
            calculScore();
            affichage();
            Debug.Break();
        }
        if(Input.anyKey)
        {
            endStage = true;
        }
    }


    void calculScore()
    {
        Debug.Log("Calcul en cours...");
        int i = 0;
        foreach(GameObject plot in listPlots)
        {
            if (plot.transform.childCount != 0)
            {
                string tagChild = plot.transform.GetChild(0).tag;
                foreach(Plant p in listPlantes)
                {
                    if(p.nom == tagChild)
                    {
                        if (process(plot.GetComponent<Plot>(), p, i))
                            score++;
                    }
                }
            } else
            {
                commentairePlots[i] = "Aucune plante n'a été planté sur cette parcelle. \n";
                Debug.Log(commentairePlots[i]);
            }
            i++;
        }
    }

    bool process (Plot plot, Plant p, int indice)   // créer les commentaires pour 1 plot et son fils
    {
        int nbBonPoints = 0;
        commentairePlots[indice] = "Des " + p.nom + " ont été planté ici. \n ";
        //  analyse type de sol
        if (p.sol != Nature.None)
        {
            if(p.sol == plot.sol)
            {
                commentairePlots[indice] += "Bien joué! Effectivement un sol " + plot.sol + " correspond mieux aux " + p.nom + ". \n";
                nbBonPoints++;
            }
        }   // pas de remarque si la plante peut pousser sur tous type de terrains.

        //  analyse des taux

        //  Humidité
        if (plot.getTauxHum() - 10 < p.tauxHum)
            commentairePlots[indice] += "Dommage, le taux d'humidité de cette parcelle est trop faible pour cette plante.\n";
        if (plot.getTauxHum() - 10 <= p.tauxHum && plot.getTauxHum() + 10 >= p.tauxHum)
        {
            commentairePlots[indice] += "Super, le taux d'humidité est idéale pour cette plante.\n";
            nbBonPoints++;
        }
        if (plot.getTauxHum() + 10 > p.tauxHum)
            commentairePlots[indice] += "Fait attention! Le taux d'humidité de cette parcelle est trop élevé pour les " + p.nom + "\n";

        // Nutriment

        if (plot.getTauxNut() - 10 < p.tauxNut)
            commentairePlots[indice] += "Dommage, le taux de nutriments de cette parcelle est trop faible pour cette plante.\n";
        if (plot.getTauxNut() - 10 >= p.tauxNut)
        {
            commentairePlots[indice] += "Super, le taux de nutriments dans cette parcelle convient bien à cette plante.\n";
            nbBonPoints++;
        }

        //  analyse du NPK
        string nutPlantePrincipale;
        string nutPlotPrincipale;
        if (p.azote == 60)
        {
            nutPlantePrincipale = "azote";
        }
        else if (p.phosphore == 60)
        {
            nutPlantePrincipale = "phosphore";
        }
        else nutPlantePrincipale = "potassium";

        if (plot.getAzote() == 60)
        {
            nutPlotPrincipale = "azote";
        }
        else if (plot.getPhosphore() == 60)
        {
            nutPlotPrincipale = "phosphore";
        }
        else nutPlotPrincipale = "potassium";

        if (nutPlantePrincipale == nutPlotPrincipale)
        {
            commentairePlots[indice] += "Parfait! Cette parcelle est riche en " + nutPlotPrincipale + " c'est exactement ce que veut les " + p.nom + ".\n";
            nbBonPoints++;
        }
        else commentairePlots[indice] += "Cette parcelle ne contient pas assez de  " + nutPlantePrincipale + " pour les " + p.nom + " c'est dommage.\n";
        //  analyse du ph
        float phPlot = plot.getPh();
        float phMin = p.phMin;
        float phMax = p.phMax;
        if (phPlot >= phMin && phPlot <= phMax)
        {
            commentairePlots[indice] += "Incroyable! Le ph de cette parcelle est parfait pour cette plante.\n";
            nbBonPoints++;
        }
        else
        {
            commentairePlots[indice] += "Ooooh il semblerait que le ph de cette parcelle ne correspond à celui des " + p.nom + ".\n\n";
        }
        return (nbBonPoints > 2);
    }

    void affichage()
    {
        string concat = "Le score est de : " + score + "/" + listPlots.Count + ".";
        if (score < objectifScore)
        {
            concat += "L'objectif n'a pas été atteint. Tant pis, regardons en détails ce qui c'est passé.\n";
        }
        else concat += "Bravo! Tu as atteint l'objectif qui était de bien gérer " + objectifScore + " parcelles sur " + listPlots.Count + ".\n";
        foreach (string str in commentairePlots)
        {
            Debug.Log(str);
            concat += str;
        }
        Debug.Log(concat);
    }
}
