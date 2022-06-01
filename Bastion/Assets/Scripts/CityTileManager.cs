using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

enum BuildingTypes
{
    cityCenter = 1,
    ironMine = 2,
    stoneMine = 3,
    farm = 4,
    lumberYard = 5,
    taxOffice = 6,
    publicForum = 7,
}
public class CityTileManager : MonoBehaviour
{
    public int[,] cityMap;
    public int[,] buildingMap;
    public int[,] terrainMap;
    private int width;
    private int height;
    public GameObject terrainGrid;
    public TIleMapMaker TMMaker;
    public int currentCityID = 1;
    public Tile cityCenter;
    public Tile grassIronMine;
    public Tile grassStoneMine;
    public Tile desertIronMine;
    public Tile desertStoneMine;
    public Tile farm;
    public Tile lumberYard;
    public Tile taxOffice;
    public Tile publicForum;
    public Tilemap buildingTiles;
    public GameObject CityCenter;
    public GameObject Mine;
    public GameObject Farm;
    public GameObject LumberYard;
    public GameObject TaxOffice;
    public GameObject PublicForum;

    // Start is called before the first frame update
    void Start()
    {
        width = TMMaker.height;
        height = TMMaker.width;
        cityMap = new int[height, width];
        buildingMap = new int[height, width];
    }

    public void addBuilding(int type, Vector3Int position)
    {
        //Debug.Log(position);
        position = new Vector3Int(position.x, position.y);
        //Debug.Log(position);
        Vector3 worldPos = buildingTiles.CellToWorld(position);
        if (cityMap[position.x, position.y] != 0)
        {
            switch (type)
            {
                case (int)BuildingTypes.ironMine:
                    buildingMap[position.x, position.y] = (int)BuildingTypes.ironMine;
                    break;
                case (int)BuildingTypes.stoneMine:
                    buildingMap[position.x, position.y] = (int)BuildingTypes.stoneMine;
                    break;
                case (int)BuildingTypes.farm:
                    buildingMap[position.x, position.y] = (int)BuildingTypes.farm;
                    break;
                case (int)BuildingTypes.lumberYard:
                    buildingMap[position.x, position.y] = (int)BuildingTypes.lumberYard;
                    break;
                case (int)BuildingTypes.taxOffice:
                    buildingMap[position.x, position.y] = (int)BuildingTypes.taxOffice;
                    break;
                case (int)BuildingTypes.publicForum:
                    buildingMap[position.x, position.y] = (int)BuildingTypes.publicForum;
                    break;
            }
        }
        else if(cityMap[position.x, position.y] == 0 && type == (int)BuildingTypes.cityCenter)
        {
            buildingMap[position.x, position.y] = (int)BuildingTypes.cityCenter;
            GameObject newCity = Instantiate(CityCenter, worldPos, transform.rotation, transform);
            newCity.GetComponent<CityCenter>().cityTileManager = GetComponent<CityTileManager>();
            newCity.GetComponent<CityCenter>().cityID = currentCityID;
            newCity.GetComponent<CityCenter>().TMMaker = TMMaker;
            currentCityID += 1;
        }
        translateBuildingMap();
    }

    public void translateBuildingMap()
    {
        terrainMap = TMMaker.terrainMap;
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                int coord = buildingMap[x, y];
                switch (coord)
                {
                    case (int)BuildingTypes.cityCenter:
                        buildingTiles.SetTile(new Vector3Int (x, y, 0), cityCenter);
                        break;
                    
                    case (int)BuildingTypes.ironMine:
                        if (terrainMap[x, y] == (int)Types.grassiron)
                        {
                            //Debug.Log("2");
                            buildingTiles.SetTile(new Vector3Int(x, y, 0), grassIronMine);
                        }
                        else if(terrainMap[x, y] == (int)Types.desertiron)
                        {
                            buildingTiles.SetTile(new Vector3Int(x, y, 0), desertIronMine);
                        }
                        
                        break;
                    case (int)BuildingTypes.stoneMine:
                        if (terrainMap[x, y] == (int)Types.grassstone)
                        {
                            buildingTiles.SetTile(new Vector3Int(x, y, 0), grassStoneMine);
                        }
                        else if (terrainMap[x, y] == (int)Types.desertstone)
                        {
                            buildingTiles.SetTile(new Vector3Int(x, y, 0), desertStoneMine);
                        }
                        break;
                    
                    case (int)BuildingTypes.farm:
                        buildingTiles.SetTile(new Vector3Int(x, y, 0), farm);
                        break;
                    case (int)BuildingTypes.lumberYard:
                        buildingTiles.SetTile(new Vector3Int(x, y, 0), lumberYard);
                        break;
                    case (int)BuildingTypes.taxOffice:
                        buildingTiles.SetTile(new Vector3Int(x, y, 0), taxOffice);
                        break;
                    case (int)BuildingTypes.publicForum:
                        buildingTiles.SetTile(new Vector3Int(x, y, 0), publicForum);
                        break;
                }
                /*
                if (coord == (int)BuildingTypes.ironMine)
                {
                    Debug.Log("IronMine");
                    Debug.Log(terrainMap[x, y]);
                    if (terrainMap[x, y] == (int)Types.grassiron)
                    {
                        Debug.Log("IronMine2");
                        buildingTiles.SetTile(new Vector3Int(x, y, 0), grassIronMine);
                    }
                    else if (terrainMap[x, y] == (int)Types.desertiron)
                    {
                        buildingTiles.SetTile(new Vector3Int(x, y, 0), desertIronMine);
                    }
                }
                else if(coord == (int)BuildingTypes.stoneMine)
                {
                    if (terrainMap[x, y] == (int)Types.grassstone)
                    {
                        buildingTiles.SetTile(new Vector3Int(x, y, 0), grassStoneMine);
                    }
                    else if (terrainMap[x, y] == (int)Types.desertstone)
                    {
                        buildingTiles.SetTile(new Vector3Int(x, y, 0), desertStoneMine);
                    }
                }
                */
            }
        }
    }
}
