using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{

    public GameObject panel;
    bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        panel.gameObject.SetActive(state);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            state = !state;
            panel.gameObject.SetActive(state);
        }
    }
}

