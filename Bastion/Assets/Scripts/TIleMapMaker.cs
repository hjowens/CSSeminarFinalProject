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
    public bool removeIslands = true;
    [Range(1, 5)]
    public int grassThresh = 3;
    [Range(1, 5)]
    public int mountainThresh = 3;
    [Range(1, 5)]
    public int forestThresh = 3;
    [Range(1, 5)]
    public int desertThresh = 3;
    [Range(0, 10)]
    public int Runs;
    int[,] directions = new int[,] { {-1, 0}, {-1, 1}, {0, -1}, {0, 1}, {1, 0}, {1, 1} };
    // Start is called at the start. I runs all of the functions I need in this file in the correct order.
    void Start()
    {
        int width2 = height;
        int height2 = width;
        width = width2;
        height = height2;
        createInitialMap();
        runSim(Runs);
        translateMap();
    }
    // This just creates the initial map with random terrain everywhere.
    // The different terrain is: Water, Grassland, Mountain, Forest, and Desert
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
                    case <= 15:
                        terrainMap[i, j] = 1;
                        break;
                    case > 15 and <= 20:
                        terrainMap[i, j] = 2;
                        break;
                    case > 20 and <= 30:
                        terrainMap[i, j] = 3;
                        break;
                    case > 30 and <= 40:
                        terrainMap[i, j] = 4;
                        break;
                    case > 40:
                        terrainMap[i, j] = 0;
                        break;
                }
            }
        }
    }

    // This implements the terrain generation/growth algorithm from another game.
    // The algorithm takes a tile and looks at all the tiles around it, and if there
    // are enough tiles of a certain type, it changes itself to that type.
    private void runSim(int numR)
    {
        for (int i = 0; i < numR; i++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
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
    // This is how the algoritm from before is implemented
    private int checkCoord(int x, int y)
    {
        int waterCount = 0;
        int grassCount = 0;
        int mountainCount = 0;
        int forestCount = 0;
        int desertCount = 0;
        for (int i = 0; i < directions.Length; i++)
        {
            try
            {
                if (removeIslands == true)
                {
                    if (terrainMap[x - directions[i, 0], y - directions[i, 1]] == 0)
                    {
                        waterCount += 1;
                    }
                }
                if (terrainMap[x - directions[i, 0], y - directions[i, 1]] == 1)
                {
                    grassCount += 1;
                }
                else if (terrainMap[x - directions[i, 0], y - directions[i, 1]] == 2)
                {
                    mountainCount += 1;
                }
                else if (terrainMap[x - directions[i, 0], y - directions[i, 1]] == 3)
                {
                    forestCount += 1;
                }
                else if (terrainMap[x - directions[i, 0], y - directions[i, 1]] == 4)
                {
                    desertCount += 1;
                }
            }
            catch
            {
                continue;
            }
        }
        if (waterCount >= 5)
        {
            return 0;
        }
        if (terrainMap[x, y] == 2)
        {
            return 5;
        }
        if (mountainCount >= mountainThresh)
        {
            return 2;
        }
        else if (forestCount >= forestThresh)
        {
            return 3;
        }
        else if (desertCount >= desertThresh)
        {
            return 4;
        }
        else if (grassCount >= grassThresh)
        {
            return 1;
        }
        return 5;
    }
    // Takes the 2d list generated from the rest of the code and turns it into a tilemap in Unity.
    private void translateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
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
                        LandTiles.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), Forest);
                        break;
                    case 4:
                        LandTiles.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), Desert);
                        break;
                }
            }
        }
    }
}
