using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Income : MonoBehaviour
{
    [SerializeField] private HUDManager hud;
    [SerializeField] private int income;
    [SerializeField] private int yearlyAddition;

    //public Dictionary<string, int> actionPrice;
    //[SerializeField]private List<string> _keys = new List<string>();
    //[SerializeField] private List<int> _values = new List<int>();


    //Unity doesn't know how to serialize a Dictionary
    public Dictionary<string, int> _actionPrices = new Dictionary<string, int>();


    // Start is called before the first frame update
    void Start()
    {
        GetActionPrices();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Fills the dictionary with the values from the inspector
    //private void  FillDictionary()
    //{
    //    for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
    //        _actionPrices.Add(_keys[i], _values[i]);
    //}
    // Gets the prices of all actions
    public Dictionary<string,int> GetActionPrices()
    {
        foreach(KeyValuePair<string, int> kvp in _actionPrices)
        {
            Debug.Log(kvp.Key + " " + kvp.Value);
        }
        return _actionPrices;
    }

    // Adds specific amount to the income sum
    public void AddToIncome(int amount)
    {
        income += amount;
        hud.updateCurrentIncomeBalance.Invoke(income.ToString());
    }

    public bool CheckEnoughIncome(int amount) {
        return !(income < amount);
    }

    // Substracts given amount from the total money. Returns true if successful and false if not
    public bool SubtractFromIncome(int amount)
    {
        if (income < amount)
        {
            return false;
        }
        income -= amount;

        hud.updateCurrentIncomeBalance.Invoke(income.ToString());

        return true;
    }
    // Adds to the income the amount for one year
    public void AddYearToIncome ()
    {
        AddToIncome(yearlyAddition);
    }
    // Gets the income
    public int GetIncome()
    {
        return income;
    }
}
