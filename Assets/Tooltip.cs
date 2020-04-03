using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{

    [SerializeField] private GameObject prefabTooltip;
    [SerializeField] private Vector3 coords;
    private Plot plot;
    private GameObject canvas;
    private GameObject myTooltip;

    // Start is called before the first frame update
    void Start()
    {
        InitTooltip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitTooltip()
    {
        this.canvas = GameObject.Find("Tooltips");
        this.plot = this.GetComponent<Plot>();

        // Init panel
        GameObject inst = (GameObject)Instantiate(prefabTooltip);
        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
        inst.transform.SetParent(canvas.transform);
        inst.transform.position = pos + coords;
        inst.transform.localScale = new Vector3(1, 1, 1);
        inst.name = "tooltip_" + this.name;
        myTooltip = inst;

        HideTooltip();
    }

    public void ShowTooltip()
    {
        myTooltip.SetActive(true);
        UpdateData();

    }

    public void HideTooltip()
    {
        myTooltip.SetActive(false);
    }

    public void UpdateData()
    {
        Debug.Log("init data");
        myTooltip.transform.Find("nomPlante").gameObject.GetComponent<Text>().text = plot.GetPlanteName();
        myTooltip.transform.Find("nomTraitement").gameObject.GetComponent<Text>().text = plot.GetTraitementName();
        myTooltip.transform.Find("nomMineral").gameObject.GetComponent<Text>().text = plot.getMineral() == Minerals.None ? " " : plot.getMineral().ToString();

        myTooltip.transform.Find("qttEngrais").gameObject.GetComponent<Text>().text = " ";
        for (int i=0; i < plot.getQNutrition(); i++)
        {
            myTooltip.transform.Find("qttEngrais").gameObject.GetComponent<Text>().text += " +";
        }

        myTooltip.transform.Find("qttEau").gameObject.GetComponent<Text>().text = " ";
        for (int i = 0; i < plot.getQEau(); i++)
        {
            myTooltip.transform.Find("qttEau").gameObject.GetComponent<Text>().text += " +";
        }

        myTooltip.transform.Find("qttPh").gameObject.GetComponent<Text>().text = plot.getPh().ToString();
    }
}
