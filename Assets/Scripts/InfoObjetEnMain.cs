using UnityEngine;
using UnityEngine.UI;

public class InfoObjetEnMain : MonoBehaviour
{

    public Image image;
    public Text nom;
    public Item itemEnMain;
    public Image backgroundText;
    // Start is called before the first frame update
    void Start()
    {
        RetirerObjet();
    }

    public void SetObjet(Item item)
    {
        itemEnMain = item;
        this.image.enabled = true;
        this.nom.enabled = true;
        this.backgroundText.enabled = true;
        this.nom.text = item.nom;
        this.image.sprite = item.icon;
    }

    public void RetirerObjet()
    {
        itemEnMain = null;
        this.image.enabled = false;
        this.backgroundText.enabled = false;
        this.nom.enabled = false;
    }
}
