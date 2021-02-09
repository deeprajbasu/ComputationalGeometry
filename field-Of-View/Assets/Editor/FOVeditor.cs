using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (FOV))]
public class FOVEditor : Editor {

	void OnSceneGUI() {
        //draw screen GUI to visualize feild of view

		FOV fow = (FOV)target;//get field of view info from tartget

		Handles.color = Color.white;


		Handles.DrawWireArc (fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);//draw circular arc 
		
        //using +- of view angle degree, to get vectors that will help us in visulizing field of view
        //using the direction method we wrote 
        Vector3 viewAngleA = fow.direction (-fow.viewAngle / 2, false);//represent limits of view angle
		Vector3 viewAngleB = fow.direction (fow.viewAngle / 2, false);

		Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);//limit 1
		Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);//limit 2

		Handles.color = Color.red;
		foreach (Transform visibleTarget in fow.visibleTargets) {
			Handles.DrawBezier(fow.transform.position,visibleTarget.position,fow.transform.position,visibleTarget.position,
								 Color.red,null,25);//draw sight line from player to visible targets

		}
	}

}