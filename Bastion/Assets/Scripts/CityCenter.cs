using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CityCenter : MonoBehaviour
{
    public int Radius = 2;
    public int Population;
    public int Food;
    public int Production;
    private int popGrowth;
    public bool Selected;
    private List<Tile> allTiles = new List<Tile>();
    private List<GameObject> Buildings = new List<GameObject>();
    private List<GameObject> inPBuildings = new List<GameObject>();
    public Tilemap baseTiles;
    public TIleMapMaker TMMaker;
    private int grassTiles;
    private int desertTiles;
    private int forestTiles;
    private int waterTiles;
    private int mineTiles;
    private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        // This is for settling tile disputes between cities when a city is spawned or expanded.
        spawnTime = Time.time;
    }

    public void executeTurn()
    {

    }
    // when using the build tab, buildings will be added to the inPBuildins list
    // and then this function will be called with the city's production distributed throughaout the first three buildings in the list.
    public void build()
    {

    }
    private List<Vector2Int> getAllDirections(int radius)
    {
        List<Vector2Int> allDirections = new List<Vector2Int>();
        //int[,] testMap = new int[radius * 4, radius * 4];
        Vector2Int origin = new Vector2Int(radius * 2, radius * 2);
        for (int i = 0; i < radius * 4; i++)
        {
            for (int j = 0; j < radius * 4; j++)
            {
                if(Mathf.Abs(origin.x - i) <= radius || Mathf.Abs(origin.y - j) <= radius)
                {
                    Vector2Int Vec = new Vector2Int(i, j);
                    allDirections.Add(Vec);
                } 
            }
        }
        return allDirections;
    }
    // in progress
    public bool checkTileIntersection(Vector2Int loc)
    {
        return false;
    }
    public void updateTiles()
    {
        GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        int[,] terrainMap = TMMaker.terrainMap;
        List<Vector2Int> directions = getAllDirections(Radius);
        foreach(var direction in directions)
        {
            try
            {
                int tile = terrainMap[direction.x, direction.y];
                switch(tile){
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
            catch
            {
                continue;
            }
        }
    }

    public float getSpawnTime()
    {
        return spawnTime;
    }
}
