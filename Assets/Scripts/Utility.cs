using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Utility
{
    private static ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public enum ControlMode
    {
        baseMode,
        rotateMode,
        scaleMode,
    }

    static Utility()
    {
        raycastManager = GameObject.FindObjectOfType<ARRaycastManager>();
    }

    public static bool Raycast(Vector2 screenPosition, out Pose pose)
    {
        if (raycastManager.Raycast(screenPosition, hits, TrackableType.Planes))
        {
            pose = hits.Last().pose;
            return true;
        }
        else
        {
            pose = Pose.identity;
            return false;
        }
    }

    public static bool TryGetInputPosition(out Vector2 position)
    {
        position = Vector2.zero;

        if (Input.touchCount == 0)
        {
            return false;
        }

        position = Input.GetTouch(0).position;

        if (Input.GetTouch(0).phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }
}
