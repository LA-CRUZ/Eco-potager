using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Engrais", menuName = "Engrais")]
public class Engrais : ScriptableObject
{
    public string nom = "Engrais riche en ...";
    public string description = "donner un exemple sur comment faire tel ou tel engrais";
    public float azote;
    public float phosphore;
    public float potassium;

    public Sprite icon;
}
