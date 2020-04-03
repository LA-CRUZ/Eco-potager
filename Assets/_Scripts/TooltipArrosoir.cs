using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipArrosoir : MonoBehaviour
{
    [SerializeField] private GameObject tooltip;
    [SerializeField] private Vector3 coords;
    private GameObject canvas;
    void Start()
    {
        this.canvas = GameObject.Find("Tooltips");

        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
        tooltip.transform.SetParent(canvas.transform);
        tooltip.transform.position = pos + coords;
        tooltip.transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        tooltip.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        tooltip.SetActive(false);
    }
}
