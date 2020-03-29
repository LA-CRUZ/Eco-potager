using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineraux : MonoBehaviour
{
    [SerializeField]
    private float azote = 0f;
    [SerializeField]
    private float phosphore = 0f;
    [SerializeField]
    private float potassium = 0f;

    // Start is called before the first frame update
    void Start()
    {
        float somme = azote + phosphore + potassium;
        if(somme != 100)
        {
            Debug.Log("WARNING : sum of all minerals (" + azote + "," + phosphore + "," + potassium + ") doesn't equal to 100 in gameObject :" + name);
        }
    }


    public float getAzote() { return azote; }

    public float getPhosphore() { return phosphore; }

    public float getPotassium() { return potassium; }

    public void setAzote(float _azote)
    {
        azote += _azote;
        phosphore -= _azote / 2;
        potassium -= _azote / 2;

    }

    public void setPhosphore(float _phosphore)
    {
        azote -= _phosphore /2;
        phosphore += _phosphore;
        potassium -= _phosphore / 2;

    }

    public void setPotassium(float _potassium)
    {
        azote -= _potassium / 2;
        phosphore -= _potassium / 2;
        potassium += _potassium;

    }

    
}
