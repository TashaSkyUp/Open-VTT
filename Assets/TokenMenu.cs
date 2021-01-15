using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace TSU
{
    public class TokenMenu : VisualElement
    {
        
        public new class UxmlFactory : UxmlFactory<TokenMenu, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        
        public void Init(string dieType,Pop lpopup, Label nhistory)
        {
            //history = nhistory;
            //popup = lpopup;
            Button button = this.Q<Button>("TokenColorButton");
            button.text = dieType;
            //button.clicked += new System.Action(roll);
            //button.RegisterCallback<MouseDownEvent>((e) => roll(button.text));
            button.RegisterCallback<ClickEvent>((e) => roll(button.text));

        }
        public void roll(string what)
        {
            
          
            
        }

    }
}