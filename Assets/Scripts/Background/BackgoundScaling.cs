using UnityEngine;

public class BackgoundScaling : MonoBehaviour
{
    private GameObject backgroundImage;

    private Camera mainCamera;

    private void Awake()
    {
        backgroundImage = this.gameObject;

        mainCamera = Camera.main;

        scaleBackgroundFitScreen();
    }

    private void scaleBackgroundFitScreen()
    {
        // Get Device Screen Aspect
        Vector2 deviceScreenResolution = new Vector2(Screen.width, Screen.height);

        var DEVICE_SCREEN_ASPECT = deviceScreenResolution.x / deviceScreenResolution.y;

        // Set Main Camera's aspect = Device's Aspect
        mainCamera.aspect = DEVICE_SCREEN_ASPECT;

        // Scale Background Image to fit with Camera's Size
        var cameraHeight = 200.0f * mainCamera.orthographicSize;
        var cameraWidth = cameraHeight * DEVICE_SCREEN_ASPECT;

        // Get Background Image Size;
        SpriteRenderer backgroundImageSR = backgroundImage.GetComponent<SpriteRenderer>();

        var backgroundImageWidth = backgroundImageSR.sprite.rect.width;

        // Caculate Ratio for scaling
        float Img_scale_ratio_Width = cameraWidth / backgroundImageWidth;

        backgroundImage.transform.localScale = new Vector3(Img_scale_ratio_Width, 1f, 1f);
    }
}