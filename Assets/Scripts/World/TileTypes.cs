public class TileTypes
{
    private TileTypes(string value) { Value = value; }

    public string Value { get; private set; }

    public static TileTypes Grass { get { return new TileTypes("Grass"); } }
    public static TileTypes ForestConiferousBig { get { return new TileTypes("Coniferous Forest Big"); } }
    public static TileTypes ForestConiferousSmall { get { return new TileTypes("Coniferous Forest Small"); } }
    public static TileTypes ForestConiferousSapling { get { return new TileTypes("Coniferous Forest Sapling"); } }
    public static TileTypes ForestDeciduousBig { get { return new TileTypes("Deciduous Forest Big"); } }
    public static TileTypes ForestDeciduousSmall { get { return new TileTypes("Deciduous Forest Small"); } }
    public static TileTypes ForestDeciduousSapling { get { return new TileTypes("Deciduous Forest Sapling"); } }
    public static TileTypes ForestCutDown { get { return new TileTypes("Cut Down Forest"); } }
    public static TileTypes CropsWheat { get { return new TileTypes("Crops Wheat"); } }
    public static TileTypes CropsCorn { get { return new TileTypes("Crops Corn"); } }
    public static TileTypes Road { get { return new TileTypes("Road"); } }
    public static TileTypes Water { get { return new TileTypes("Water"); } }
    public static TileTypes Building { get { return new TileTypes("Building"); } }
    public static TileTypes Ecoduct { get { return new TileTypes("Ecoduct"); } }
    public static TileTypes Other { get { return new TileTypes("Other"); } }
    public static TileTypes HorizontalRoad { get { return new TileTypes("Horizontal Road"); } }
    public static TileTypes DiagonalRoadLeft { get { return new TileTypes("Diagonal Road Left"); } }
    public static TileTypes DiagonalRoadRight { get { return new TileTypes("Diagonal Road Right"); } }
    public static TileTypes CurveRoadTopLeft { get { return new TileTypes("Curve Road Top Left"); } }
    public static TileTypes CurveRoadTopRight { get { return new TileTypes("Curve Road Top Right"); } }
    public static TileTypes CurveRoadLeft { get { return new TileTypes("Curve Road Left"); } }
    public static TileTypes CurveRoadRight { get { return new TileTypes("Curve Road Right"); } }
    public static TileTypes CurveRoadBottomLeft { get { return new TileTypes("Curve Road Bottom Left"); } }
    public static TileTypes CurveRoadBottomRight { get { return new TileTypes("Curve Road Bottom Right"); } }
    public static TileTypes HorizontalEcoduct { get { return new TileTypes("Horizontal Ecoduct"); } }
    public static TileTypes DiagonalEcoductLeft { get { return new TileTypes("Diagonal Ecoduct Left"); } }
    public static TileTypes DiagonalEcoductRight { get { return new TileTypes("Diagonal Ecoduct Right"); } }
    public static TileTypes CurveEcoductTopLeft { get { return new TileTypes("Curve Ecoduct Top Left"); } }
    public static TileTypes CurveEcoductTopRight { get { return new TileTypes("Curve Ecoduct Top Right"); } }
    public static TileTypes CurveEcoductLeft { get { return new TileTypes("Curve Ecoduct Left"); } }
    public static TileTypes CurveEcoductRight { get { return new TileTypes("Curve Ecoduct Right"); } }
    public static TileTypes CurveEcoductBottomLeft { get { return new TileTypes("Curve Ecoduct Bottom Left"); } }
    public static TileTypes CurveEcoductBottomRight { get { return new TileTypes("Curve Ecoduct Bottom Right"); } }
}
