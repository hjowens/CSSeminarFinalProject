using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
enum Types
{
    water = 0,
    grass = 1,
    mountain = 2,
    forest = 3,
    desert = 4,
    grassiron = 5,
    grassstone = 6,
    desertiron = 7,
    desertstone = 8
}

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
    public Tile grassStone;
    public Tile grassIron;
    public Tile desertStone;
    public Tile desertIron;
    public Tilemap LandTiles;
    public bool removeIslands = true;
    [Range(1, 100)]
    public int resourceAbundance;
    [Range(0, 100)]
    public int grassChance;
    [Range(0, 100)]
    public int mountainChance;
    [Range(0, 100)]
    public int forestChance;
    [Range(0, 100)]
    public int desertChance;
    [Range(0, 100)]
    public int waterChance;
    int total;
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
        total = grassChance + mountainChance + forestChance + desertChance + waterChance;
        int width2 = height;
        int height2 = width;
        width = width2;
        height = height2;
        createInitialMap();
        runSim(Runs);
        randomResources(resourceAbundance);
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
                float seed = Random.Range(0, total);
                int top = waterChance;
                int bottom = 0;
                if (seed <= top)
                {
                    terrainMap[i, j] = (int)Types.water;
                }
                top += grassChance;
                bottom += waterChance;
                if (seed <= top && seed > bottom)
                {
                    terrainMap[i, j] = (int)Types.grass;
                }
                top += mountainChance;
                bottom += grassChance;
                if (seed <= top && seed > bottom)
                {
                    terrainMap[i, j] = (int)Types.mountain;
                }
                top += forestChance;
                bottom += mountainChance;
                if (seed <= top && seed > bottom)
                {
                    terrainMap[i, j] = (int)Types.forest;
                }
                top += desertChance;
                bottom += forestChance;
                if (seed <= top && seed > bottom)
                {
                    terrainMap[i, j] = (int)Types.desert;
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
    public void randomResources(int abundance)
    {
        /*
        This function distributes random resources (iron and stone) throughout the map based on the abundance value.
        */
        List<Vector2Int> resourceList = new List<Vector2Int>();
        int resourceLength = 0;
        // Loops throught the map to determine how many grass, desert and forest tiles there are and adds them to a list.
        // This is because I only place resources on land tiles that aren't mountains.
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                int coord = terrainMap[i, j];
                switch (coord)
                {
                    case (int)Types.desert:
                        resourceList.Add(new Vector2Int(i, j));
                        resourceLength += 1;
                        break;
                    case (int)Types.grass:
                        resourceList.Add(new Vector2Int(i, j));
                        resourceLength += 1;
                        break;
                    case (int)Types.forest:
                        resourceList.Add(new Vector2Int(i, j));
                        resourceLength += 1;
                        break;
                }
            }
        }
        // Determines how many resources I wish to distribute.
        int amountResources = amountResources = (int)(width * height * ((float)abundance / 1000.0));
        // Loops through list of land tiles and randomly distributes resources.
        for (int i = 0; i < amountResources; i++)
        {
            int stoneIron = Random.Range(0, 2);
            if (stoneIron == 2)
            {
                stoneIron = 1;
            }
            try
            {
                int Rand = Random.Range(0, resourceLength - 1);
                // checks if there are other resources nearby before distribution.
                if (checkforResources(resourceList[Rand].x, resourceList[Rand].y) == false)
                {
                    if(terrainMap[resourceList[Rand].x, resourceList[Rand].y] == (int)Types.grass || terrainMap[resourceList[Rand].x, resourceList[Rand].y] == (int)Types.forest)
                        terrainMap[resourceList[Rand].x, resourceList[Rand].y] = (int)Types.grassiron + stoneIron;
                    else if(terrainMap[resourceList[Rand].x, resourceList[Rand].y] == (int)Types.desert)
                        terrainMap[resourceList[Rand].x, resourceList[Rand].y] = (int)Types.desertiron + stoneIron;
                }
            }
            catch
            {
                Debug.Log("CATCH");
                continue;
            }
        }
    }
    private bool checkforResources(int xCoord, int yCoord)
    {
        /*
        Checks if there are other reosurces within a distance of one tile of a given position.
        */
        for (int i = 0; i < directions.Length - 1; i++)
        {
            for (int j = (int)Types.grassiron; j <= (int)Types.desertstone; j++)
            {
                try
                {
                    if (terrainMap[xCoord + directions[i, 0], yCoord + directions[i, 1]] == j)
                    {
                        return true;
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
        return false;
    }
    // Takes the 2d list generated from the rest of the code and turns it into a tilemap in Unity.
    private void translateMap()
    {
        /*
        translates a given 2d array into a tilemap using the given enum and tiles assigned in the unity project.
        */
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int coord = terrainMap[x, y];
                switch (coord)
                {
                    case (int)Types.water:
                        LandTiles.SetTile(new Vector3Int(x, y, 0), Water);
                        break;
                    case (int)Types.grass:
                        LandTiles.SetTile(new Vector3Int(x, y , 0), Grass);
                        break;
                    case (int)Types.mountain:
                        LandTiles.SetTile(new Vector3Int(x , y, 0), Mountain);
                        break;
                    case (int)Types.forest:
                        LandTiles.SetTile(new Vector3Int(x , y , 0), Forest);
                        break;
                    case (int)Types.desert:
                        LandTiles.SetTile(new Vector3Int(x , y , 0), Desert);
                        break;
                    case (int)Types.grassiron:
                        LandTiles.SetTile(new Vector3Int(x , y , 0), grassIron);
                        break;
                    case (int)Types.grassstone:
                        LandTiles.SetTile(new Vector3Int(x , y , 0), grassStone);
                        break;
                    case (int)Types.desertiron:
                        LandTiles.SetTile(new Vector3Int(x, y , 0), desertIron);
                        break;
                    case (int)Types.desertstone:
                        LandTiles.SetTile(new Vector3Int(x , y , 0), desertStone);
                        break;
                }
            }
        }
    }
}
