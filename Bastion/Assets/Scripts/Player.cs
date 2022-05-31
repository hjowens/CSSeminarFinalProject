using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public CityTileManager CityManager;
    public int Selected = 0;
    public UITab Panel;
    public Tilemap buildingMap;
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Selected = 0;
        }
        else if (Input.GetMouseButtonDown(0) && Panel.CheckMouseOverTab() == false)
        {
            mouseClicked();
        }
    }

    private void mouseClicked()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int CellPos = buildingMap.WorldToCell(mousePos);
        
        switch (Selected)
        {
            case (int)BuildingTypes.cityCenter:
                CityManager.addBuilding((int)BuildingTypes.cityCenter, CellPos);
                break;
            case (int)BuildingTypes.ironMine:
                CityManager.addBuilding((int)BuildingTypes.ironMine, CellPos);
                break;
            case (int)BuildingTypes.stoneMine:
                CityManager.addBuilding((int)BuildingTypes.stoneMine, CellPos);
                break;
            case (int)BuildingTypes.lumberYard:
                CityManager.addBuilding((int)BuildingTypes.lumberYard, CellPos);
                break;
            case (int)BuildingTypes.farm:
                CityManager.addBuilding((int)BuildingTypes.farm, CellPos);
                break;
            case (int)BuildingTypes.taxOffice:
                CityManager.addBuilding((int)BuildingTypes.taxOffice, CellPos);
                break;
            case (int)BuildingTypes.publicForum:
                CityManager.addBuilding((int)BuildingTypes.publicForum, CellPos);
                break;
        }
    }

    public void SelectBuilding(string ID)
    {
        switch (ID)
        {
            case "CityCenter":
                Selected = (int)BuildingTypes.cityCenter;
                break;
            case "IronMine":
                Selected = (int)BuildingTypes.ironMine;
                break;
            case "StoneMine":
                Selected = (int)BuildingTypes.stoneMine;
                break;
            case "LumberYard":
                Selected = (int)BuildingTypes.lumberYard;
                break;
            case "TaxOffice":
                Selected = (int)BuildingTypes.taxOffice;
                break;
            case "Farm":
                Selected = (int)BuildingTypes.farm;
                break;
            case "PublicForum":
                Selected = (int)BuildingTypes.publicForum;
                break;
        }
    }
}