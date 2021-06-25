public class BuildEcoductAction : CostAction {
    public BuildEcoductAction(Income income, TileSelection tileSelection, HUDManager hud)
        : base(income, tileSelection, hud,
            ActionsNames.BuildEcoduct.Value, new string[] { TileTypes.HorizontalRoad.Value,
            TileTypes.DiagonalRoadLeft.Value, TileTypes.DiagonalRoadRight.Value,
            TileTypes.CurveRoadBottomLeft.Value, TileTypes.CurveRoadBottomRight.Value,
            TileTypes.CurveRoadLeft.Value, TileTypes.CurveRoadRight.Value,
            TileTypes.CurveRoadTopLeft.Value, TileTypes.CurveRoadTopRight.Value, }) { }

    public override int GetPricePerTile() {
        return (int)ActionsPrices.BuildEcoduct;
    }

    public override string GetTileType() {
        return TileTypes.Ecoduct.Value;
    }

    public override void DoExecute() {
        foreach (Tile tile in tileSelection.SelectedTiles) {
            if (tileSelection.CheckTileNeighboursEcoduct(tile)) { // ignore the neighbours of the ecoduct in execution
                tile.EvolveTile();
            }
        }
    }
}
