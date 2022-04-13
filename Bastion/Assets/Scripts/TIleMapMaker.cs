using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TIleMapMaker : MonoBehaviour
{
    public int width;
    public int height;
    public int[,] terrainMap;
    public Tile Grass;
    public Tile Mountain;
    public Tile Forest;
    public Tile Desert;
    public Tile Water;
    public Tilemap LandTiles;
    [Range(1, 5)]
    public int grassThresh = 2;
    [Range(1, 5)]
    public int mountainThresh = 3;
    [Range(1, 5)]
    public int forestThresh = 3;
    [Range(1, 5)]
    public int desertThresh = 3;
    [Range(1, 10)]
    public int Runs;
    int[,] directions = new int[,] { {-1, 0}, {-1, 1}, {0, -1}, {0, 1}, {1, 0}, {1, 1} };
    // Start is called before the first frame update
    void Start()
    {
        createInitialMap();
        runSim();
        translateMap();
    }
    private void createInitialMap()
    {
        terrainMap = new int[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float seed = Random.Range(0, 100);
                switch (seed)
                {
                    case <= 20:
                        terrainMap[i, j] = 1;
                        break;
                    case > 20 and <= 30:
                        terrainMap[i, j] = 2;
                        break;
                    case > 30 and <= 40:
                        terrainMap[i, j] = 3;
                        break;
                    case > 40 and <= 50:
                        terrainMap[i, j] = 4;
                        break;
                    case > 50:
                        terrainMap[i, j] = 0;
                        break;
                }
            }
        }
    }
    private void runSim()
    {
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                int changeCoord = checkCoord(x, y);
                switch (changeCoord)
                {
                    case 0:
                        terrainMap[x, y] = 0;
                        break;
                    case 1:
                        terrainMap[x, y] = 1;
                        break;
                    case 2:
                        terrainMap[x, y] = 2;
                        break;
                    case 3:
                        terrainMap[x, y] = 3;
                        break;
                    case 4:
                        terrainMap[x, y] = 4;
                        break;
                    case 5:
                        continue;
                }
            }
        }
        for (int i = 0; i < width; i++)
        {
            string row = "";
            for (int j = 0; j < height; j++)
            {
                row += terrainMap[i, j].ToString() + ", ";
            }
            Debug.Log(row);
        }
    }
    private int checkCoord(int x, int y)
    {
        for(int i = 0; i < directions.Length; i++)
        {
            
        }
        return 0;
    }
    private void translateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                int coord = terrainMap[x, y];
                switch (coord)
                {
                    case 0:
                        LandTiles.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), Water);
                        break;
                    case 1:
                        LandTiles.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), Grass);
                        break;
                    case 2:
                        LandTiles.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), Mountain);
                        break;
                    case 3:
                        LandTiles.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), Desert);
                        break;
                    case 4:
                        LandTiles.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), Forest);
                        break;
                }
            }
        }
    }
}
