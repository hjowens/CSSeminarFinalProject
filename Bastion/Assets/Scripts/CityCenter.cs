using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCenter : MonoBehaviour
{
    public int Radius = 5;
    public int Population;
    public int Food;
    public int Production;
    private int popGrowth;
    public bool Selected;
    private List<GameObject> Buildings = new List<GameObject>();
    private List<GameObject> inPBuildings = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void executeTurn()
    {

    }
    // when using the build tab, buildings will be added to the inPBuildins list
    // and then this function will be called with the city's production distributed throughaout the first three buildings in the list.
    public void build()
    {

    }
}
