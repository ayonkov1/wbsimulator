using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using System;

public class HUDManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI totalBoarPopulation;
    [SerializeField] private TextMeshProUGUI currentYear;
    [SerializeField] private TextMeshProUGUI currentActionName;
    [SerializeField] private TextMeshProUGUI currentActionInfo;
    [SerializeField] private TextMeshProUGUI incomeBalance;
    [SerializeField] private TextMeshProUGUI warningMsg;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Dropdown actionsDropdown;
    [SerializeField] private GameObject riskFactorImg;
    [SerializeField] private TextMeshProUGUI gameEndInfo;

    public SetHUDTextEvent updateTotalBoarPopulation;
    public SetHUDTextEvent updateCurrentActionName;
    public SetHUDTextEvent updateCurrentActionInfo;
    public SetHUDTextEvent updateCurrentIncomeBalance;
    public SetWarningTextEvent updateWarningMsg;
    public ToggleObjectEvent toggleConfirmCancelButtons;
    public SetHUDYearEvent updateCurrentYear;
    public SetGameEndTextEvent updateGameEndInfo;

    public Dropdown ActionsDropdown { get => actionsDropdown; }
    public GameObject RiskFactorImg { get => riskFactorImg; }

    //Listeners added in the awake funcions so they are ready for the inital print on the hud
    private void Awake() {
        updateTotalBoarPopulation.AddListener(UpdateTotalBoarPopulation);
        updateCurrentActionName.AddListener(UpdateCurrentActionName);
        updateCurrentActionInfo.AddListener(UpdateCurrentActionInfo);
        updateCurrentIncomeBalance.AddListener(UpdateCurrentIncomeBalance);
        updateWarningMsg.AddListener(UpdateWarningMsg);
        toggleConfirmCancelButtons.AddListener(ToggleConfirmCancelButtons);
        updateCurrentYear.AddListener(UpdateCurrentYear);
        updateGameEndInfo.AddListener(UpdateGameEndInfo);
    }

    private void OnDestroy() {
        updateTotalBoarPopulation.RemoveListener(UpdateTotalBoarPopulation);
        updateCurrentActionName.RemoveListener(UpdateCurrentActionName);
        updateCurrentActionInfo.RemoveListener(UpdateCurrentActionInfo);
        updateCurrentIncomeBalance.RemoveListener(UpdateCurrentIncomeBalance);
        updateWarningMsg.RemoveListener(UpdateWarningMsg);
        toggleConfirmCancelButtons.RemoveListener(ToggleConfirmCancelButtons);
        updateCurrentYear.RemoveListener(UpdateCurrentYear);
        updateGameEndInfo.RemoveListener(UpdateGameEndInfo);
    }

    private void ToggleConfirmCancelButtons(bool isActive) {
        confirmButton.gameObject.SetActive(isActive);
        cancelButton.gameObject.SetActive(isActive);
    }

    private void UpdateTotalBoarPopulation(string count) {
        totalBoarPopulation.text = "Boars: " + count;
    }

    private void UpdateCurrentYear (int year)
    {
        currentYear.text = "Year " + year;
    }

    private void UpdateCurrentActionName(string action) {
        currentActionName.text = "Action: " + action;
    }

    private void UpdateCurrentActionInfo(string info) {
        currentActionInfo.text = info;
    }

    private void UpdateCurrentIncomeBalance(string income)
    {
        incomeBalance.text = income + " €";
    }

    private void UpdateGameEndInfo(int[] info)
    {
        gameEndInfo.text = "Game Over - Summary:\n\nNatural deaths: " + info[0] + "\nRoad deaths: " + info[1] + "\nStarvation deaths: " + info[2] + "\nHeavy winter deaths: " + info[3] + "\nBirths: " + info[4] + "\nMigrations: " + info[5];
    }

    // sets the warning msg and calls the pop up system animation script (it is attached to to the popup)
    private void UpdateWarningMsg(string msg, int seconds)
    {
        warningMsg.text = msg;
        warningMsg.transform.parent.gameObject.GetComponent<PopUpSystem>().PopUp(seconds);
    }
}

[System.Serializable] public class SetHUDTextEvent : UnityEvent<string> { }
[System.Serializable] public class SetGameEndTextEvent : UnityEvent<int[]> { }
[System.Serializable] public class SetWarningTextEvent : UnityEvent<string, int> { }
[System.Serializable] public class SetHUDYearEvent : UnityEvent<int> { }
[System.Serializable] public class ToggleObjectEvent : UnityEvent<bool> { }