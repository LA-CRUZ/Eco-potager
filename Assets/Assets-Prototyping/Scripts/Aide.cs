using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aide : MonoBehaviour
{
    public string messageAide;
    public GameObject helperUi;
    public KeyCode touche = KeyCode.I;
    private bool isShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        helperUi.SetActive(isShowing);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(touche))
        {
            if (isShowing)
            {
                helperUi.SetActive(false);
                isShowing = false;
            }
            else
            {
                genHelp();
                isShowing = true;
            }
        }

    }

    public void genHelp()
    {
        Item tmp = GameObject.FindGameObjectWithTag("Player").GetComponent<SimpleCharacterControlFree>().getObjetInHand();
        messageAide = "<b>Aide</b> : ";
        if (tmp != null)
        {
            string type = tmp.GetType().ToString();
            
            if (type == "Plant")
            {
                string leNom = translate((Plant)tmp);
                messageAide += "Tu peux planter les <b>" + leNom + "</b> dans une des parcelles ou bien choisir un autre élément.";
            }
            else if (type == "Engrais")
            {
                messageAide += "Tu peux appliquer cet engrais en t'approchant suffisement près d'une parcelle.";
            }
            else if(type == "Traitement")
            {
                messageAide += "Si tu souhaites appliquer ce traitement sur une parcelle rapproche toi de celle-ci.";
            }
            else if (type == "Item")
            {
                messageAide += "Tu peux arroser une plante maintenant en t'approchant d'une parcelle.";
            }
        }
        else
        {
            messageAide += "Pour commencer tu peux choisir un élément dans le <b>stock</b> ou bien voir les caractéristiques d'une parcelle en t'approchant de celle-ci.";
        }
        genUi();

    }

    private void genUi()
    {
        GameObject canvas = helperUi.transform.GetChild(0).gameObject;
        GameObject txt = canvas.transform.GetChild(2).gameObject;
        Text t = txt.GetComponent<Text>();
        t.text = messageAide;
        isShowing = !isShowing;
        helperUi.SetActive(isShowing);
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
