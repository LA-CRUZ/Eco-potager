using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Saison
{
    Eté,
    Automne,
    Hiver,
    Printemps,
    None
}

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class Plant : Item
{
    public List<Saison> saison;
    public Nature sol;
    public List<Traitement> listPes;

    public int quantiteEau;
    public int quantiteNutrition;
    public int quantiteLumière;

    public float phMin;
    public float phMax;

    public float azote;
    public float phosphore;
    public float potassium;
}
