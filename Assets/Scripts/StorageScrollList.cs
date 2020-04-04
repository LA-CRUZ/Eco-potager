using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageScrollList : MonoBehaviour
{

    public List<Item> itemList;
    public Transform contentPanel;
    public SimpleObjectPool simpleObjectPool;

    // Start is called before the first frame update
    void Start()
    {
        refreshDisplay();
    }

    public void refreshDisplay()
    {
        addButtons();
    }

    private void addButtons()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            Item item = itemList[i];
            GameObject newItem = simpleObjectPool.GetObject();
            newItem.transform.SetParent(contentPanel);

            // On a 3 types d'item donc on a besoin de 3 générateurs différent, c'est un peu dégeu mais on va faire comme ça pour l'instant
            if(newItem.GetComponent<InfoPlante>() != null)
            {
                InfoPlante infos = newItem.GetComponent<InfoPlante>();
                infos.Setup((Plant) item, this);
            }
            else if (newItem.GetComponent<InfoPesticide>() != null)
            {
                InfoPesticide infos = newItem.GetComponent<InfoPesticide>();
                infos.Setup((Traitement) item, this);
            }
            else if (newItem.GetComponent<InfoEngrais>() != null)
            {
                InfoEngrais infos = newItem.GetComponent<InfoEngrais>();
                infos.Setup((Engrais) item, this);
            }
        }
    }
}
