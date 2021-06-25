using System.Linq;

public abstract class ScareAction : CostAction {
    public ScareAction(Income income, TileSelection tileSelection, HUDManager hud, string name) 
        : base(income, tileSelection, hud, name, new string[] { }) { }

    public override void DoExecute() {
        foreach (Tile tile in tileSelection.SelectedTiles) {
            foreach (WildBoar wildBoar in tile.BoarsOnTile.ToList()) {
                Scare(tile, wildBoar);
            }
        }

        hud.updateTotalBoarPopulation.Invoke(tileSelection.Map.GetTotalBoars().ToString());
        tileSelection.Map.ResetBoarIcons();
    }

    public abstract void Scare(Tile tile, WildBoar wildBoar);
}
