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
public class Scene
{
    public string Name;
    public string Image;
    public string Discription;
    public List<Token> tokens;

    public string ToJson(){
        return JsonUtility.ToJson(this);
    }
    public void LoadFromJson(string aJson){
        JsonUtility.FromJsonOverwrite(aJson, this);
    }
}
public interface ISavable
{
    void Populate(Scene aScene);
    void Save(Scene aScene);
}
