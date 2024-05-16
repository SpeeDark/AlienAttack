using UnityEngine;
using UnityEngine.UI;

public class ScoreGUI : MonoBehaviour
{
    private Text Score;

    private void Awake()
    {
        Score = GetComponent<Text>();
    }

    public void ChangeScore(int scoreValue)
    {
        Score.text = scoreValue.ToString();
    }
}