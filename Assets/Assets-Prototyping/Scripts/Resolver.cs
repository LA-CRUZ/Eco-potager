using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    private List<AnalysePlante> listComPlants = new List<AnalysePlante>();

    private List<GameObject> listPlots = new List<GameObject>();     // on stock des gameObject et non des Plots pour pouvoir savoir pour chaque élément si il a un fils avec getChild
    private List<commentairePlot> commentairePlots = new List<commentairePlot>();

    public int objectifScore; // de 0 à nb parcelles
    private int score = 0;
    private string conseil;
    private List<int> plotScores = new List<int>();
    //Resolver Window Variables
    private float plotButWidth = 47.2f;
    private float plotButHeight = 43.4f;
    private Transform plotDetails;
    public Canvas resolverGUI;
    //-1 si non, 0-9 si oui (0 parcelle de test)
    private int plotSelected;
    public int nbCriteres = 3;
    public int nbPlotRequis = 1;

    void Start()
    {   
        // initialisation de tous ce qui concerne les plantes
        if(listPlantes.Capacity == 0)
        {
            foreach (Plants p in plantes)
            {
                // recuperation des objets Plant stocké dans _Data/...
                listPlantes.Add((Plant)AssetDatabase.LoadAssetAtPath("Assets/_Data/Plantes/" + p + ".asset", typeof(Plant)));
                commentairePlantes.Add("Pour les " + p + " : ");

                // initialisation de l'analysePlant
                listComPlants.Add(new AnalysePlante { plant = p });
            }
        }

        // init de tous ce qui concerne les plots
        foreach (GameObject plot in GameObject.FindGameObjectsWithTag("Plot"))
        {
            listPlots.Add(plot);
            commentairePlots.Add(new commentairePlot());
            plotScores.Add(0);
        }

        //Resolver Window Initilisation
        plotSelected = -1;
        plotDetails = GameObject.Find("SelectedPlotDetails").transform;
        Transform plotsListing = GameObject.Find("PlotsListing").transform;
        for (int i=0; i< GameObject.FindGameObjectsWithTag("Plot").Length; i++)
        {
            GameObject pb = (GameObject)Instantiate(Resources.Load("PlotButton"), plotsListing, false);
            if(i <= 2)
            {
                pb.GetComponent<RectTransform>().localPosition = pb.GetComponent<RectTransform>().localPosition + new Vector3((plotButWidth+14)*(i%3),0,0);
            } else if (i <= 5)
            {
                pb.GetComponent<RectTransform>().localPosition = pb.GetComponent<RectTransform>().localPosition + new Vector3((plotButWidth+14)*(i%3), (plotButHeight + 14)*-1, 0);
            } else if(i <= 8)
            {
                pb.GetComponent<RectTransform>().localPosition = pb.GetComponent<RectTransform>().localPosition + new Vector3((plotButWidth+14)*(i%3), (plotButHeight + 14)*-2, 0);
            }
            pb.transform.GetChild(0).GetComponent<Text>().text = (i+1).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(endStage)
        {
            
            displayPlotDetails();
            //Debug.Break();
        }
        if(Input.GetKey(KeyCode.F))
        {
            endStage = true;
            calculScore();
            affichage();
            AfficherResolverWindow();
        }
    }


    void calculScore()
    {
        Debug.Log("Calcul en cours...");
        int i = 0;
        foreach(GameObject plot in listPlots)
        {
            string tagChild;
            if (plot.transform.childCount > 1)
                tagChild = plot.transform.GetChild(1).tag;
            else if (plot.transform.childCount > 0)
                tagChild = plot.transform.GetChild(0).tag;
            else tagChild = "Untagged";

            if(tagChild == "Untagged")
            {
                commentairePlots[i].setLegume("Aucune");
            } else
            {
                int j = 0;
                foreach (Plant p in listPlantes)
                {
                    AnalysePlante ap = listComPlants[j];
                    if (p.nom == tagChild)
                    {
                        Debug.Log("avant lancement du process pour le legume " + listComPlants[j].plant + " dont le score est actuellement de " + listComPlants[j].scoreTot);
                        Debug.Log("avant lancement du process pour le legume " + ap.plant + " dont le score est actuellement de " + ap.scoreTot);
                        ap.nbPlanter++;
                        plotScores[i] = process(plot.GetComponent<Plot>(), p, i, ap);
                        if (plotScores[i] > 2)
                            score++;
                    }
                    j++;
                }
            }   
            i++;
        }
        // generation des commentaires sur chaques plantes
        AnalysePlante planteNonMaitrise = new AnalysePlante { scoreTot = 0 };
        foreach(AnalysePlante tmp in listComPlants)
        {
            if(tmp.nbPlanter > 0)
            {
                tmp.genCommentaire();
                Debug.Log("pour les " + tmp.plant + " il y a " + tmp.scoreTot);
                Debug.Log("pour les " + planteNonMaitrise.plant + " il y a " + planteNonMaitrise.scoreTot);
                if (planteNonMaitrise.scoreTot <= tmp.scoreTot)
                    planteNonMaitrise = tmp;    // on récupère la plante la moins bien maitrisé sur la partie
            }
        }
        if (planteNonMaitrise.scoreTot != 0)
            conseil = planteNonMaitrise.conseil;
        else conseil = "";  // pas de conseil si le joueur n'a pas commis d'erreur
    }

    int process (Plot plot, Plant p, int indice, AnalysePlante ap)   // créer les commentaires pour 1 plot et son fils
    {
        int nbBonPoints = 0;
        commentairePlots[indice].setLegume(p.nom);
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
            commentairePlots[indice].setSaison("Il s'agit bien d'un légume de saison, c'est super!\n");
            nbBonPoints++;
        }
        else commentairePlots[indice].setSaison("Attention, les " + p.nom + " poussent en " + s + " et non en " + saison + ".\n");

        //  traitement
        string plotTraitement = plot.GetTraitementName();
        bool traitementOk = false;
        foreach(Traitement t in p.listPes)
        {
            if (plotTraitement == t.nom)
            {
                commentairePlots[indice].setTraitement("bien joué tu as appliqué le bon traitement sur cette plante.\n");
                nbBonPoints++;
                traitementOk = true;
            }
        }
        if(!traitementOk)
            commentairePlots[indice].setTraitement("attention tu n'as pas apppliqué le bon traitement!\n");

        //  Humidité
        if (plot.getQEau() < p.quantiteEau)
        {
            commentairePlots[indice].setHydratation("Dommage, la quantité d'eau de cette parcelle est trop faible pour cette plante.\n");
        }
        if (plot.getQEau() == p.quantiteEau)
        {
            commentairePlots[indice].setHydratation("Super, la quantité d'eau est idéale pour cette plante.\n");
            ap.maitriseQEau++;
            nbBonPoints++;
        }
        if (plot.getQEau()  > p.quantiteEau)
            commentairePlots[indice].setHydratation("Fait attention! la quantité d'eau de cette parcelle est trop élevé pour les " + p.nom + "\n");

        // Nutriment

        if (plot.getQNutrition() < p.quantiteNutrition)
            commentairePlots[indice].setEngrais("Dommage, la quantité de nutriments de cette parcelle est trop faible pour cette plante.\n");
        if (plot.getQNutrition() >= p.quantiteNutrition)
        {
            commentairePlots[indice].setEngrais("Super, la quantité de nutriments dans cette parcelle convient bien à cette plante.\n");
            ap.maitriseQNut++;
            nbBonPoints++;
        }

        //  analyse du NPK
        if( plot.getMineral() == p.mineral)
        {
            commentairePlots[indice].setEngrais("Cette parcelle est riche en " + p.mineral.ToString() + ", c'est idéale pour les " + p.nom + ".\n");
            ap.maitriseMin++;
            nbBonPoints++;
        } else
        {
            commentairePlots[indice].setEngrais("Dommage. Cette plante aime les parcelles richent en " + p.mineral.ToString() + " et ce n'est pas le cas de cette parcelle.\n");
        }
        //  analyse du ph
        float phPlot = plot.getPh();
        float phMin = p.phMin;
        float phMax = p.phMax;
        if (phPlot >= phMin && phPlot <= phMax)
        {
            commentairePlots[indice].setPH("Incroyable! Le ph de cette parcelle est parfaite pour cette plante.\n");
            ap.maitrisePh++;
            nbBonPoints++;
        }
        else
        {
            commentairePlots[indice].setPH("Ooooh il semblerait que le ph de cette parcelle ne correspond à celui des " + p.nom + ".\n");
        }
        //commentairePlots[indice] += "\n\n";
        return nbBonPoints;
    }

    void affichage()
    {
        Transform starsGroup = GameObject.Find("starsGroup").transform;
        int nbReussite = 0;
        string appreciation = "";
        foreach (Transform child in starsGroup)
            GameObject.Destroy(child.gameObject);
        for(int i=0; i<plotScores.Count; i++)
        {
            if(plotScores[i] >= nbCriteres)
            {
                Instantiate(Resources.Load("starFull"), starsGroup, false);
                commentairePlots[i].estReussi();
                nbReussite++;
            }
                
        }
        foreach (int plotScore in plotScores)
        {
            if (plotScore < nbCriteres)
                Instantiate(Resources.Load("starEmpty"), starsGroup, false);
        }
        if(nbReussite == plotScores.Count)
        {
            appreciation = "Parfait !";
        } else if(nbReussite == nbPlotRequis)
        {
            appreciation = "Super !";
        } else
        {
            appreciation = "Dommage...";
        }
        GameObject.Find("Appreciation").GetComponent<Text>().text = appreciation;
    }

    public void displayPlotDetails()
    {
        if (plotSelected < 0)
        {
            togglePlotDetails(false);
        }
        else
        {
            togglePlotDetails(true);
            plotDetails.GetChild(2).GetComponent<Text>().text = "Parcelle " + plotSelected;
            plotDetails.GetChild(3).GetChild(0).GetComponent<Text>().text = commentairePlots[plotSelected-1].getLegume() + ". " + commentairePlots[plotSelected - 1].getSaison();
            plotDetails.GetChild(4).GetChild(0).GetComponent<Text>().text = commentairePlots[plotSelected - 1].getHydratation();
            plotDetails.GetChild(5).GetChild(0).GetComponent<Text>().text = commentairePlots[plotSelected - 1].getEngrais();
            plotDetails.GetChild(6).GetChild(0).GetComponent<Text>().text = commentairePlots[plotSelected - 1].getTraitement();
            plotDetails.GetChild(7).GetChild(0).GetComponent<Text>().text = commentairePlots[plotSelected - 1].getPH();
            Transform starPosition = GameObject.Find("starPosition").transform;
            foreach (Transform child in starPosition)
                GameObject.Destroy(child.gameObject);
            if(commentairePlots[plotSelected-1].getReussi())
                Instantiate(Resources.Load("starFull"), starPosition, false);
            else Instantiate(Resources.Load("starEmpty"), starPosition, false);

        }
        GameObject.Find("Tips").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = conseil;



    }

    public void selectPlot(Transform clickedButton)
    {
        int newSelectedPlot = int.Parse(clickedButton.GetChild(0).GetComponent<Text>().text);
        if (plotSelected == newSelectedPlot)
            newSelectedPlot = -1;
        plotSelected = newSelectedPlot;
    }

    void togglePlotDetails(bool plotSelected)
    {
        //Si un plot est select, on affiche les détails, sinon on affiche le msg par défaut (index 1)
        plotDetails.GetChild(1).gameObject.SetActive(!plotSelected);
        for (int i = 2; i < 8; i++)
        {
            plotDetails.GetChild(i).gameObject.SetActive(plotSelected);
        }
    }

    public void CacherResolverWindow()
    {
        if (resolverGUI.GetComponent<Canvas>().enabled == true)
        {
            resolverGUI.GetComponent<Canvas>().enabled = false;
            GameObject.Find("Player").GetComponent<Storage>().enabled = true;
            GameObject.Find("Player").GetComponent<SimpleCharacterControlFree>().enabled = true;
        }
        plotSelected = -1;
    }


    public void AfficherResolverWindow()
    {
        if (resolverGUI.GetComponent<Canvas>().enabled == false)
        {
            resolverGUI.GetComponent<Canvas>().enabled = true;
            GameObject.Find("Player").GetComponent<Storage>().enabled = false;
            GameObject.Find("Player").GetComponent<SimpleCharacterControlFree>().enabled = false;
        }
    }
}

//EEDCC0
//463823
