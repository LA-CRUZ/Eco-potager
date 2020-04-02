﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class DialogueScript : MonoBehaviour
{
    public Canvas dialoguesGUI;
    public GameObject player;
    public List<String> texte;
    public GameObject plantPanel;

    private int nbEtape;
    private bool tutorielFini;
    private int indexPhrase;

    // Start is called before the first frame update
    void Start()
    {
        nbEtape = 0;
        tutorielFini = false;
        indexPhrase = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorielFini)
            tutoriel();
    }

    void tutoriel()
    {
        Text dialogueText = dialoguesGUI.GetComponentInChildren<Text>();
        Storage stock = player.GetComponent<Storage>();
        InfoPlante ip = plantPanel.GetComponentInChildren<InfoPlante>();
        Plot plot = GameObject.FindGameObjectWithTag("Plot").GetComponent<Plot>();
        switch (nbEtape)
        {
            case 0: //début du tuto
                AfficherDialogue();
                if (dialogueText.text == "New Text")
                    dialogueText.text = texte[0];
                if (Input.GetKeyDown(KeyCode.E) && indexPhrase == 4)
                    nbEtape++;
                if (Input.GetKeyDown(KeyCode.E) && indexPhrase < 4)
                {
                    dialogueText.text = texte[indexPhrase];
                    indexPhrase++;
                }
                break;
            case 1: //le joueur a ouvert le stock
                CacherDialogue();
                if(stock.state)
                {
                    AfficherDialogue();
                    if(indexPhrase == 4)
                    {
                        dialogueText.text = texte[indexPhrase];
                        indexPhrase++;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && indexPhrase == 7)
                        nbEtape++;
                    if (Input.GetKeyDown(KeyCode.E) && indexPhrase < 7)
                    {
                        dialogueText.text = texte[indexPhrase];
                        indexPhrase++;
                    }
                }
                break;
            case 2: //le joueur a cliqué sur "plus d'infos"
                CacherDialogue();
                if(ip.showMore)
                {
                    AfficherDialogue();
                    if(indexPhrase == 7)
                    {
                        dialogueText.text = texte[indexPhrase];
                        indexPhrase++;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && indexPhrase == 13)
                        nbEtape++;
                    if (Input.GetKeyDown(KeyCode.E) && indexPhrase < 13)
                    {
                        dialogueText.text = texte[indexPhrase];
                        indexPhrase++;
                    }
                }
                break;
            case 3: //le joueur a appliqué l'engrais
                CacherDialogue();
                if (plot.getQNutrition() == 1)
                {
                    AfficherDialogue();
                    if (indexPhrase == 13)
                    {
                        dialogueText.text = texte[indexPhrase];
                        indexPhrase++;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && indexPhrase == 14)
                        nbEtape++;
                }
                break;
            case 4: //le joueur a réappliqué l'engrais
                CacherDialogue();
                if (plot.getQNutrition() == 3)
                {
                    AfficherDialogue();
                    if (indexPhrase == 14)
                    {
                        dialogueText.text = texte[indexPhrase];
                        indexPhrase++;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && indexPhrase == 15)
                        nbEtape++;
                }
                break;
            case 5: //le joueur a planté les tomates
                CacherDialogue();
                if (GameObject.FindGameObjectWithTag("Plot").transform.childCount > 1)
                {
                    AfficherDialogue();
                    if (indexPhrase == 15)
                    {
                        dialogueText.text = texte[indexPhrase];
                        indexPhrase++;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && indexPhrase == 17)
                        nbEtape++;
                    if (Input.GetKeyDown(KeyCode.E) && indexPhrase < 17)
                    {
                        dialogueText.text = texte[indexPhrase];
                        indexPhrase++;
                    }
                }
                break;
            case 6: //le joueur a arrosé les tomates
                CacherDialogue();
                if (plot.getQEau() == 2)
                {
                    AfficherDialogue();
                    if (indexPhrase == 17)
                    {
                        dialogueText.text = texte[indexPhrase];
                        indexPhrase++;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && indexPhrase == 20)
                        nbEtape++;
                    if (Input.GetKeyDown(KeyCode.E) && indexPhrase < 20)
                    {
                        dialogueText.text = texte[indexPhrase];
                        indexPhrase++;
                    }
                }
                break;
            case 7: //le joueur a appliqué le traitement
                CacherDialogue();
                if (true) //TODO
                {
                    AfficherDialogue();
                    if (indexPhrase == 20)
                    {
                        dialogueText.text = texte[indexPhrase];
                        indexPhrase++;
                    }
                    if (Input.GetKeyDown(KeyCode.E) && indexPhrase == 23)
                        nbEtape++;
                    if (Input.GetKeyDown(KeyCode.E) && indexPhrase < 23)
                    {
                        dialogueText.text = texte[indexPhrase];
                        indexPhrase++;
                    }
                }
                break;
            default:
                CacherDialogue();
                tutorielFini = true;
                break;
        }

        
    }

    public void CacherDialogue()
    {
        if (dialoguesGUI.GetComponent<Canvas>().enabled == true)
        {
            dialoguesGUI.GetComponent<Canvas>().enabled = false;
            player.GetComponent<Storage>().enabled = true;
            player.GetComponent<SimpleCharacterControlFree>().enabled = true;
        }
    }
        

    public void AfficherDialogue()
    {
        if(dialoguesGUI.GetComponent<Canvas>().enabled == false)
        {
            dialoguesGUI.GetComponent<Canvas>().enabled = true;
            player.GetComponent<Storage>().enabled = false;
            player.GetComponent<SimpleCharacterControlFree>().enabled = false;
        }
    }

}
