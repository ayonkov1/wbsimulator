public class CutDownForestAction : CostAction {
    public CutDownForestAction(Income income, TileSelection tileSelection, HUDManager hud)
        : base(income, tileSelection, hud, 
            ActionsNames.CutDownForest.Value, new string[] { TileTypes.ForestConiferousBig.Value, TileTypes.ForestDeciduousBig.Value,
            TileTypes.ForestConiferousSmall.Value, TileTypes.ForestDeciduousSmall.Value,
            TileTypes.ForestConiferousSapling.Value, TileTypes.ForestDeciduousSapling.Value}) { }

    public override int GetPricePerTile() {
        return (int)ActionsPrices.CutDownForest;
    }

    public override string GetTileType() {
        return TileTypes.ForestCutDown.Value;
    }

    public override void DoExecute() {
        tileSelection.ChangeSelectedTilesTo(GetTileType());
    }
}
