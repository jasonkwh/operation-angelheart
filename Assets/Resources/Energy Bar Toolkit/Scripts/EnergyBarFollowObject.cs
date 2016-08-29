/*
* Copyright (c) Mad Pixel Machine
* http://www.madpixelmachine.com/
*/

using System;
using UnityEngine;

namespace EnergyBarToolkit {

/// <summary>
/// Makes the object follow another world object.
/// </summary>
[ExecuteInEditMode]
public class EnergyBarFollowObject : MonoBehaviour {

    #region Public Fields

    public GameObject followObject;
    public Camera renderCamera;
    public Vector3 offset;

    #endregion

    #region Unity Methods

    void Start() {
        if (followObject != null) {
            UpdateFollowObject();
        }
    }

    void Update() {
        if (followObject != null) {
            UpdateFollowObject();
        }
    }

    #endregion

    #region Private Methods

    private void UpdateFollowObject() {
        var canvas = GetComponentInParent<Canvas>();
        switch (canvas.renderMode) {
            case RenderMode.ScreenSpaceOverlay:
                UpdateFollowObjectScreenSpaceOverlay();
                break;
            case RenderMode.ScreenSpaceCamera:
                UpdateFollowObjectScreenSpaceCamera();
                break;
            case RenderMode.WorldSpace:
                UpdateFollowObjectWorldSpace();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateFollowObjectScreenSpaceOverlay() {
        if (renderCamera == null) {
            Debug.LogError("Render Camera must be set for the follow script to work.", this);
        }

        var w2 = Screen.width / 2;
        var h2 = Screen.height / 2;

        var worldPoint = renderCamera.WorldToScreenPoint(followObject.transform.position);
        MadTransform.SetLocalPosition(transform, worldPoint + offset - new Vector3(w2, h2));
    }

    private void UpdateFollowObjectScreenSpaceCamera() {
        MadTransform.SetPosition(transform, followObject.transform.position + offset);
    }

    private void UpdateFollowObjectWorldSpace() {
        MadTransform.SetPosition(transform, followObject.transform.position + offset);
    }

    #endregion

    #region Inner and Anonymous Classes
    #endregion
}

} // namespace