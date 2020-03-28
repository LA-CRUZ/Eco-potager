using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoToShow : MonoBehaviour
{
    public GameObject panel;
    public Button prendre;
    public Button moreInfo;
    public Text nom;
    public Text description;
    public Image iconImage;

    private Plant plante;
    private StorageScrollList storageScrollList;
    public bool showMore = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Setup(Plant currentPlant, StorageScrollList currentScrollList)
    {
        prendre.onClick.AddListener(delegate() { prendreObjet(); });
        moreInfo.onClick.AddListener(delegate () { expandInfos(); });

        this.panel.transform.localScale = new Vector3(1, 1, 1);
        storageScrollList = currentScrollList;
        plante = currentPlant;
        nom.text = plante.name;
        description.text = plante.description;
        iconImage.sprite = plante.icon;

        this.GetComponent<LayoutElement>().minHeight = 200;

    }

    private void prendreObjet()
    {
        Debug.Log("prendre !");
    }

    private void expandInfos()
    {
        Debug.Log("expand !");
        //this.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 500);
        if (!showMore)
        {
            this.GetComponent<LayoutElement>().minHeight = 500;
            showMore = true;
        }
        else
        {
            this.GetComponent<LayoutElement>().minHeight = 200;
            showMore = false;
        }
    }
}
