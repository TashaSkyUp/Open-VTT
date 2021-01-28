using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;
using UnityEditor;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UIElements.Experimental;


public class initAbleVisualElement : VisualElement { public void init() { }
}


public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    private UIDocument mydoc;
    private Button HistoryClearButton;
    public VisualTreeAsset dieRoller;
    private Button DieButton01;
    private VisualElement Right;
    private static VisualElement TokenMenu;
    private Label History;
    private Pop PopUp;
    
    public enum UISTATES { None, dragToken, dragMap, pinch, Pause };
    public static UISTATES uISTATE = UISTATES.None;

    public GameObject map;
    private List<TSU.DieRollerElement> dice = new List<TSU.DieRollerElement>();
    public Renderer maprenderer;
    public GameObject TokenPrefab;
    public static UISTATES UISTATE {
        get { return uISTATE; }
        set { uISTATE = value;print(uISTATE); }
    }
    private static Token mSelectedToken;
    public static Token selectedToken {
        get { return mSelectedToken; }
        set {
            mSelectedToken=value;
            if (selectedToken != null) {
                TokenMenu.style.display = DisplayStyle.Flex; 
            } else {
                TokenMenu.style.display = DisplayStyle.None; 
            }
           
        }
    }

    private Texture2D PickImage(int maxSize=-1)
    {
        Texture2D texture= new Texture2D(2,2);
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery( (path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                maprenderer.material.mainTexture   = NativeGallery.LoadImageAtPath(path, maxSize,false);
                float w = (float) maprenderer.material.mainTexture.width;
                float h = (float) maprenderer.material.mainTexture.height;
                
                float s = 10/Mathf.Sqrt(w*h);
                
                map.transform.localScale = new Vector3(w* s, h *s, 1);
            }
        }, "Select a PNG image", "image/png");
        
        Debug.Log("Permission result: " + permission);

        return texture;
        
    }
    
    void Start()
    {
        mydoc = gameObject.GetComponent<UIDocument>();
        //wakeup custom visual elements        
        //TSU.TokenMenu TM = mydoc.rootVisualElement.Q<TSU.TokenMenu>();
        //TM.init();



        HistoryClearButton = mydoc.rootVisualElement.Q<Button>("HistoryClear");
        HistoryClearButton.clicked += new System.Action(() => History.text = "");
        History = mydoc.rootVisualElement.Q<Label>("History");

        var OpenImageMapButton = mydoc.rootVisualElement.Q<Button>("LoadMapImage");
        OpenImageMapButton.clicked += new System.Action(() => PickImage());

        var AddTokenButton = mydoc.rootVisualElement.Q<Button>("AddToken");
        AddTokenButton.clicked += new System.Action(() => AddToken());

        PopUp = mydoc.rootVisualElement.Q<Pop>("Pop"); PopUp.Init();
        Right = mydoc.rootVisualElement.Q<VisualElement>("right");
        
        TokenMenu = mydoc.rootVisualElement.Q<VisualElement>("TokenMenu");
        TokenMenu.style.display = DisplayStyle.None;

        setupdie();

    }
    private void AddToken()
    {
        GameObject.Instantiate(TokenPrefab,null);
    }

    private void setupdie()
    {

        string[] diecodes = { "1d4", "1d6", "1d8","1d10","1d12","1d20" };
        foreach (var diecode in diecodes)
        {
            var newdie = dieRoller.CloneTree();
            var what = newdie.Q<TSU.DieRollerElement>("DieRollerElement");
            what.Init(diecode, PopUp, History);
            dice.Add(what);
            Right.Add(what);

        }

    }

    // Update is called once per frame
    void Update()
    {
       
        PopUp.update();
    }
    void roll()
    {

    }
}
