using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] backgrounds;

    [SerializeField] private Sprite[] sprites;

    private int _currentSprite = 0;

    public void SetNext()
    {
        _currentSprite++;

        if (_currentSprite > sprites.Length - 1)
            _currentSprite = 0;

        for (var i = 0; i < backgrounds.Length; i++)
            backgrounds[i].sprite = sprites[_currentSprite];
    }
}