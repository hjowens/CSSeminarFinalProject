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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void executeTurn()
    {

    }
}
