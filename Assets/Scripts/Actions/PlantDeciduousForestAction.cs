public class PlantDeciduousForestAction : CostAction {
    public PlantDeciduousForestAction(Income income, TileSelection tileSelection, HUDManager hud)
        : base(income, tileSelection, hud, 
            ActionsNames.PlantForestDeciduous.Value, new string[] { TileTypes.Grass.Value }) { }

    public override int GetPricePerTile() {
        return (int)ActionsPrices.PlantForest;
    }

    public override string GetTileType() {
        return TileTypes.ForestDeciduousSapling.Value;
    }

    public override void DoExecute() {
        tileSelection.ChangeSelectedTilesTo(GetTileType());
    }
}
