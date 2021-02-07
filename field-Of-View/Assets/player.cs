using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    Rigidbody body ;
    Camera cam;

    public float speed = 6000000000000;
    Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        body=GetComponent<Rigidbody>();
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        //get mouse position using screen to world point from camera
        Vector3 mousepos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,cam.transform.position.y));
    
        //make character look towards mouseposition
        transform.LookAt(mousepos+Vector3.up*transform.position.y);

        //determine velocity of player by getting horizintal and vertical axis input
        velocity = new Vector3 (Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized * speed;
    
    }

    void FixedUpdate(){
        //as our plyer is a rigid body we update its position in a special method, 
        //here the previous position and velocity with the time is being used to update the position every frame 
        body.MovePosition(body.position+velocity*Time.fixedDeltaTime);
        
    }
}
