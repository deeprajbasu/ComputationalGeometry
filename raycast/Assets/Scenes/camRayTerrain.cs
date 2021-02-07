using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camRayTerrain : MonoBehaviour
{
    public Transform placed;
    public Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            //create a ray from camera to mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit h; 

        if (Physics.Raycast(ray,out h)){
            // if ray hits anything draw greenline 
            placed.position=h.point;
        }
    
    
    }
}
