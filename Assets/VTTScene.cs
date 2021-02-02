using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class handles all data needed to run and reproduce a scene:
// including:
// Tokens
// path and file name of map (maybe will save the image data to)
// Camera position
// Map name
// Scale

[System.Serializable]
public class VTTScene:ISavable
{
    public long _UID=0;
    public string _name = "NoLand";
    public string _image = "old-averoigne-8-768x1148";
    public string _discription = "This is a place for a description";
    public List<Token> _tokens;

    public string Name { get => _name; set {_name = value;Save();} }
    public string Image { get => _image; set { _image = value; Save(); } }
    public string Discription { get => _discription; set { _discription = value; Save(); } }
    public List<Token> Tokens { get => _tokens; set { _tokens = value; Save(); } }
    
    public VTTScene(long uid)
    {
        _UID = uid;
    }
    public string ToJson(){        
        return JsonUtility.ToJson(this);
    }
    public void LoadFromJson(string aJson){
        JsonUtility.FromJsonOverwrite(aJson, this);
    }

    public void Load(string SceneName)
    {
        var json = PlayerPrefs.GetString("Scene_" + SceneName);
        LoadFromJson(json);
    }

    public void Save()
    {
        var json = ToJson();
        PlayerPrefs.SetString("Scene_"+ Name, json);
        PlayerPrefs.SetString("LastScene", Name);
    }
}
public interface ISavable
{
    void Load(string SceneName);
    void Save();
}
