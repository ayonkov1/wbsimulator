using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Tile
{
    private Map map;
    private string type;
    private int foodValue;
    private int availableFood;
    private int foodGrowPerYear;
    private int attractiveness;
    private int riskFactor;
    private List<Tile> neighbourTiles;
    private List<WildBoar> boarsOnTile;
    private Vector3Int coordinates;

    private bool willChange;
    private int yearsPassed;
    private int yearsToChange;
    private string typeToChange;

    public Map Map { get => map; set => map = value; }
    public string Type { get => type; set => type = value; }
    public int FoodValue { get => foodValue; set => foodValue = value; }
    public int AvailableFood { get => availableFood; set => availableFood = value; }
    public int FoodGrowPerYear { get => foodGrowPerYear; set => foodGrowPerYear = value; }
    public int Attractiveness { get => attractiveness; set => attractiveness = value; }
    public int RiskFactor { get => riskFactor; set => riskFactor = value; }
    public List<Tile> NeighbourTiles { get => neighbourTiles; set => neighbourTiles = value; }
    public List<WildBoar> BoarsOnTile { get => boarsOnTile; set => boarsOnTile = value; }

    public Vector3Int Coordinates { get => coordinates; set => coordinates = value; }
    public bool WillChange { get => willChange; set => willChange = value; }
    public int YearsPassed { get => yearsPassed; set => yearsPassed = value; }
    public int YearsToChange { get => yearsToChange; set => yearsToChange = value; }
    public string TypeToChange { get => typeToChange; set => typeToChange = value; }

    public Tile(Map map, TileInfo tileInfo, Vector3Int coordinates)
    {
        this.map = map;
        this.type = tileInfo.Type;
        this.foodValue = tileInfo.FoodValue;
        this.availableFood = tileInfo.AvailableFood;
        this.riskFactor = tileInfo.RiskFactor;
        this.foodGrowPerYear = tileInfo.FoodGrowPerYear;
        this.willChange = tileInfo.WillChange;
        this.yearsToChange = tileInfo.YearsToChange;
        this.yearsPassed = tileInfo.YearsPassed;
        this.typeToChange = tileInfo.TypeToChange;
        this.coordinates = coordinates;
        this.boarsOnTile = new List<WildBoar>();
        this.neighbourTiles = new List<Tile>();
        CalculateAttractiveness();

        if (type.Contains("Forest")) {
            if (Random.Range(0, 100) < 60) {
                int numberOfWildBoar = Random.Range(1, 21);
                for (int i = 0; i < numberOfWildBoar; i++) {
                    this.boarsOnTile.Add(new WildBoar(this));
                }
            }
        }
    }

    public Tile(Map map, string type, int foodValue, int availableFood, int riskFactor, int foodGrowPerYear,
        bool willChange, int yearsToChange, int yearsPassed, string typeToChange, Vector3Int coordinates)
    {
        this.map = map;
        this.type = type;
        this.foodValue = foodValue;
        this.availableFood = availableFood;
        this.riskFactor = riskFactor;
        this.foodGrowPerYear = foodGrowPerYear;
        this.willChange = willChange;
        this.yearsToChange = yearsToChange;
        this.yearsPassed = yearsPassed;
        this.typeToChange = typeToChange;
        this.coordinates = coordinates;
        this.boarsOnTile = new List<WildBoar>();
        this.neighbourTiles = new List<Tile>();
        CalculateAttractiveness();

        if (type.Contains("Forest"))
        {
            if (Random.Range(0, 100) < 60)
            {
                int numberOfWildBoar = Random.Range(1, 21);
                for (int i = 0; i < numberOfWildBoar; i++)
                {
                    this.boarsOnTile.Add(new WildBoar(this));
                }
            }
        }
    }

    public void UpdateProperties(TileInfo tileInfo) {
        this.type = tileInfo.Type;
        this.foodValue = tileInfo.FoodValue;
        this.availableFood = tileInfo.AvailableFood;
        this.riskFactor = tileInfo.RiskFactor;
        this.foodGrowPerYear = tileInfo.FoodGrowPerYear;
        this.willChange = tileInfo.WillChange;
        this.yearsPassed = tileInfo.YearsPassed;
        this.yearsToChange = tileInfo.YearsToChange;
        this.typeToChange = tileInfo.TypeToChange;
        //TODO boarsOnTile should it stay the same or kill em all :P
        CalculateAttractiveness();
    }

    public void EvolveTile()
    {
        ChangeTileTo(this.TypeToChange);
    }

    public void ChangeTileTo(string type)
    {
        if (map.TilesInfo.TryGetValue(type, out TileInfo tileInfo))
        {
            this.UpdateProperties(tileInfo);
            map.ChangeTileSprite(this.Coordinates, tileInfo.Sprite);
        }
    }

    public void CalculateAttractiveness() {
        this.attractiveness = (this.foodValue * this.availableFood) / this.riskFactor;
    }

    public List<Tile> GetNeighbours(int range)
    {
        List<Vector3Int> neighboursCoordinates = this.GetNeighborsCoordinates(range);
        List<Tile> tiles = new List<Tile>();

        for (int i = 0; i < neighboursCoordinates.Count; i++)
        {
            if (map.Tiles.TryGetValue(neighboursCoordinates[i], out Tile tile))
            {
                tiles.Add(tile);
            }
        }

        return tiles;
    }

    private List<Vector3Int> GetNeighborsCoordinates(int range) {
        var centerCubePos = UnityCellToCube(this.coordinates);

        var result = new List<Vector3Int>();

        int min = -range, max = range;

        for (int x = min; x <= max; x++) {
            for (int y = min; y <= max; y++) {
                var z = -x - y;
                if (z < min || z > max) {
                    continue;
                }

                var cubePosOffset = new Vector3Int(x, y, z);
                if (cubePosOffset != new Vector3Int(0, 0, 0)) {
                    result.Add(CubeToUnityCell(centerCubePos + cubePosOffset));
                }
            }

        }

        return result;
    }

    private Vector3Int UnityCellToCube(Vector3Int cell)
    {
        var yCell = cell.x;
        var xCell = cell.y;
        var x = yCell - (xCell - (xCell & 1)) / 2;
        var z = xCell;
        var y = -x - z;
        return new Vector3Int(x, y, z);
    }

    private Vector3Int CubeToUnityCell(Vector3Int cube)
    {
        var x = cube.x;
        var z = cube.z;
        var col = x + (z - (z & 1)) / 2;
        var row = z;

        return new Vector3Int(col, row, 0);
    }

}