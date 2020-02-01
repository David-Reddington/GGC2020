using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{

    Transform mTransform;

    float mainSpeed = 10.0f; //regular speed
    //float shiftAdd = 40.0f; //multiplied by how long shift is held.  Basically running
    //float maxShift = 50.0f; //Maximum speed when holdin gshift
    //float camSens = 0.25f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    //bool bIsAttached = false;

    // Start is called before the first frame update
    void Start()
    {
        mTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mTransform.position;
        transform.rotation = mTransform.rotation;
        //bIsAttached = false;

        //if (Input.GetMouseButton(1))
        //{
        //    lastMouse = Input.mousePosition - lastMouse;
        //    lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        //    lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        //    transform.eulerAngles = lastMouse;
        //    //lastMouse = Input.mousePosition;
        //    //Mouse  camera angle done. 
        //}
        //lastMouse = Input.mousePosition;


        //Keyboard commands
        //float f = 0.0f;
        Vector3 p = GetBaseInput();

        totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
        p = p * mainSpeed;

        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.Space))
        { //If player wants to move on X and Z axis only
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else
        {
            transform.Translate(p);
        }
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        Vector3 camPos = transform.position;

        if (Input.GetKey(KeyCode.W) && camPos.z > -2.0f)
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S) && camPos.z < 25.0f)
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A) && camPos.x < 15.0f)
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) && camPos.x > -15.0f)
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
