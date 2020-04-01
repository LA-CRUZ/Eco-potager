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
    private Saison saison;

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
        if(Input.GetKey(KeyCode.F))
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
            string tagChild;
            if(plot.transform.childCount > 1)
                tagChild = plot.transform.GetChild(1).tag;
            else tagChild = plot.transform.GetChild(0).tag;

            if(tagChild == "Untagged")
            {
                commentairePlots[i] = "Aucune plante n'a été planté sur cette parcelle. \n";
            } else
            {
                foreach (Plant p in listPlantes)
                {
                    if (p.nom == tagChild)
                    {
                        if (process(plot.GetComponent<Plot>(), p, i))
                            score++;
                    }
                }
            }   
            i++;
        }
    }

    bool process (Plot plot, Plant p, int indice)   // créer les commentaires pour 1 plot et son fils
    {
        int nbBonPoints = 0;
        commentairePlots[indice] = "Des " + p.nom + " ont été planté ici. \n ";
        // analyse de la saison
        bool estDeSaison = false;
        Saison s = Saison.None;
        foreach(Saison tmp in p.saison)
        {
            s = tmp;
            if (tmp == saison || tmp == Saison.None)
                estDeSaison = true;
        }
        if (estDeSaison)
        {
            commentairePlots[indice] += "Il s'agit bien d'un légume de saison, c'est super!\n";
            nbBonPoints++;
        }
        else commentairePlots[indice] += "Attention, les " + p.nom + " poussent en " + s + " et non en " + saison + ".\n";

        //  analyse des taux

        //  Humidité
        if (plot.getQEau() < p.quantiteEau)
            commentairePlots[indice] += "Dommage, la quantité d'eau de cette parcelle est trop faible pour cette plante.\n";
        if (plot.getQEau() == p.quantiteEau)
        {
            commentairePlots[indice] += "Super, la quantité d'eau est idéale pour cette plante.\n";
            nbBonPoints++;
        }
        if (plot.getQEau()  > p.quantiteEau)
            commentairePlots[indice] += "Fait attention! la quantité d'eau de cette parcelle est trop élevé pour les " + p.nom + "\n";

        // Nutriment

        if (plot.getQNutrition() < p.quantiteNutrition)
            commentairePlots[indice] += "Dommage, la quantité de nutriments de cette parcelle est trop faible pour cette plante.\n";
        if (plot.getQNutrition() >= p.quantiteNutrition)
        {
            commentairePlots[indice] += "Super, la quantité de nutriments dans cette parcelle convient bien à cette plante.\n";
            nbBonPoints++;
        }

        //  analyse du NPK

        //  analyse du ph
        float phPlot = plot.getPh();
        float phMin = p.phMin;
        float phMax = p.phMax;
        if (phPlot >= phMin && phPlot <= phMax)
        {
            commentairePlots[indice] += "Incroyable! Le ph de cette parcelle est parfaite pour cette plante.\n";
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
            concat += str;
        }
        Debug.Log(concat);
    }
}
