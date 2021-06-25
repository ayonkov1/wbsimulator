using UnityEngine;

public class RecreationalActivitiesAction : ScareAction {
    public RecreationalActivitiesAction(Income income, TileSelection tileSelection, HUDManager hud) 
        : base(income, tileSelection, hud, ActionsNames.RecreationalActivities.Value) { }

    public override int GetPricePerTile() {
        return (int)ActionsPrices.RecreationalActivities;
    }

    public override string GetTileType() {
        return TileTypes.Grass.Value;
    }

    public override void Scare(Tile tile, WildBoar wildBoar) {
        if (Random.Range(0, 100) < 80) { // 80% chance for each boar to be hunted down, disappear from map, etc.
            tile.BoarsOnTile.Remove(wildBoar);
        } else { // 20% chance for each boar to migrate on a neighbour tile
            wildBoar.Migrate();
        }
    }
}
