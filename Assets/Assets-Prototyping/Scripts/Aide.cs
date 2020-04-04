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
        if ( Input.GetKeyDown(touche))
        {
            if (isShowing == false)
                genHelp();
            else genHelp();
        } 

        if(Input.anyKey && !Input.GetKey(touche))
        {
            helperUi.SetActive(false);
        }
        
    }

    public void genHelp()
    {
        Item tmp = GameObject.FindGameObjectWithTag("Player").GetComponent<SimpleCharacterControlFree>().getObjetInHand();
        messageAide = "<b>Aide</b> : ";
        if (tmp != null)
        {
            string type = tmp.GetType().ToString();
            Debug.Log(type);
            
            if (type == "Plant")
            {
                messageAide += "tu peux planter les <b>" + tmp.nom + "</b> dans une des parcelles ou bien choisir un autre item.";
            }
            else if (type == "Engrais")
            {
                messageAide += "tu peux appliquer cet engrais en t'approchant suffisement près d'une parcelle.";
            }
            else if(type == "Traitement")
            {
                messageAide += "si tu souhaites appliquer ce traitement sur une parcelle rapproche toi de celle-ci.";
            }
            else if (type == "Item")
            {
                messageAide += "tu peux arroser une plante maintenant en t'approchant d'une parcelle.";
            }
            
        }else
        {
            messageAide += "pour commencer tu peux choisir un élément dans le stock (touche <b>TAB</b>) ou bien voir les caractéristiques d'une parcelle.";
        }
        Debug.Log(messageAide);
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
}
