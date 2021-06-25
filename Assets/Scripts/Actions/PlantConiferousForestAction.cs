public class PlantConiferousForestAction : CostAction {
    public PlantConiferousForestAction(Income income, TileSelection tileSelection, HUDManager hud)
        : base(income, tileSelection, hud, 
            ActionsNames.PlantForestConiferous.Value, new string[] { TileTypes.Grass.Value }) { }

    public override int GetPricePerTile() {
        return (int)ActionsPrices.PlantForest;
    }

    public override string GetTileType() {
        return TileTypes.ForestConiferousSapling.Value;
    }

    public override void DoExecute() {
        tileSelection.ChangeSelectedTilesTo(GetTileType());
    }
}
