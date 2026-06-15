using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public bool inEditMode = true;
    [SerializeField] TMP_InputField LevelNameField;
    public Tilemap tilemap;
    
    public static LevelManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    
    private void OnEnable()
    {
        StartCoroutine(AccountManager.instance.
            GetLvlJson(LevelInfoHolder.instance.lvlID, (temp) =>
            {
                try {
                    JsonToTilemap(temp);
                }
                catch (Exception e) {
                    Debug.LogError("invalid json");
                    throw;
                }
            }));
    }

    public void SaveLvl()
    {
        if (LevelInfoHolder.instance.lvlID == "")
        {
            AccountManager.instance.StartCreateLevel(LevelNameField.text, LevelNameField.text);
        }
        else
        {
            AccountManager.instance.StartUpdateLvl(LevelNameField.text);
        }
        
    }
    
    #region Converters

    // outputs a string of the tilemap data in a json representation
    public string TilemapToString()
    {
        BoundsInt bounds = tilemap.cellBounds;
        LevelData levelData = new LevelData();
        
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (temp != null)
                {
                    levelData.tiles.Add(temp);
                    levelData.poses.Add(new Vector3Int(x, y, 0));
                }
            }
        }

        string json = JsonUtility.ToJson(levelData, true);
        return json;
    }

    //loads level from targeted .json file
    public void JsonToTilemap(string json)
    {
        LevelData lvlData = JsonUtility.FromJson<LevelData>(json);
        
        tilemap.ClearAllTiles();
        
        for (int i = 0; i < lvlData.poses.Count; i++)
        {
            tilemap.SetTile(lvlData.poses[i], lvlData.tiles[i]);
        }
        print("lvl loaded");
    }
    
    #endregion
    
    public class LevelData
    {
        public string name;
        public int id;
        public List<TileBase> tiles = new List<TileBase>();
        public List<Vector3Int> poses =  new List<Vector3Int>();
    }
}
