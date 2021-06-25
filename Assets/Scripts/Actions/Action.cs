public interface Action {
    void Execute();

    void UpdateHud();

    string GetName();

    string[] GetTileTypesFilter();
}
