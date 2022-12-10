using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultiMarker : MonoBehaviour
{
    [SerializeField] private GameObject[] _arPrefabs;
    [SerializeField] private ARTrackedImageManager _imageManager;

    private readonly Dictionary<string, GameObject> _markerNameAndPrefabDictionary = new Dictionary<string, GameObject>();

    private void Start()
    {
        _imageManager.trackedImagesChanged += OnTrackedImagesChanged;

        for (var i = 0; i < _arPrefabs.Length; i++)
        {
            var arPrefab = Instantiate(_arPrefabs[i]);
            _markerNameAndPrefabDictionary.Add(_imageManager.referenceLibrary[i].name, arPrefab);
            arPrefab.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void ActivateARObject(ARTrackedImage trackedImage)
    {
        var arObject = _markerNameAndPrefabDictionary[trackedImage.referenceImage.name];
        var imageMarkerTransform = trackedImage.transform;

        var markerFrontRotation = imageMarkerTransform.rotation * Quaternion.Euler(90f, 0f, 0f);
        arObject.transform.SetPositionAndRotation(imageMarkerTransform.transform.position, markerFrontRotation);
        arObject.transform.SetParent(imageMarkerTransform);

        arObject.SetActive(trackedImage.trackingState == TrackingState.Tracking);
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            ActivateARObject(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            ActivateARObject(trackedImage);
        }
    }
    
}
