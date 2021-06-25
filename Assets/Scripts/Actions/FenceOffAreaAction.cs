using UnityEngine;

public class FenceOffAreaAction : CostAction {
    public FenceOffAreaAction(Income income, TileSelection tileSelection, HUDManager hud)
        : base(income, tileSelection, hud, 
            ActionsNames.FenceOffArea.Value, new string[] { TileTypes.Building.Value, TileTypes.CropsCorn.Value, TileTypes.CropsWheat.Value }) { }

    public override int GetPricePerTile() {
        return (int)ActionsPrices.FenceOff;
    }

    public override string GetTileType() {
        return TileTypes.Grass.Value;
    }

    public override void DoExecute() {
        foreach (Tile tile in tileSelection.SelectedTiles) {
            tile.RiskFactor = 20;
            tile.AvailableFood = 0;
            tile.CalculateAttractiveness();
        }
    }
}
