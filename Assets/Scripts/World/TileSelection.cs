using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TileSelection : MonoBehaviour {
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private Map map;
    [SerializeField] private HUDManager hud;
    [SerializeField] private GameObject riskFactorImg;
    [SerializeField] private ActionManager actionManager;

    private List<Tile> selectedTiles = new List<Tile>();
    private string[] typesFilter = new string[] { };
    private Vector2 startPos;
    private bool isMouseOnUI = false;
    private bool isCtrlHeldDown = false;

    public List<Tile> SelectedTiles { get => selectedTiles; }
    public string[] TypesFilter { get => typesFilter; set => typesFilter = value; }
    public Map Map { get => map; }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) {
            isCtrlHeldDown = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl)) {
            isCtrlHeldDown = false;
        }

        if (Input.GetMouseButtonDown(0)) {
            isMouseOnUI = EventSystem.current.IsPointerOverGameObject();
            if (isMouseOnUI) return;
            
            if (!isCtrlHeldDown) { // when ctrl button is held down, add every new selection to current selection; else clear selection
                ClearSelection();
            }

            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && !isMouseOnUI) {
            ReleaseSelectionBox();
            SelectArea();
        }

        // mouse held down
        if (Input.GetMouseButton(0) && !isMouseOnUI) {
            UpdateSelectionBox(Input.mousePosition);
        }
    }

    public void ClearSelection() {
        ColorSelection(Color.white);
        selectedTiles.Clear();
    }

    public void ChangeSelectedTilesTo(string type) {
        foreach (Tile tile in selectedTiles) {
            tile.ChangeTileTo(type);
        }
    }

    private void SelectArea() {
        Vector2 screenMinPos = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 screenMaxPos = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        for (int x = (int)screenMinPos.x; x <= screenMaxPos.x; x++) {
            for (int y = (int)screenMinPos.y; y <= screenMaxPos.y; y++) {
                TrySelect(new Vector3Int(x, y, 0));
            }
        }

        ColorSelection(Color.green);

        actionManager.CurrentAction.UpdateHud();
    }

    private void TrySelect(Vector3Int screenPos) {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        Vector3Int tilemapPos = map.Tilemap.WorldToCell(worldPos);

        if (map.Tiles.TryGetValue(tilemapPos, out Tile tile)) {
            if (selectedTiles.Contains(tile)) return;

            if (typesFilter.Length > 0) {
                foreach (string type in typesFilter) {
                    if (tile.Type.Equals(type)) {
                        if (actionManager.CurrentAction.GetName()
                            .Equals(ActionsNames.BuildEcoduct.Value)) { // if the current action is to build an ecoduct
                            if (CheckTileNeighboursEcoduct(tile)) { // ignore the neighbours of the ecoduct in selection
                                selectedTiles.Add(tile);
                            }
                        } else {
                            selectedTiles.Add(tile);
                        }
                    }
                }
            } else {
                selectedTiles.Add(tile);
            }
        }
    }

    public bool CheckTileNeighboursEcoduct(Tile tile) {
        bool result = true;

        foreach (Tile tile1 in tile.NeighbourTiles) {
            if (tile1.Type.Contains(TileTypes.Ecoduct.Value)) {
                result = false;
                break;
            }
        }

        return result;
    }

    private void UpdateSelectionBox(Vector2 curMousePos) {
        if (!selectionBox.gameObject.activeInHierarchy) {
            selectionBox.gameObject.SetActive(true);
        }

        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    // called when the selection box is released
    private void ReleaseSelectionBox() {
        selectionBox.gameObject.SetActive(false);
    }

    public void ColorSelection(Color color) {
        foreach (Tile tile in selectedTiles) {
            if (tile.RiskFactor > 6) {
                SetTileColor(tile, new Color(255, 50, 0, 120));
            } else {
                SetTileColor(tile, color);
            }
        }
    }

    private void SetTileColor(Tile tile, Color color) {
        tile.Map.Tilemap.SetTileFlags(tile.Coordinates, TileFlags.None);
        tile.Map.Tilemap.SetColor(tile.Coordinates, color);
    }
}
