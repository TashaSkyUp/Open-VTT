using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace TSU
{
    public class TokenMenu : VisualElement
    {
        Button ColorButton;
        private TextField NameField;

        public new class UxmlFactory : UxmlFactory<TokenMenu, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }        
        private float rnd() { return Random.Range(.5f, 1f); }
        
        public TokenMenu()
        {
            this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);            
            var childMargins = new StyleLength(5.0f);
            
        }

        void OnGeometryChange(GeometryChangedEvent evt)
        {
            Draggable2D.evnt.AddListener(updateValues);
            ColorButton = this.Q<Button>("TokenColorButton");
            if (ColorButton != null)
            {
                ColorButton.RegisterCallback<ClickEvent>((e) => changeColor());
                this.Q<TextField>().RegisterCallback<ChangeEvent<string>>(e =>
                {
                    Debug.Log(e);
                    UIController.selectedToken.name = e.newValue;
                    updateValues();
                });
            }

            //NameField
        }


        public void Init()
        {
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
}