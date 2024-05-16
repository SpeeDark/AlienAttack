using UnityEngine;

public class RecordSaver : MonoBehaviour
{
    private string _name = "MaxScore";

    [SerializeField] private ScoreCounter scoreCounter;

    public void SaveRecord()
    {
        if (scoreCounter.Score > PlayerPrefs.GetInt(_name))
        {
            PlayerPrefs.SetInt(_name, scoreCounter.Score);
            PlayerPrefs.Save();
        }
    }
}