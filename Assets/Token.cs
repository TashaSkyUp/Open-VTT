using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    public static int lastTokenID;
    public Color color;
    public int id;
    public Color Color {
        get => color;
        set => updateColor(value);
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
