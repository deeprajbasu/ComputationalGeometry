using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position,transform.forward);
        //create a ray going from object position to forward direction
        RaycastHit h; 
        //RayCast Hit detector

        if (Physics.Raycast(ray,out h,100)){
            // if ray hits anything draw greenline 
            Debug.DrawLine(ray.origin,h.point,Color.green);

        }else{
            // else draw red linesegment to represent ray
             Debug.DrawLine(ray.origin,ray.origin+ray.direction*100,Color.red);
        }


    }
}
