using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Map : MonoBehaviour {
	[SerializeField] private Sprite boarIcon;
	[SerializeField] private List<TileInfo> tilesInfoList;

	private Dictionary<Vector3Int, Tile> tiles = new Dictionary<Vector3Int, Tile>();
	private Dictionary<string, TileInfo> tilesInfo = new Dictionary<string, TileInfo>();
	private Tilemap tilemap;
	private List<GameObject> boarIcons = new List<GameObject>();

	public Dictionary<Vector3Int, Tile> Tiles { get => tiles; }
	public Dictionary<string, TileInfo> TilesInfo { get => tilesInfo; }
	public List<Tile> TileList { get => tiles.Values.ToList(); }
	public Tilemap Tilemap { get => tilemap; set => tilemap = value; }

	void Start() {
		tilemap = GetComponent<Tilemap>();
		SetTilesInfo();
		SetTiles();
	}

	public int GetTotalBoars() {
		return tiles.Values.Sum(tile => tile.BoarsOnTile.Count);
	}

	public void ResetBoarIcons() {
		foreach (GameObject icon in this.boarIcons) {
			Destroy(icon);
		}

		boarIcons.Clear();

		foreach (Tile tile in tiles.Values) {
			if (tile.BoarsOnTile.Count >= 10) {
				DisplayBoarIcon(tile);
			}
		}
	}

	public void ChangeTileSprite(Vector3Int pos, Sprite sprite) {
		if (!tilemap.HasTile(pos)) return;

		SpriteTileBase newTile = ScriptableObject.CreateInstance<SpriteTileBase>();
		newTile.Sprite = sprite;
		tilemap.SetTile(pos, newTile);
	}

	private void DisplayBoarIcon(Tile tile) {
		GameObject boarIcon = new GameObject();
		SpriteRenderer sR = boarIcon.AddComponent<SpriteRenderer>();
		sR.sprite = this.boarIcon;
		sR.sortingOrder = 1;
		sR.transform.localScale = new Vector3(0.4f, 0.4f, 0);

		var worldPoint = tilemap.CellToWorld(tile.Coordinates);
		boarIcon.transform.position = worldPoint;

		boarIcons.Add(boarIcon);
	}

	private void GenerateNeighbours() {
		foreach (Tile tile in tiles.Values) {
			tile.NeighbourTiles = tile.GetNeighbours(1);
		}
	}

	private void SetTilesInfo() {
        foreach (TileInfo tileInfo in this.tilesInfoList) {
			tilesInfo.Add(tileInfo.Type, tileInfo);
        }
    }

	private void SetTiles() {
		foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin) {
			if (!tilemap.HasTile(pos)) continue;

			Tile tile = null;
			TileBase tb = tilemap.GetTile(pos);

			string key;

			if (tb.name.Any(char.IsDigit)) {
				key = tb.name.Split(' ')[0];
            } else {
				key = tb.name;
            }

			if (tilesInfo.TryGetValue(key, out TileInfo tileInfo)) {
				tile = new Tile(this, tileInfo, pos);
			} else {
				Debug.Log(key); // keep this for when there is an unknown tile added
				throw new UnityException("Tile of Type " + key + " does not exist. Please change its name.");
			}

			/* Plan b when things mess up :) */

			/*if (tb.name.Contains("Grass")) {
				tile = new Tile(this, TileTypes.Grass.Value, 3, 300, 3, 20, false, 0, 0, "", pos);
			} else if (tb.name.Contains("Coniferous Forest Sapling")) {
				tile = new Tile(this, TileTypes.ForestConiferousSapling.Value, 2, 50, 2, 10, true, 1, 0, "Coniferous Forest Small", pos);
			} else if (tb.name.Contains("Coniferous Forest Small")) {
				tile = new Tile(this, TileTypes.ForestConiferousSmall.Value, 2, 200, 2, 30, true, 1, 0, "Coniferous Forest Big", pos);
			} else if (tb.name.Contains("Coniferous Forest Big")) {
				tile = new Tile(this, TileTypes.ForestConiferousBig.Value, 2, 300, 2, 60, false, 0, 0, "", pos);
			} else if (tb.name.Contains("Deciduous Forest Sapling")) {
				tile = new Tile(this, TileTypes.ForestDeciduousSapling.Value, 4, 50, 2, 10, true, 1, 0, "Deciduous Forest Small", pos);
			} else if (tb.name.Contains("Deciduous Forest Small")) {
				tile = new Tile(this, TileTypes.ForestDeciduousSmall.Value, 4, 200, 2, 30, true, 1, 0, "Deciduous Forest Big", pos);
			} else if (tb.name.Contains("Deciduous Forest Big")) {
				tile = new Tile(this, TileTypes.ForestDeciduousBig.Value, 4, 300, 2, 60, false, 0, 0, "", pos);
			} else if (tb.name.Contains("Wheat Crops")) {
				tile = new Tile(this, TileTypes.CropsWheat.Value, 5, 500, 4, 0, false, 0, 0, "", pos);
			} else if (tb.name.Contains("Corn Crops")) {
				tile = new Tile(this, TileTypes.CropsCorn.Value, 6, 500, 4, 0, false, 0, 0, "", pos);
			} else if (tb.name.Contains("Road")) {
				tile = new Tile(this, TileTypes.Road.Value, 1, 100, 5, 20, false, 0, 0, "", pos);
			} else if (tb.name.Contains("Water")) {
				tile = new Tile(this, TileTypes.Water.Value, 0, 0, 5, 0, false, 0, 0, "", pos);
			} else if (tb.name.Contains("Building")) {
				tile = new Tile(this, TileTypes.Building.Value, 1, 200, 4, 10, false, 0, 0, "", pos);
			} else if (tb.name.Contains("Ecoduct")) {
				tile = new Tile(this, TileTypes.Ecoduct.Value, 2, 100, 3, 20, false, 0, 0, "", pos);
			} else {
				tile = new Tile(this, TileTypes.Other.Value, 0, 0, 3, 0, false, 0, 0, "", pos);
			}*/


			tiles.Add(pos, tile);

			
			if (tile.BoarsOnTile.Count >= 10) {
				DisplayBoarIcon(tile);
			}
			
		}

		GenerateNeighbours();
	}
}