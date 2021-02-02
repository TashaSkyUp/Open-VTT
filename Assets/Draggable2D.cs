using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Draggable2D : MonoBehaviour
{
    // Start is called before the first frame update
    bool canMove;
    bool dragging;
    public static UnityEvent evnt=new UnityEvent();
    CapsuleCollider collider;
    void Start()
    {
        collider = GetComponentInChildren<CapsuleCollider>();
        canMove = false;
        dragging = false;

    }
    // Update is called once per frame

    void Update()
    {
        
        Vector2 ScrrenPos;
        if (Input.touchCount == 0)
        {
            ScrrenPos = Input.mousePosition;
        }
        else
        {
            ScrrenPos = Input.GetTouch(0).position;
        }

        Vector3 mouseScreenPos3d = new Vector3(ScrrenPos.x, ScrrenPos.y,0);        
        var mouseRay = Camera.main.ScreenPointToRay(mouseScreenPos3d);

        RaycastHit HitInfo;

        LayerMask TokenMask = LayerMask.GetMask("Token");
        LayerMask MapMask = LayerMask.GetMask("Map");

        if (Input.GetMouseButtonDown(0))
        {

            var TokenHit = Physics.Raycast(mouseRay,out HitInfo,100,TokenMask.value);
                        
            if (HitInfo.collider==collider)
            {
                canMove = true;
                UIController.UISTATE = UIController.UISTATES.dragToken;
                UIController.selectedToken = gameObject.GetComponentInChildren<TokenSkin>();
                evnt.Invoke();
            }
            else
            {
                canMove = false;
            }
            if (canMove)
            {
                dragging = true;
            }


        }
        if (dragging)
        {
            var MapHit = Physics.Raycast(mouseRay, out HitInfo, 100, MapMask.value);
            this.transform.position = HitInfo.point;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (dragging)
            {
                canMove = false;
                dragging = false;
                UIController.UISTATE = UIController.UISTATES.None;
            }
        }
    }
}
