using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARTrackedImageManager))]

public class imageTracker : MonoBehaviour
{



	[SerializeField]
	private Text imageTrackedText;


	[SerializeField]
	private GameObject[] arObjectToPlace;






	private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();

	private ARTrackedImageManager trackedImageManager;


	void Awake(){
	

		trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
	


		foreach(GameObject arObject in arObjectToPlace){

			GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
			newARObject.name = arObject.name;
			arObjects.Add(arObject.name, newARObject);
		


	}
}




 void OnEnable(){
	trackedImageManager.trackedImagesChanged += ImageChanged;
}




 void OnDisable(){
		trackedImageManager.trackedImagesChanged -= ImageChanged;

}




void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs){

	foreach(ARTrackedImage trackedImage in eventArgs.added){
		UpdateImage(trackedImage);
	}

	foreach(ARTrackedImage trackedImage in eventArgs.updated){
		UpdateImage(trackedImage);
	}

	foreach(ARTrackedImage trackedImage in eventArgs.removed){
		arObjects[trackedImage.name].SetActive(false);	

	}
}


private void UpdateImage(ARTrackedImage trackedImage){

	

	//Display the name of the tracked image in the canvas
imageTrackedText.text = trackedImage.referenceImage.name;


	//Assign and place the game object
	AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);

	Debug.Log($"trackedImage.referenceImage.name: {trackedImage.referenceImage.name}");


}




void AssignGameObject(string name, Vector3 newPosition){





	if(arObjectToPlace!=null){

		arObjects[name].SetActive(true);
		arObjects[name].transform.position = newPosition;


		foreach(GameObject go in arObjects.Values){

			Debug.Log($"Go in arObjects.Values: {go.name}");

				if(go.name != name){
					go.SetActive(false);
				}
			}

	}



}


}
