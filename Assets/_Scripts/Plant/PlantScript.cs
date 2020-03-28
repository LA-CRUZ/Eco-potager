using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class Plant : ScriptableObject
{
    public string name;
    public string description;
    public string saison;
    public Sprite icon;

}
