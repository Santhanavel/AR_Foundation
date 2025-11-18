using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceObject : MonoBehaviour
{

    [SerializeField]ARRaycastManager raycastManager;
    bool isPlacing = false; 
   
    // Update is called once per frame
    void Update()
    {
        if(!raycastManager)
            return;

        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began && !isPlacing)
        {
            isPlacing = true;

            PlaceObject(Input.GetTouch(0).position);
        }
    }

    void PlaceObject(Vector2 touchPos)
    {
        var rayHit = new List<ARRaycastHit>();

        raycastManager.Raycast(touchPos,rayHit , TrackableType.AllTypes);

        if (rayHit.Count > 0)
        {
            Vector3 hitpos = rayHit[0].pose.position;
            Quaternion hitrot = rayHit[0].pose.rotation;

            Instantiate(raycastManager.raycastPrefab, hitpos, hitrot);
        }
        StartCoroutine(DelayObjectCreation());
    }

    IEnumerator DelayObjectCreation()
    {
        yield return new WaitForSeconds(.25f);
        isPlacing = false;
    }
}
