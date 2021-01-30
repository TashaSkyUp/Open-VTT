using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Token : MonoBehaviour
{
    public static int lastTokenID;
    public Color color;
    public int id;
    public new string name = "token name";
    public float scale = 1;
    public Color Color {
        get => color;
        set => updateColor(value);
    }
    public float size { 
        get { return scale*100; } 
        set { scale = value / 100;
            transform.localScale = Vector3.one * scale;}
    }

    private void updateColor(Color value)
    {
        color = value;
        GetComponentInChildren<MeshRenderer>().material.color = color;
    }

    // Start is called before the first frame update
    void Start()
    {
        id = lastTokenID + 1;
        lastTokenID = id;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
