using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Application = UnityEngine.Application;

public class LevelManager : MonoBehaviour
{
    
    public bool inEditMode = true;
    [SerializeField] InputField inputField;
    
    public static LevelManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    
    public Tilemap tilemap;

    void Update() //test
    {
        if (Input.GetKeyDown(KeyCode.Q)) TilemapToString("testLevel");
        if (Input.GetKeyDown(KeyCode.E)) JsonToTilemap("testLevel");
    }
    
    
    #region Converters
    
    // outputs a string of the tilemap data in a json representation
    string TilemapToString(string desiredName)
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
        /*File.WriteAllText(Application.dataPath +"/Saves/" + desiredName + ".json",json);
        print("lvl saved");*/
    }
    
    //loads level from targeted .json file
    void JsonToTilemap(string targetFile)
    {
        
        if (!File.Exists(Application.dataPath + "/Saves/" + targetFile))
        {
            print("no lvl saved"); return;
        }
        string json = File.ReadAllText(Application.dataPath +"/Saves/" + targetFile + ".json");
        LevelData lvlData = JsonUtility.FromJson<LevelData>(json);
        
        tilemap.ClearAllTiles();

        for (int i = 0; i < lvlData.poses.Count; i++)
        {
            tilemap.SetTile(lvlData.poses[i], lvlData.tiles[i]);
        }
        print("lvl loaded");
    }
    
    #endregion
    
    
    
    #region RemoteSaves

    void UploadLvlToRemote()
    {
        
    }

    void GetLvlJsonRemote()
    {
        
    }
    
    void GetLvlInfosRemote()
    {
        
    }

    void UpdateLvlRemote() //calls the delete waits for a positive result and upload functions
    {
        
    }
    
    void DeleteLvlRemote()
    {
        
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
