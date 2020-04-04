using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoEngrais : MonoBehaviour
{
    public GameObject panel;
    public Button prendre;
    public Button moreInfo;
    public Text nom;
    public Text description;
    public Image iconImage;

    private Engrais item;
    private StorageScrollList storageScrollList;
    public bool showMore = false;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        this.description.enabled = false;
    }

    public void Setup(Engrais currentItem, StorageScrollList currentScrollList)
    {
        prendre.onClick.AddListener(delegate () { prendreObjet(); });
        moreInfo.onClick.AddListener(delegate () { expandInfos(); });
        this.panel.transform.localScale = new Vector3(1, 1, 1);
        storageScrollList = currentScrollList;
        item = currentItem;
        // Nom de l'engrais
        nom.text = item.nom;
        // Description 
        description.text = item.description;
        // Icône
        iconImage.sprite = item.icon;

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
            showMore = true;
        }
        else
        {
            this.GetComponent<LayoutElement>().minHeight = 200;
            this.description.enabled = false;
            showMore = false;
        }
    }
}
