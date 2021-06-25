using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Tile")]
public class TileInfo : ScriptableObject
{
    [SerializeField] private string type;
    [SerializeField] private int foodValue;
    [SerializeField] private int availableFood;
    [SerializeField] private int riskFactor;
    [SerializeField] private int foodGrowPerYear;
    [SerializeField] private Sprite sprite;
    [SerializeField] private bool willChange;
    [SerializeField] private int yearsPassed;
    [SerializeField] private int yearsToChange;
    [SerializeField] private string typeToChange;

    public string Type { get => type; set => type = value; }
    public int FoodValue { get => foodValue; set => foodValue = value; }
    public int AvailableFood { get => availableFood; set => availableFood = value; }
    public int RiskFactor { get => riskFactor; set => riskFactor = value; }
    public int FoodGrowPerYear { get => foodGrowPerYear; set => foodGrowPerYear = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public bool WillChange { get => willChange; set => willChange = value; }
    public int YearsPassed { get => yearsPassed; set => yearsPassed = value; }
    public int YearsToChange { get => yearsToChange; set => yearsToChange = value; }
    public string TypeToChange { get => typeToChange; set => typeToChange = value; }
}
