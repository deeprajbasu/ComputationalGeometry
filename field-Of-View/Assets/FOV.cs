﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FOV : MonoBehaviour {

	public float viewRadius;//how far can the agent see

	[Range(0,360)]//cannot be beyond 360, 360 is circular vision, lower degreens narrows the scope of sight 
	public float viewAngle;//how wide, can the agent see, when looking in front

	public LayerMask targetMask;//get targets from layers
	public LayerMask obstacleMask;//get wall objects from layer

	public float meshRes;//for drawing field of view viualization 

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();//list to strore targets that are currently visible

	void Start() {
        //we start finding targets after a slight delay
		StartCoroutine ("FindTargetsWithDelay", .2f);
	}
	void Update() {
		drawFOVregion();//highlights the entire region of field of view
	}


	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);//wait for a bit
			FindVisibleTargets ();//start finding targets
		}
	}

	void FindVisibleTargets() {
        //this method allows the agent to look for targets and determine if they are visible or not
        //visible targets are recognized and stored

		visibleTargets.Clear ();//start with empy list of targets

        //draw collider sphere from player, for the given view radius,and were dealing with only target objects
        //this collider allows us to recognize the targets within our view radius 
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {//compute for every target that falls inside the collidersphere

			Transform target = targetsInViewRadius [i].transform;//get position of target
			Vector3 dirToTarget = (target.position - transform.position).normalized;//compute direction vector to target

			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
                //if facing target ie. if target is withing view angle, 

				float dstToTarget = Vector3.Distance (transform.position, target.position);//calculate distance from target

                //cast ray from player position towards target direction, till target, 
                //if not colliding with obstacle maask, ie walls
				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					visibleTargets.Add (target);//add target to visible targets list
				}
			}
		}
	}

	void drawFOVregion(){
		//cast rays to highlight entire FOV region
		int numrays =Mathf.RoundToInt(viewAngle*meshRes) ;// round to whole number 
		float raystepSize = viewAngle/numrays;//angle stepsize for subsequent rays. 

		list<Vector3> viewPoints = new list<Vector3>();//store visible points after view region raycasting 

		//cast view rays in the view region and get their info
		for(int i =0; i <= numrays;i++){//for every ray in the view region
			// Vector3 viewAngleA = fow.direction (-fow.viewAngle / 2, false);
			// remember we used something similar to figure out +- limits of players view according to view angle,
			//using our direction method  


			float angle = transform.eulerAngles.y - viewAngle/2 + (raystepSize*i) ;
			//figure out ray angle for ith ray
			
			viewRayInfo viewRay = new viewCast(angle);//cast and capture ith ray info
			viewPoints.Add(viewRay.point);//add end point of ray to list of view points.


		}
		//once we have the view points in the view region 
		//we can try to create a triangulated mesh to better visualize the field of view



	}
	viewRayInfo viewCast(float angl){  //casts rays in the view region
		Vector3 dir = direction(angl);
		// get direction of ray to be cast

		RaycastHit hit; //if ray hit something
		if (Physics.Raycast(transform.position,dir,out hit,viewRadius,obstacleMask)){
			//if ray cast from position till view radius in view region hits any wall 
			//return ray info as our custom datastructure
			
			return new viewRayInfo(true,hit.point,hit.distance,angle);
			
		}else{
			return new viewRayInfo(false, transform.position + dir * viewRadius, viewRadius, angl);
			//return whole lenght ray if not hit anything

		}

	}
	
	public Vector3 direction(float angleInDegrees, bool angleIsGlobal) {
		//output direction vector from view angle degrees
		//used for on screen visualization 
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}


	public struct viewRayInfo {//will store info about cast rays in the view region.
		public bool hit;//if ray hit anything. 
		public Vector3 point;//point of collision
		public float dst;//distance of collision 
		public float angle;// angle at which ray was cast 

		public viewRayInfo(bool _hit, Vector3 _point, float _dst, float _angle) {
			hit = _hit;
			point = _point;
			dst = _dst;
			angle = _angle;
			}
		}	
	}
