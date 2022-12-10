using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;



public class ARMarker_UI : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject rawImage;
    [SerializeField] private ARTrackedImageManager aRTrackedImageManager;
    private Texture2D screenShot;


    public void InActivateUI()
    {
        button.SetActive(false);
        rawImage.SetActive(false);
        StartCoroutine(ScreenShot());
    }

    private IEnumerator ScreenShot()
    {
        yield return new WaitForEndOfFrame();

        int width = 800;
        int height = 800;

        screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenShot.Apply();

        button.SetActive(true);
        rawImage.SetActive(true);

        

        SetTrackedImage();
    }

    private void SetTrackedImage()
    {
        RawImage imageTarget = rawImage.GetComponent<RawImage>();
        imageTarget.texture = screenShot;
        aRTrackedImageManager.referenceLibrary = aRTrackedImageManager.CreateRuntimeLibrary();

        if (aRTrackedImageManager.referenceLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {
            mutableLibrary.ScheduleAddImageWithValidationJob(screenShot, "my new image", 0.5f);
        }
        aRTrackedImageManager.enabled = true;
    }

}
