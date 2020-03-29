using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private bool endStage = false;

    [SerializeField]
    private Plants[] listPlants;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(endStage)
        {
            calculScore();
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
        string resultat = "";
        foreach (Plants p in listPlants)
        {
            resultat += "Pour la plante : " + p + "\n";
            foreach(GameObject plot in GameObject.FindGameObjectsWithTag("Plot"))
            {
                GameObject childOfPlot = plot.transform.GetChild(0).gameObject;
                if (childOfPlot.tag == p.ToString())
                {
                    // exemple avec le ph
                    resultat += "elle a été planté sur le plot : " + plot.name + "\n";
                    float phPlot = plot.GetComponent<Plot>().getPh();
                    float phMin = childOfPlot.GetComponent<Vegetable>().getPhMin();
                    float phMax = childOfPlot.GetComponent<Vegetable>().getPhMax();

                    if(phPlot >= phMin && phPlot <= phMax)
                    {
                        resultat += "Incroyable! Le ph de ce plot est parfait pour les " + p + "." + "\n";
                    } else
                    {
                        resultat += "Ooooh il semblerait que le ph de ce plot ne correspond pas du tous à celui des " + p + ". " + "\n";
                    }

                    // etc...
                }
            }
        }

        Debug.Log(resultat);
    }
}
