public abstract class AbstractAction : Action {
    protected Income income;
    protected TileSelection tileSelection;
    protected HUDManager hud;
    protected string name;
    protected string[] tileTypesFilter;

    public AbstractAction(Income income, TileSelection tileSelection, HUDManager hud, string name, string[] tileTypesFilter) {
        this.income = income;
        this.tileSelection = tileSelection;
        this.hud = hud;
        this.name = name;
        this.tileTypesFilter = tileTypesFilter;
    }

    public abstract void Execute();

    public abstract void UpdateHud();

    public string GetName() {
        return name;
    }

    public string[] GetTileTypesFilter() {
        return tileTypesFilter;
    }
}
