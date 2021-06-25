public abstract class CostAction : AbstractAction {
    public CostAction(Income income, TileSelection tileSelection, HUDManager hud, string name, string[] tileTypesFilter) 
        : base(income, tileSelection, hud, name, tileTypesFilter) { }

    public override void Execute() {
        int price = tileSelection.SelectedTiles.Count * GetPricePerTile();

        if (!income.CheckEnoughIncome(price)) { // check & substract money if enough
            hud.updateWarningMsg.Invoke("Not enough money", 2); // if money is not enough, display a pop-up
        } else { // if money is enough, execute change tiles action
            DoExecute();
            income.SubtractFromIncome(price);

            tileSelection.ClearSelection();
            hud.updateCurrentActionName.Invoke("");
            hud.updateCurrentActionInfo.Invoke("");
            hud.toggleConfirmCancelButtons.Invoke(false);
        }
    }

    public override void UpdateHud() {
        hud.toggleConfirmCancelButtons.Invoke(tileSelection.SelectedTiles.Count > 0); // show confirm and cancel buttons only when there are selected tiles

        var selectedTiles = tileSelection.SelectedTiles;

        hud.updateCurrentActionName.Invoke(this.name);
        hud.updateCurrentActionInfo.Invoke(selectedTiles.Count.ToString() + " tiles x €" + GetPricePerTile().ToString() +
            "\n_______________\n€" +
            (selectedTiles.Count * GetPricePerTile()).ToString());
    }

    public abstract int GetPricePerTile();
    public abstract string GetTileType();

    public abstract void DoExecute();
}
