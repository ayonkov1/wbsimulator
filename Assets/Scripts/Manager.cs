using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    private int currentYear;
    [SerializeField] private Map map;
    [SerializeField] private HUDManager hud;
    [SerializeField] private Sprite ecoductSprite;
    [SerializeField] private Income income;
    [SerializeField] private GameEnd gameEnd;
    [SerializeField] private ActionManager actionManager;

    //private int forestTilesGrown;
    private int boarDeathsNatural;
    private int boarDeathsOnRoad;
    private int boarDeathsFromStarvation;
    private int boarDeathsFromWinter;
    private int boarBirths;
    private int boarMigrations;

    private void Start() {
        hud.updateTotalBoarPopulation.Invoke(map.GetTotalBoars().ToString());
        hud.updateCurrentIncomeBalance.Invoke(income.GetIncome().ToString());
        currentYear = 2021;
    }

    public void NextYear() {
        if (currentYear + 1 >= 2031)
        {
            hud.updateWarningMsg.Invoke("GAME OVER - 10 years have passed.", 10);
            currentYear++;
            NormalYear();
            gameEnd.DeactivateButtons();
            int[] info = { boarDeathsNatural, boarDeathsOnRoad, boarDeathsFromStarvation, boarDeathsFromWinter, boarBirths, boarMigrations };
            hud.updateGameEndInfo.Invoke(info);
            gameEnd.ShowInfo();
        }
        else
        {
            currentYear++;
            if (Random.Range(1, 11) < 2)
                HeavyWinterYear();
            else
                NormalYear();
        }

        hud.updateTotalBoarPopulation.Invoke(map.GetTotalBoars().ToString());
        hud.updateCurrentYear.Invoke(currentYear);
        map.ResetBoarIcons();

        income.AddYearToIncome();

        actionManager.CurrentAction.UpdateHud();
    }

    private void NormalYear()
    {
        foreach (Tile tile in map.TileList)
        {
            IncreaseAvailableFoodOnTile(tile);

            if (tile.WillChange && tile.YearsPassed < tile.YearsToChange)
            {
                tile.YearsPassed++;
                tile.EvolveTile();
            }

            if (tile.BoarsOnTile.Count > 0)
            {
                int starvationChance = CalculateStarvationChance(tile);
                boarDeathsNatural += KillPercantageOfBoars(tile.BoarsOnTile, Random.Range(4, 11)); // 4-10% of all boars die from natural death

                foreach (WildBoar wildBoar in tile.BoarsOnTile.ToList())
                {
                    if (tile.BoarsOnTile.Count > 0)
                    {
                        if (DecreaseFoodValueOnTile(tile, wildBoar.FoodConsumption))
                        { // there is enough food on the tile for every boar
                            boarBirths += wildBoar.Reproduce();
                        }
                        else
                        { //there isn’t enough food on the tile for every boar
                            if (KillBoarFromStarvation(tile.BoarsOnTile, wildBoar, starvationChance))
                            {
                                boarDeathsFromStarvation++;
                            }; // every boar has a starvationChance % chance to die of starvation every year
                            int[] results = wildBoar.Migrate();
                            boarMigrations += results[0];
                            boarDeathsOnRoad += results[1];
                        }
                    }
                }
            }
        }
    }

    private void HeavyWinterYear()
    {
        foreach (Tile tile in map.TileList)
        {
            IncreaseAvailableFoodOnTile(tile);

            if (tile.WillChange && tile.YearsPassed < tile.YearsToChange)
            {
                tile.YearsPassed++;
                tile.EvolveTile();
            }

            if (tile.BoarsOnTile.Count > 0)
            {
                int starvationChance = CalculateStarvationChance(tile);
                boarDeathsNatural += KillPercantageOfBoars(tile.BoarsOnTile, Random.Range(4, 11)); // 4-10% of all boars die from natural death
                boarDeathsFromWinter += KillPercantageOfBoars(tile.BoarsOnTile, 25); // 25% of boar die due to the severe winter conditions
                hud.updateWarningMsg.Invoke("Due to heavy winter conditions, 25% of wild boar did not make it to " + currentYear, 8);

                foreach (WildBoar wildBoar in tile.BoarsOnTile.ToList())
                {
                    if (tile.BoarsOnTile.Count > 0)
                    {
                        if (DecreaseFoodValueOnTile(tile, wildBoar.FoodConsumption))
                        { // there is enough food on the tile for every boar
                            boarBirths += wildBoar.Reproduce();
                        }
                        else
                        { //there isn’t enough food on the tile for every boar
                            if (KillBoarFromStarvation(tile.BoarsOnTile, wildBoar, starvationChance))
                            {
                                boarDeathsFromStarvation++;
                            }; // every boar has a starvationChance % chance to die of starvation every year 
                            int[] results = wildBoar.Migrate();
                            boarMigrations += results[0];
                            boarDeathsOnRoad += results[1];
                        }
                    }
                }
            }
        } 
    }

    private int CalculateStarvationChance(Tile tile) {
        int x = 0;

        if (tile.AvailableFood / tile.BoarsOnTile.Count > 10) { // there is enough food for each boar
            x = 9; // so that the starvation chance will be 0%
        } else {
            x = tile.AvailableFood / tile.BoarsOnTile.Count;
        }

        return 90 - (10 * x); // starvation chance increases as the food on the tile decreases
    }

    private void IncreaseAvailableFoodOnTile(Tile tile) {
        tile.AvailableFood += tile.FoodGrowPerYear;
    }

    private bool DecreaseFoodValueOnTile(Tile tile, int foodConsumption) {
        bool hasEnoughFood;

        if (tile.AvailableFood - (tile.BoarsOnTile.Count * foodConsumption) <= 0) {
            tile.AvailableFood = 0;
            hasEnoughFood = false;
        } else {
            tile.AvailableFood -= tile.BoarsOnTile.Count * foodConsumption;
            hasEnoughFood = true;
        }

        tile.CalculateAttractiveness();

        return hasEnoughFood;
    }

    private bool KillBoarFromStarvation(List<WildBoar> boars, WildBoar wildBoar, int percentage) {
        if (Random.Range(0, 100) < percentage) {
            boars.Remove(wildBoar);
            return true;
        }
        else
        {
            return false;
        }
    }

    private int KillPercantageOfBoars(List<WildBoar> boars, int percentage) {
        int boarsToRemove = Mathf.RoundToInt(boars.Count * (percentage / 100f));

        if (boarsToRemove == 0) {
            boars.Clear();
            return 0;
        } else {
            boars.RemoveRange(0, boarsToRemove);
            return boarsToRemove;
        }
    }

    static void ClearConsole() {
        var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");

        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

        clearMethod.Invoke(null, null);
    }


    // Tests the Income change on the action of building an ecoduct
    public void EcoductIncomeTest()
    {
        Debug.Log("Ecoduct price: " + (int)ActionsPrices.BuildEcoduct);
        if (!income.SubtractFromIncome((int)ActionsPrices.BuildEcoduct))
        {// make popup here
            hud.updateWarningMsg.Invoke("Not enough money", 2);
            Debug.Log("Too little money for an ecoduct");
        }
        hud.updateCurrentIncomeBalance.Invoke(income.GetIncome().ToString());
    }
}
