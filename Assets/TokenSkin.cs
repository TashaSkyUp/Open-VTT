using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TokenSkin : MonoBehaviour
{
    private Token token = new Token();
    public static int lastTokenID;
    public Color color;
    public int id;
    
    public float scale = 1;
    public Color Color {
        get => Token.Color;
        set => updateColor(value);
    }
    public float size { 
        get { return Token.scale*100; } 
        set { UpdateScale(value); }
    }
    [SerializeField]
    public Token Token { get => token; set { token = value;UpdateSkin();} }

    public string Name { get =>  token.name;  set =>  token.name = value;  }
    public void Update()
    {
        if (transform.hasChanged)
        {
            UpdatePosition(transform.localPosition);
        }
    }
    private void UpdateSkin()
    {
        updateColor(token.Color);
        updateName (token.name);
        UpdateScale(token.scale*100);
        UpdatePosition(token.position);
    }

    private void UpdatePosition(Vector3 position)
    {
        transform.localPosition = position;
        token.position = position;
    }

    private void UpdateScale(float scale)
    {
        token.scale = scale / 100;
        transform.localScale = Vector3.one * token.scale;
    }

    private void updateName(string name)
    {
        name = token.name;
    }

    private void updateColor(Color value)
    {
        
        Token.Color = value;
        var renderer = GetComponentInChildren<MeshRenderer>();
        renderer.material.color = Token.Color;

    }

    // Start is called before the first frame update
    void Start()
    {
        Token.id = lastTokenID + 1;
        lastTokenID = id;

    }

    // Update is called once per frame

}
