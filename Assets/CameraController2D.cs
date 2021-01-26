using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    private Camera camera;
    public float moveSpeed=1;
    bool DraggingCamera=false;
    Vector3 startPos;
    Vector3 endPos;    
    LineRenderer lineRenderer;

    
    Vector3 camOffset = new Vector3(0, 0, 10);
    [SerializeField] AnimationCurve ac;
    // Start is called before the first frame update
    void Start()
    {
        if (lineRenderer == null) { lineRenderer = gameObject.AddComponent<LineRenderer>(); }
        camera = Camera.main;




    }
    public Object GetInputObject()
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

        Vector3 mouseScreenPos3d = new Vector3(ScrrenPos.x, ScrrenPos.y, 0);
        var mouseRay = Camera.main.ScreenPointToRay(mouseScreenPos3d);

        RaycastHit HitInfo;

        var TokenHit = Physics.Raycast(mouseRay, out HitInfo);
        if (HitInfo.collider != null)
        {
            return HitInfo.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (
            (UIController.UISTATE == UIController.UISTATES.None) |
            (UIController.UISTATE == UIController.UISTATES.dragMap) |
            (UIController.UISTATE == UIController.UISTATES.pinch)
            )
        {
            Touch t0 = new Touch();
            Touch t1 = new Touch();

            var mb0 = Input.GetMouseButton(0);
            var mb1 = Input.GetMouseButton(1);
            var mb2 = Input.GetMouseButton(2);

            var mbd0 = Input.GetMouseButtonDown(0);
            var mbd1 = Input.GetMouseButtonDown(1);
            var mbd2 = Input.GetMouseButtonDown(2);

            var mbu0 = Input.GetMouseButtonUp(0);
            var mbu1 = Input.GetMouseButtonUp(1);
            var mbu2 = Input.GetMouseButtonUp(2);

            if (Input.touchCount == 0) { DraggingCamera = false; }
            if (Input.touchCount >= 1) { t0 = Input.GetTouch(0); DraggingCamera = true; }
            if (Input.touchCount >= 2) { t1 = Input.GetTouch(1); }
            
            
            //print(Screen.width);
            //print(t0.position.x);
            if (t0.position.x > ((float)Screen.width * .8f)) { return; }
            if (Input.mousePosition.x > ((float)Screen.width * .8f)) { return; }

            /// just logging
            if (Input.touchCount >= 1)
            {
                print("Touched:" + Input.touchCount.ToString());
            }
            if (mb0 | mb1 | mb2)
            {
                print("Mouse input:" + mb0.ToString() + mbd0.ToString() + mbu0.ToString());
            }
            ///

            //test for dragmap
            if (Input.GetMouseButtonDown(0))//initial press
            {
                startPos = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
                DraggingCamera = true;
                UIController.UISTATE = UIController.UISTATES.dragMap;
                UIController.selectedToken = null;
                return;
            }
            if (UIController.UISTATE == UIController.UISTATES.dragMap)
            {
                if (Input.GetMouseButton(0))//continued press
                {
                    endPos = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    lineRenderer.enabled = false;
                    DraggingCamera = false;
                    UIController.UISTATE = UIController.UISTATES.None;
                }
                if (Input.touchCount > 0) { mb0 = false; mbd0 = false; }


                if (mbd0 | mb0)
                {
                    SetLineRenderer();
                    MoveCamera();
                }
            }

             if (Input.touchCount == 1)//dragmap via touch
            {
                UIController.UISTATE = UIController.UISTATES.dragMap;
                t1 = Input.GetTouch(0);
                var pstart = t1.position - t1.deltaPosition;
                var pend = t1.position;
                var v3pstart = new Vector3(pstart.x, pstart.y, 0);
                var v3pend = new Vector3(pend.x, pend.y, 0);

                var WPStart = camera.ScreenToWorldPoint(v3pstart);
                var WPEnd = camera.ScreenToWorldPoint(v3pend);
                var WPDir = WPEnd - WPStart;

                transform.position -= WPDir;

            }
            else if (Input.touchCount == 2)
            {//pinch via touch
                var o0p = t0.position - t0.deltaPosition;
                var o1p = t1.position - t1.deltaPosition;
                var od = (o0p - o1p).magnitude;

                var p0 = t0.position;
                var p1 = t1.position;
                var nd = (p0 - p1).magnitude;

                var cd = nd - od;

                camera.orthographicSize+=(cd * .005f);
                camera.orthographicSize = Mathf.Max(.5f, camera.orthographicSize);
            }
            if (Input.mouseScrollDelta.y != 0)
            {
                camera.orthographicSize += (Input.mouseScrollDelta.y * .1f);
                camera.orthographicSize = Mathf.Max(.5f, camera.orthographicSize);

            }


        }
    }


public void SetLineRenderer()
    {
               
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.useWorldSpace = true;
        lineRenderer.widthCurve = ac;
        lineRenderer.numCapVertices = 10;
        lineRenderer.SetPosition(1, endPos);

        Vector3 dir = endPos - startPos;        
        
        
        var NewstartPos = endPos - dir.normalized;
        startPos = (NewstartPos * .025f) + (startPos * .975f);
        lineRenderer.SetPosition(0, startPos);

    }

    private void MoveCamera()
    {
        var dirVector = lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0);
        var localPosition = (-dirVector * moveSpeed * Time.deltaTime) + transform.localPosition;
        transform.localPosition = localPosition;
    }

    public void TouchMove()
    {
        
    }
    public void MouseMove()
    {

    }
    public void CamMove()
    {
        
    }

}
