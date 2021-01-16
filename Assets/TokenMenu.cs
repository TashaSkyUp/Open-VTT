using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace TSU
{
    public class TokenMenu : VisualElement
    {
        Button ColorButton;
        
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
            ColorButton.RegisterCallback<ClickEvent>((e) => changeColor());
            
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
            this.Q<Label>("TokenIDLabel").text="Token "+ UIController.selectedToken.id.ToString();
            ColorButton.style.backgroundColor = new StyleColor(UIController.selectedToken.Color);
        }

    }
}