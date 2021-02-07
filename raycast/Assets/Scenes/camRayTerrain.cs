using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camRayTerrain : MonoBehaviour
{
    // object to be placed and camera to raycast from 
    public Transform placed;
    public Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //create a ray from camera towards mouse pointer position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit h; //will detect if ray hit something

        if (Physics.Raycast(ray,out h)){//if ray cast from camera hits something, 
        
            // change position of selected object to point where the ray hit 
            placed.position=h.point;
        }
    
    
    }
}
