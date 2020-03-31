using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPlante : MonoBehaviour
{
    public GameObject panel;
    public Button prendre;
    public Button moreInfo;
    public Text nom;
    public Text description;
    public Text saison;
    public Text eau;
    public Text engrais;
    public Text pesticide;
    public Text mineraux;
    public Image iconImage;

    private Plant item;
    private StorageScrollList storageScrollList;
    public bool showMore = false;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Setup(Plant currentItem, StorageScrollList currentScrollList)
    {
        prendre.onClick.AddListener(delegate() { prendreObjet(); });
        moreInfo.onClick.AddListener(delegate () { expandInfos(); });
        this.panel.transform.localScale = new Vector3(1, 1, 1);
        storageScrollList = currentScrollList;
        item = currentItem;
        // Nom de la plante
        nom.text = item.nom;
        
        // Description "le saviez-vous" : desactiver par défaut
        description.text += item.description;
        this.description.enabled = false;
        
        // Icône de la plante
        iconImage.sprite = item.icon;

        // Saisons : désactiver par défaut
        this.saison.enabled = false;
        for (int i = 0; i < item.saison.Count; i++)
        {
            this.saison.text += (i == item.saison.Count - 1) ? item.saison[i].ToString() : item.saison[i].ToString() + ", ";
        }

        // Eau : désactiver par défaut
        this.eau.enabled = false;
        this.eau.text += new string('+', (int) item.quantiteEau);

        // Engrais : désactiver par défaut
        this.engrais.enabled = false;
        this.engrais.text += new string('+', (int)item.quantiteNutrition);

        // Pesticide : désactiver par défaut
        this.pesticide.enabled = false;
        if(item.listPes.Count == 0)
        {
            this.pesticide.text += "Pas nécessaire";
        }
        else
        {
            for (int i = 0; i < item.listPes.Count; i++)
            {
                this.pesticide.text += (i == item.listPes.Count - 1) ? item.listPes[i].nom : item.listPes[i].nom + ", ";
            }
        }

        // Besoins minéraux : désactiver par défaut
        this.mineraux.enabled = false;
        this.mineraux.text += "Azote : " + item.azote + "%, Phosphore : " + item.phosphore + "%, Potassium : " + item.potassium + "%";

        this.GetComponent<LayoutElement>().minHeight = 200;

    }

    private void prendreObjet()
    {
        player.GetComponent<SimpleCharacterControlFree>().SetObjetInHand(item);
    }

    private void expandInfos()
    {
        if (!showMore)
        {
            this.GetComponent<LayoutElement>().minHeight = 500;
            this.description.enabled = true;
            this.saison.enabled = true;
            this.eau.enabled = true;
            this.engrais.enabled = true;
            this.pesticide.enabled = true;
            this.mineraux.enabled = true;
            showMore = true;
        }
        else
        {
            this.GetComponent<LayoutElement>().minHeight = 200;
            this.description.enabled = false;
            this.saison.enabled = false;
            this.eau.enabled = false;
            this.engrais.enabled = false;
            this.pesticide.enabled = false;
            this.mineraux.enabled = false;
            showMore = false;
        }
    }
}
