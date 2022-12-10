using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageRecognition : MonoBehaviour
{
    [SerializeField] private MarkerModelSwitcher markerModelSwitcher;
    [SerializeField] private ARTrackedImageManager aRTrackedImageManager;

    private int _presentMarkerNum = -1;

    private void Awake()
    {
        aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        string _name;
        int _nameNum;

        foreach (var a in args.updated)
        {
            _name = a.referenceImage.name;
            _nameNum = int.Parse(_name);
            Vector3 markerPos = a.transform.position;
            Quaternion qua = a.transform.rotation;

            if (markerModelSwitcher._arObj != null)
            {
                markerModelSwitcher._arObj.transform.position = markerPos;
                markerModelSwitcher._arObj.transform.rotation = qua;
            }

            if (a.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                if (_nameNum == _presentMarkerNum) return;

                markerModelSwitcher.SwitchingObject(_nameNum, markerPos, qua);
                _presentMarkerNum = _nameNum;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
