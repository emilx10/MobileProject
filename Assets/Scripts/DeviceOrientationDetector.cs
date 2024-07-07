using UnityEngine;

public class DeviceOrientationDetector : MonoBehaviour
{
    void Update()
    {
        DeviceOrientation orientation = Input.deviceOrientation;

        switch (orientation)
        {
            case DeviceOrientation.Unknown:
                Debug.Log("Unknown orientation");
                break;
            
            case DeviceOrientation.Portrait:
                Debug.Log("Device is in portrait mode");
                break;
            
            case DeviceOrientation.PortraitUpsideDown:
                Debug.Log("Device is in portrait upside-down mode");
                break;
            
            case DeviceOrientation.LandscapeLeft:
                Debug.Log("Device is in landscape left mode");
                break;
            
            case DeviceOrientation.LandscapeRight:
                Debug.Log("Device is in landscape right mode");
                break;
            
            case DeviceOrientation.FaceUp:
                Debug.Log("Device is face up");
                break;
            
            case DeviceOrientation.FaceDown:
                Debug.Log("Device is face down");
                break;
        }
    }
}