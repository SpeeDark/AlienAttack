using UnityEngine;
using UnityEngine.UI;

public class MaxScoreViev : MonoBehaviour
{
    [SerializeField] private string _text = "Your max score: ";

    private Text vievText;

    private void Awake()
    {
        vievText = GetComponent<Text>();

        vievText.text = _text + PlayerPrefs.GetInt("MaxScore").ToString();
    }
}