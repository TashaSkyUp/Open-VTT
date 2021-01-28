using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
public class Pop : Label
{
    public float display_time_left_sec;
    // UxmlFactory and UxmlTraits allow UIBuilder to use CardElement as a building block
    public new class UxmlFactory : UxmlFactory<Pop, UxmlTraits> { }
    public new class UxmlTraits : Label.UxmlTraits { }
    private VisualElement decoration;
    
    public void Init()
    {
        decoration = this.parent.parent;
        //decoration.visible = false;
        this.parent.RegisterCallback<MouseDownEvent>((e) => { decoration.visible = false;DEPOP(); });
    }
    
    public void POP(string text,float TimeoutSeconds=20,bool click_to_dismiss=true)
    {
        this.text = text;
        decoration.visible = true;
        display_time_left_sec = TimeoutSeconds * 1;
        UIController.UISTATE = UIController.UISTATES.Pause;

    }
    public void update()
    {        
        if (display_time_left_sec <= 0) {
            if (decoration.visible == true)
            {
                DEPOP();
            }

        } else { display_time_left_sec -= Time.deltaTime; }
    }
    public void DEPOP()
    {
        decoration.visible = false;
        UIController.UISTATE = UIController.UISTATES.None;
    }    
}
