using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    private GameObject sprite;
    private Camera mainCamera;

    private void Awake()
    {
        sprite = this.gameObject;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        scaleAlienSpriteFitScreen();
    }

    private void scaleAlienSpriteFitScreen()
    {
        float DefaultScreenAspect = 16f / 9f;

        Vector3 SpriteScale = new Vector3(transform.localScale.x / DefaultScreenAspect * mainCamera.aspect, transform.localScale.y, 1);

        sprite.transform.localScale = SpriteScale;
    }
}