using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace TSU
{
    public class DieRollerElement : VisualElement
    {
        // UxmlFactory and UxmlTraits allow UIBuilder to use CardElement as a building block
        public new class UxmlFactory : UxmlFactory<DieRollerElement, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        private Label history;
        private Pop popup;
        
        public void Init(string dieType,Pop lpopup, Label nhistory)
        {
            history = nhistory;
            popup = lpopup;
            Button button = this.Q<Button>("Button");
            button.text = dieType;
            //button.clicked += new System.Action(roll);
            //button.RegisterCallback<MouseDownEvent>((e) => roll(button.text));
            button.RegisterCallback<ClickEvent>((e) => roll(button.text));

        }
        public void roll(string what)
        {
            
            int resulti = (int)Random.Range(1, float.Parse(what.Split('d')[1]) + 1); ;
            string result = resulti.ToString();

            
            if (popup.visible == false)
            {
                string outtext = what + "\n" + resulti.ToString();
                popup.POP(outtext, 5);
            }
            else//add result
            {
                int prevval = int.Parse(popup.text.Split('\n')[1]);

                string outtext2 = what + "\n" + (resulti + prevval).ToString();
                popup.POP(outtext2, 5);
            }

            history.text += result + ", ";
            
        }
        public void ChangeActiveState(bool isActive)
        {
            this.style.opacity = (isActive) ? .05f : 1f;
        }

        public void MoveAndScaleIntoPosition(int cardSlot, Vector2 position)
        {
            AnimatedMoveTo(position, .2f + (.05f * cardSlot));
            Scale(1f);
        }

        public void Scale(float ratio)
        {
            transform.scale = Vector3.one * ratio;
        }
        public void AnimatedScale(float endScale, float tweenDuration)
        {
            experimental.animation.Scale(endScale, Mathf.RoundToInt(tweenDuration * 1000)).Ease(Easing.OutQuad);
        }

        public void MoveTo(Vector2 screenPosition)
        {
            transform.position = new Vector3(screenPosition.x, screenPosition.y, transform.position.z);
        }
        public void AnimatedMoveTo(Vector2 endPosition, float tweenDuration)
        {
            experimental.animation.Position(new Vector3(endPosition.x, endPosition.y, transform.position.z),
                Mathf.RoundToInt(tweenDuration * 1000)).Ease(Easing.OutQuad);
        }
        public void Translate(Vector2 screenPositionDelta)
        {
            transform.position += (Vector3)screenPositionDelta;
        }

        public void SetAsLastSibling()
        {
            BringToFront();
        }

        public void Delete()
        {
            RemoveFromHierarchy();
        }
    }
}