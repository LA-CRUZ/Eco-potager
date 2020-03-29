using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{

    [NonSerialized]
    public Caracteristique caract;

    [SerializeField]
    private float ph;

    public float getPh() { return ph; }

    public void setPh(float _ph) { ph = (_ph >= 0f && _ph <= 14f) ? _ph : ph; }   // si le ph passé en param est incompatible on ne fait rien 

    public void addToPh(float _ph)
    {
        if (_ph >= 0f && _ph <= 14f)
        {
            if (ph + _ph > 14f)
                ph = 14f;
            else if (ph + _ph < 0f)
                ph = 0f;
            else ph += _ph;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }
}
