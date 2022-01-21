using Chess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    private GameObject selectedObject;
    public MainList List;
    public bool Hudworking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&!Hudworking)
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();
                if(hit.collider!=null)
                {
                    if (!hit.collider.CompareTag("drag"))
                    {
                        return;
                      
                    }
                    selectedObject = hit.collider.gameObject;
                    List.Remove(selectedObject);
                    selectedObject.GetComponent<Collider>().enabled = false;
                    selectedObject.GetComponent<TooltipTrigger>().active = false;
                    Cursor.visible = false;
                }
              
            }
            else
            {
                Vector3 position = new Vector3(Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(WorldPosition.x, 0f, WorldPosition.z);
              
                Drop();
            }
        }
        if(selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x,
           Input.mousePosition.y,
           Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(WorldPosition.x, 0.25f, WorldPosition.z);
        }
           
    }
    public void Drop()
    {
        //  selectedObject.GetComponent<Collider>().enabled = true;
        selectedObject.GetComponent<Collider>().enabled = true;
        selectedObject.GetComponent<TooltipTrigger>().active = true;

        Cursor.visible = true;
        selectedObject = null;
    }
    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, 
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x,
           Input.mousePosition.y,
           Camera.main.nearClipPlane);
        Vector3 worldMopusePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMopusePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMopusePosNear, worldMopusePosFar - worldMopusePosNear, out hit);

        return hit;
    }
}
