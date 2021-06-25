using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WildBoar
{
    private int foodConsumption = 10;
    private int movesPerYear = 1;
    private Tile currentTile;

    public int FoodConsumption { get => foodConsumption; set => foodConsumption = value; }
    public int MovesPerYear { get => movesPerYear; set => movesPerYear = value; }
    public Tile CurrentTile { get => currentTile; set => currentTile = value; }

    public WildBoar(Tile currentTile)
    {
        this.CurrentTile = currentTile;
    }

    public int[] Migrate() {
        int boarMigrations = 0;
        int boarDeathsOnRoad = 0;
        Tile mostAttractiveNeighbourTile = FindMostAttractiveTile(currentTile.NeighbourTiles); // get most attractive neighbour tile

        List<Tile> tempNeighbours = new List<Tile>();

        foreach (Tile tile in currentTile.NeighbourTiles)
        {
            tempNeighbours.Add(tile);
        }

        currentTile.BoarsOnTile.Remove(this); // remove boar from current tile

        if (Random.Range(0, 100) < 80) { // has 80% chance to move to the most attractive tile
            this.currentTile = mostAttractiveNeighbourTile;
        } else { // has 20% chance to move to a random neighbour that is not the most attractive tile
            tempNeighbours.Remove(mostAttractiveNeighbourTile);

            Tile randomTile = GetRandomAttractiveTile(tempNeighbours);

            this.currentTile = randomTile;
        }

        if (currentTile.Type.Contains("Road")) { // if the tile that the boar will move to is a road
            bool ecoductIsPresent = false;
            foreach (Tile tile in currentTile.NeighbourTiles)
            {
                if (tile.Type.Contains("Ecoduct"))
                {
                    Tile attractiveTile = FindMostAttractiveTile(tile.NeighbourTiles);

                    this.currentTile = attractiveTile;
                    attractiveTile.BoarsOnTile.Add(this);
                    boarMigrations++;
                    ecoductIsPresent = true;
                    break;
                }
            }

            if (!ecoductIsPresent)
            {
                if (Random.Range(0, 100) < 50)
                { // has 50% to move to the most attractive neighbour of the road or else won't be added to any tile (boar is dead)
                    mostAttractiveNeighbourTile = FindMostAttractiveTile(currentTile.NeighbourTiles);

                    this.currentTile = mostAttractiveNeighbourTile;
                    mostAttractiveNeighbourTile.BoarsOnTile.Add(this);
                    boarMigrations++;
                }
                else
                {
                    boarDeathsOnRoad++;
                }
            }
        }
        else 
        { // the tile is not a road
            this.currentTile.BoarsOnTile.Add(this); // add the boar to the tile (boar is alive)
            boarMigrations++;
        }
        int[] result = { boarMigrations, boarDeathsOnRoad };
        return result;
    }

    private Tile GetRandomAttractiveTile(List<Tile> tiles) {
        tiles = tiles.OrderBy(a => new System.Random().Next()).ToList(); // shuffle tiles

        Tile randomTile = this.currentTile;

        foreach (Tile tile in tiles) {
            if (tile.Attractiveness != 0 && tile.RiskFactor < 6) {
                randomTile = tile;
                break;
            }
        }

        return randomTile; // return random attractive tile or null if any of them isn't attractive
    }

    private Tile FindMostAttractiveTile(List<Tile> tiles) {
        Tile mostAttractive = currentTile;
        int highestAttractiveness = 0;

        foreach (Tile tile in tiles) {
            if (tile.Attractiveness > highestAttractiveness) {
                highestAttractiveness = tile.Attractiveness;
                mostAttractive = tile;
            }
        }

        return mostAttractive;
    }

    public int Reproduce()
    {
        int boarBorn = 0;
        if (Random.Range(0, 100) < 40)
        {
            for (int i = 0; i < Random.Range(4, 8); i++)
            {
                WildBoar child = new WildBoar(CurrentTile);
                currentTile.BoarsOnTile.Add(child);
                boarBorn++;
            }
        }
        return boarBorn;
    }
}
