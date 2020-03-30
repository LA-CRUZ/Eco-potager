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

public class Manager : MonoBehaviour
{
    private bool endStage = false;

    [SerializeField]
    private Plants[] plantes;

    public List<Plant> listPlantes;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        if(listPlantes.Capacity == 0)
        {
            foreach (Plants p in plantes)
            {
                listPlantes.Insert( i , (Plant)AssetDatabase.LoadAssetAtPath("Assets/_Data/Plant/" + p + ".asset", typeof(Plant)));
                i++;
            }
        }
        
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
        foreach (Plant p in listPlantes)
        {
            resultat += "Pour la plante : " + p.nom + "\n";
            foreach(GameObject plot in GameObject.FindGameObjectsWithTag("Plot"))
            {
                GameObject childOfPlot = plot.transform.GetChild(0).gameObject;
                if (childOfPlot.tag == p.nom)
                {
                    // exemple avec le ph
                    resultat += "elle a été planté sur le plot : " + plot.name + "\n";
                    float phPlot = plot.GetComponent<Plot>().getPh();
                    float phMin = p.phMin;
                    float phMax = p.phMax;

                    if(phPlot >= phMin && phPlot <= phMax)
                    {
                        resultat += "Incroyable! Le ph de ce plot est parfait pour les " + p.nom + "." + "\n";
                    } else
                    {
                        resultat += "Ooooh il semblerait que le ph de ce plot ne correspond pas du tous à celui des " + p.nom + ". " + "\n";
                    }

                    // etc...
                }
            }
        }

        Debug.Log(resultat);
    }
}
