using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tilemap highlightTilemap;
    [SerializeField] TileBase ruleTile;
    [SerializeField] TileBase highlightTile;
    
    [SerializeField] Camera cam;

    TileBase wishedTile;
    Vector3Int pos;

    private void Awake()
    {
        wishedTile = ruleTile;
    }

    private void Update()
    {
        pos = highlightTilemap.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition));
        HighlightTilemap();
        if (Input.GetMouseButton(0)) PlaceTile();
        if (Input.GetMouseButton(1)) RemoveTile();
        
    }
    
    void HighlightTilemap()
    {
        highlightTilemap.ClearAllTiles();
        highlightTilemap.SetTile(pos, highlightTile);
    }

    void PlaceTile()
    {
        tilemap.SetTile(pos, wishedTile);
    }
    
    void RemoveTile()
    {
        tilemap.SetTile(pos, null);
    }
}













