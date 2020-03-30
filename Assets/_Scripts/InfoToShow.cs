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

    private Item item;
    private StorageScrollList storageScrollList;
    public bool showMore = false;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Setup(Item currentItem, StorageScrollList currentScrollList)
    {
        prendre.onClick.AddListener(delegate() { prendreObjet(); });
        moreInfo.onClick.AddListener(delegate () { expandInfos(); });
        Debug.Log("setup");
        this.panel.transform.localScale = new Vector3(1, 1, 1);
        storageScrollList = currentScrollList;
        item = currentItem;
        nom.text = item.name;
        description.text = item.description;
        iconImage.sprite = item.icon;
        Debug.Log("Item:" + item);
        Debug.Log("currentItem:"  + currentItem);

        this.GetComponent<LayoutElement>().minHeight = 200;

    }

    private void prendreObjet()
    {
        Debug.Log("setup");
        Debug.Log(item);
        player.GetComponent<SimpleCharacterControlFree>().SetObjetInHand(item);
    }

    private void expandInfos()
    {
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
