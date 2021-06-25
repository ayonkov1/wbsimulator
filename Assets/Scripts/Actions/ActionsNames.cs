public class ActionsNames {
    // The value strings here must be the same as the options in ActionsDropdown in Canvas

    private ActionsNames(string value) { Value = value; }

    public string Value { get; private set; }

    public static ActionsNames Research { get { return new ActionsNames("Research"); } }
    public static ActionsNames PlantForestConiferous { get { return new ActionsNames("Plant a coniferous forest"); } }
    public static ActionsNames PlantForestDeciduous { get { return new ActionsNames("Plant a deciduous forest"); } }
    public static ActionsNames CutDownForest { get { return new ActionsNames("Cut down a forest"); } }
    public static ActionsNames FenceOffArea { get { return new ActionsNames("Fence off an area"); } }
    public static ActionsNames BuildEcoduct { get { return new ActionsNames("Build an ecoduct"); } }
    public static ActionsNames Hunting { get { return new ActionsNames("Initiate hunting"); } }
    public static ActionsNames RecreationalActivities { get { return new ActionsNames("Recreational activities"); } }
}
