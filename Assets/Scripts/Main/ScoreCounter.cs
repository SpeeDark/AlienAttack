using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private float _score = 0;

    public int Score => (int)_score;

    [SerializeField] private ScoreGUI scoreView;

    public void AddScore(float amount)
    {
        if (amount < 0f)
            throw new ArgumentOutOfRangeException();

        _score += amount;

        scoreView.ChangeScore(Score);
    }
}