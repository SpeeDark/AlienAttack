using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneLoader.SwitchScene("Main");
    }
}