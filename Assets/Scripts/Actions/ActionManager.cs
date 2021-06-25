using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour {
    [SerializeField] private Income income;
    [SerializeField] private TileSelection tileSelection;
    [SerializeField] private HUDManager hud;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private Action currentAction;

    public Action CurrentAction { get => currentAction; }

    private void Start() {
        AddAction(new ResearchAction(income, tileSelection, hud));
        AddAction(new PlantConiferousForestAction(income, tileSelection, hud));
        AddAction(new PlantDeciduousForestAction(income, tileSelection, hud));
        AddAction(new CutDownForestAction(income, tileSelection, hud));
        AddAction(new FenceOffAreaAction(income, tileSelection, hud));
        AddAction(new BuildEcoductAction(income, tileSelection, hud));
        AddAction(new RecreationalActivitiesAction(income, tileSelection, hud));

        SetCurrentAction();
    }

    public void SetCurrentAction() {
        string actionName = hud.ActionsDropdown.options[hud.ActionsDropdown.value].text;

        if (actions.TryGetValue(actionName, out Action action)) {
            currentAction = action;

            tileSelection.TypesFilter = currentAction.GetTileTypesFilter(); // set types filter on action change
            ClearSelectionAndActionHud();

            hud.RiskFactorImg.GetComponent<ChangeSprite>().setInactive(); // hide risk factor image on action change
        }
    }

    public void ClearSelectionAndActionHud() {
        tileSelection.ClearSelection();
        hud.updateCurrentActionName.Invoke("");
        hud.updateCurrentActionInfo.Invoke("");
        hud.toggleConfirmCancelButtons.Invoke(false);
    }

    public void ExecuteSelectedAction() {
        currentAction.Execute();
    }

    private void AddAction(Action action) {
        actions.Add(action.GetName(), action);
    }
}
