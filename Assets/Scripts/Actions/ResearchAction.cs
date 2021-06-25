using System.Linq;

public class ResearchAction : AbstractAction {
    public ResearchAction(Income income, TileSelection tileSelection, HUDManager hud)
        : base(income, tileSelection, hud, 
            ActionsNames.Research.Value, new string[] { }) { }

    public override void Execute() {
        throw new System.NotImplementedException(); // the research action is executed immediately without calling the Execute() method
    }

    public override void UpdateHud() {
        // set the risk factor to inactive
        hud.RiskFactorImg.GetComponent<ChangeSprite>().setInactive();

        var selectedTiles = tileSelection.SelectedTiles;

        if (selectedTiles.Count > 1) {
            hud.updateCurrentActionName.Invoke(this.name + " area");
            hud.updateCurrentActionInfo.Invoke("Selected Tiles: " + selectedTiles.Count.ToString() +
                "\nBoars: " + selectedTiles.Sum(t => t.BoarsOnTile.Count) +
                "\nFood: " + selectedTiles.Sum(t => t.AvailableFood));

        } else {
            // change risk factor sprite and set to active
            hud.RiskFactorImg.GetComponent<ChangeSprite>().ChangeSpriteImg(selectedTiles[0].RiskFactor);

            hud.updateCurrentActionName.Invoke(this.name + " tile");

            string selectedTileType = selectedTiles[0].Type;

            if (selectedTileType.Contains(TileTypes.Road.Value)) {
                selectedTileType = TileTypes.Road.Value;
            } else if (selectedTileType.Contains(TileTypes.Ecoduct.Value)) {
                selectedTileType = TileTypes.Ecoduct.Value;
            }

            hud.updateCurrentActionInfo.Invoke("Type:  " + selectedTileType +
                "\nRisk: " +
                "\nBoars: " + selectedTiles[0].BoarsOnTile.Count +
                "\nFood:  " + selectedTiles[0].AvailableFood);
        }
    }
}
