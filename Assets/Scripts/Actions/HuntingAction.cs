using UnityEngine;

public class HuntingAction : ScareAction {
    public HuntingAction(Income income, TileSelection tileSelection, HUDManager hud)
        : base(income, tileSelection, hud, ActionsNames.Hunting.Value) { }

    public override int GetPricePerTile() {
        return (int)ActionsPrices.Hunting;
    }

    public override string GetTileType() {
        return TileTypes.Grass.Value;
    }

    public override void Scare(Tile tile, WildBoar wildBoar) {
        if (Random.Range(0, 100) < 80) { // 80% chance for each boar to be hunted down, disappear from map, etc.
            tile.BoarsOnTile.Remove(wildBoar);
        } else {
            wildBoar.Migrate();
        }
    }
}
