using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipInteraction : MonoBehaviour
{
    [SerializeField] private GameObject tooltip;
    [SerializeField] private Vector3 coords;
    private GameObject canvas;
    void Start()
    {
        this.canvas = GameObject.Find("Tooltips");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
            tooltip.transform.SetParent(canvas.transform);
            tooltip.transform.position = pos + coords;
            tooltip.transform.localScale = new Vector3(1, 1, 1);
            tooltip.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            tooltip.SetActive(false);
        }
    }
}
