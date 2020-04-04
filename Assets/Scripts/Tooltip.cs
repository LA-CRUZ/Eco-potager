using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{

    private Plot plot;
    [SerializeField] private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        this.plot = this.GetComponent<Plot>();
        HideTooltip();
    }
    public void ShowTooltip()
    {
        panel.SetActive(true);
        UpdateData();

    }

    public void HideTooltip()
    {
        panel.SetActive(false);
    }

    public void UpdateData()
    {
        panel.transform.Find("nomPlante").gameObject.GetComponent<Text>().text = plot.GetPlanteName();
        panel.transform.Find("nomTraitement").gameObject.GetComponent<Text>().text = plot.GetTraitementName();
        panel.transform.Find("nomMineral").gameObject.GetComponent<Text>().text = plot.getMineral() == Minerals.None ? " " : plot.getMineral().ToString();

        panel.transform.Find("qttEngrais").gameObject.GetComponent<Text>().text = " ";
        for (int i=0; i < plot.getQNutrition(); i++)
        {
            panel.transform.Find("qttEngrais").gameObject.GetComponent<Text>().text += " +";
        }

        panel.transform.Find("qttEau").gameObject.GetComponent<Text>().text = " ";
        for (int i = 0; i < plot.getQEau(); i++)
        {
            panel.transform.Find("qttEau").gameObject.GetComponent<Text>().text += " +";
        }

        panel.transform.Find("qttPh").gameObject.GetComponent<Text>().text = plot.getPh().ToString();
    }
}
