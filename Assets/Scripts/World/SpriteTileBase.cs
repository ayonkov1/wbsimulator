using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpriteTileBase : TileBase {
    private Sprite sprite;

    public Sprite Sprite { get => sprite; set => sprite = value; }

    public SpriteTileBase (Sprite sprite) {
        this.sprite = sprite;
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData) {
        tileData.sprite = sprite;
    }
}
