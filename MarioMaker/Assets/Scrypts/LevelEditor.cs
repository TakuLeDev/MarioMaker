using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] Tilemap mainTilemap;
    [SerializeField] Tilemap highlightTilemap;
    [SerializeField] TileBase ruleTile;
    [SerializeField] Tile highlightTile;
    
    [SerializeField] Camera cam;

    TileBase wishedTile = null;

    private Vector3Int pos;

    private void Update()
    {
        if (IsOverUI())
        {
            highlightTilemap.ClearAllTiles();
            return;
        }
        pos = highlightTilemap.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition));
        HighlightTilemap();
    }

    bool IsOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    void HighlightTilemap()
    {
        highlightTilemap.ClearAllTiles();
        highlightTilemap.SetTile(pos, highlightTile);
    }

    void PlaceTile(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !IsOverUI())
        {
            mainTilemap.SetTile(pos, wishedTile);
        }
    }
}













