using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

    public class TokenMenu : VisualElement
    {
        Button ColorButton;
        private TextField NameField;
        private Slider SizeSlider;

        public new class UxmlFactory : UxmlFactory<TokenMenu, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }
        private float rnd() { return Random.Range(.5f, 1f); }

        public TokenMenu()
        {
            this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
            this.RegisterCallback<ClickEvent>(OnAttachToPanel);
            
        }
        

        public void init()
        {
            Debug.Log("INIT");
            
            Draggable2D.evnt.AddListener(updateValues);

            ColorButton = (Button)this.Q("TokenColorButton");
            if (ColorButton != null)
            {
                ColorButton.RegisterCallback<ClickEvent>((e) => changeColor());
                
                this.Q<TextField>().RegisterCallback<ChangeEvent<string>>(e =>
                {
                    Debug.Log(e);
                    UIController.selectedToken.name = e.newValue;
                    updateValues();
                });

                this.Q<Slider>().RegisterCallback<ChangeEvent<float>>(e =>
                {
                    Debug.Log(e);
                    UIController.selectedToken.size = e.newValue;
                    updateValues();
                });

            }

        }
        public override void HandleEvent(EventBase evt)
        {
            Debug.Log("HE: "+ evt.ToString()+" "+this.childCount.ToString());
            base.HandleEvent(evt);
        }

        private void OnAttachToPanel(ClickEvent evt)
        {
            
            Debug.Log(evt);

        }

        void OnGeometryChange(GeometryChangedEvent evt)
        {

            init();

            
        }     
        public void changeColor()
        {
            Debug.Log("test");
            var nc = new Color(rnd(), rnd(), rnd(), 1);
            UIController.selectedToken.Color = nc;
            updateValues();   
            

        }
        public void updateValues()
        {
            if (this.Q<Label>("TokenIDLabel") != null)
            {
                this.Q<Label>("TokenIDLabel").text = "Token " + UIController.selectedToken.id.ToString();
                this.Q<TextField>().SetValueWithoutNotify(UIController.selectedToken.name);
                ColorButton.style.backgroundColor = new StyleColor(UIController.selectedToken.Color);
            }
        }

    }
