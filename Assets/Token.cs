using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Token :ISavable
{
    public Vector3 position = new Vector3(0, 0, 0);
    public float scale;
    public Color Color;
    public int id;
    public string name = "token name";
    
    public void Load(string SceneName)
    {
        throw new System.NotImplementedException();
    }

    public void Save()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
