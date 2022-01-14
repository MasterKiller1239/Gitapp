using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    bool isMoving = false;
    public Transform startpoint;
    public Vector3 endpoint;
  
    // Settings
    public float movementSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        endpoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (isMoving == true)
        {

            float movementStep = movementSpeed * Time.deltaTime;
            //float rotationStep = rotationSpeed * Time.deltaTime;

            Vector3 directionToTarget = endpoint - transform.position;
            

        


            float distance = Vector3.Distance(transform.position, endpoint);
            if(Vector3.Distance(transform.position, endpoint)<0.01)
            {
                isMoving = false;
            }
           

            transform.position = Vector3.MoveTowards(transform.position, endpoint, movementStep);
        }
    }

    public void Move(Vector3 Newpos)
    {
        isMoving = true;
        startpoint = this.transform;
        endpoint = Newpos;
    }
    public void MovefromZero()
    {
        this.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        //this.transform.position = new Vector3(17.3199997f, 0.129999995f, -16.9599991f);
        isMoving = true;
        startpoint = this.transform;
       
    }

}
