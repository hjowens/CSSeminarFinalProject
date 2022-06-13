using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CityCenter : MonoBehaviour
{
    /*
    This class is quite obviously unfinished. 
    */
    public int Radius = 2;
    public int Population;
    public int Food;
    public int Production;
    public int Iron;
    public int Stone;
    private int popGrowth;
    public bool Selected;
    private List<Tile> allTiles = new List<Tile>();
    private List<GameObject> Buildings = new List<GameObject>();
    private List<Building> inPBuildings = new List<Building>();
    public Tilemap baseTiles;
    public TIleMapMaker TMMaker;
    private int grassTiles;
    private int desertTiles;
    private int forestTiles;
    private int waterTiles;
    private int mineTiles;
    public int cityID;
    //public GameObject Controller;
    public CityTileManager cityTileManager;
    private int[,] cityMap;
    private int[,] buildingMap;
    // Start is called before the first frame update
    void Start()
    {
        cityMap = cityTileManager.cityMap;
        buildingMap = cityTileManager.buildingMap;
        cityID = cityTileManager.currentCityID;
        updateTiles();
    }

    public void executeTurn()
    {

    }

    // when using the build tab, buildings will be added to the inPBuildins list
    // and then this function will be called with the city's production given to the first building in the list.
    public void build()
    {

    }
    // returns a list of all vectors to hexagonal tiles a certain radius away when updating tiles.
    private List<Vector2Int> getAllDirections(int radius)
    {
        List<Vector2Int> allDirections = new List<Vector2Int>();
        //int[,] testMap = new int[radius * 4, radius * 4];
        Vector2Int origin = new Vector2Int(radius * 2, radius * 2);
        for (int i = 0; i < radius * 4; i++)
        {
            for (int j = 0; j < radius * 4; j++)
            {
                if(Mathf.Abs(origin.x - i) <= radius && Mathf.Abs(origin.y - j) <= radius)
                {
                    Vector2Int Vec = new Vector2Int(origin.x - i, origin.y - j);
                    allDirections.Add(Vec);
                } 
            }
            
        }
        /*
        foreach (var direciton in allDirections)
        {
            Debug.Log(direciton);
        }
        */
        return allDirections;
    }
    // supposed to check whether or not there is already a building in that tile spot it's not finished
    public bool checkTileIntersection(Vector2Int loc)
    {
        return false;
    }
    public void updateTiles()
    {
        /*
        This is supposed to be called to determine how many of each tile type a city has in its radius.
        The counted tile types would then be converted to resources generated every turn.
        */
        GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        Vector3Int cellPos = gridLayout.WorldToCell(transform.position);
        int[,] terrainMap = TMMaker.terrainMap;
        List<Vector2Int> directions = getAllDirections(Radius);
        grassTiles = 0;
        waterTiles = 0;
        desertTiles = 0;
        forestTiles = 0;
        mineTiles = 0;
        foreach(var direction in directions)
        {
            try
            {
                int tile = terrainMap[cellPos.x + direction.x, cellPos.y + direction.y];
                int tileID = cityMap[cellPos.x + direction.x, cellPos.y + direction.y];
                // checks whether this tile is already owned by this city or another city or whether it's not owned at all
                if (tileID == cityID || tileID == 0)
                {
                    if(tileID == 0)
                    {
                        cityMap[cellPos.x + direction.x, cellPos.y + direction.y] = cityID;
                        //Debug.Log(cityMap[cellPos.x + direction.x, cellPos.y + direction.y]);
                    }
                    switch (tile)
                    {
                        case (int)Types.grass:
                            grassTiles += 1;
                            break;
                        case (int)Types.forest:
                            forestTiles += 1;
                            break;
                        case (int)Types.desert:
                            desertTiles += 1;
                            break;
                        case (int)Types.water:
                            waterTiles += 1;
                            break;
                        case (int)Types.grassiron:
                            mineTiles += 1;
                            break;
                        case (int)Types.desertiron:
                            mineTiles += 1;
                            break;
                        case (int)Types.grassstone:
                            mineTiles += 1;
                            break;
                        case (int)Types.desertstone:
                            mineTiles += 1;
                            break;
                    }
                }
            }
            catch
            {
                continue;
            }
        }
    }

    public float getCityID()
    {
        return cityID;
    }
}
