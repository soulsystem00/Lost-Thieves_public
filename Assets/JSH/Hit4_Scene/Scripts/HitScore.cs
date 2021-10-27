using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HitScore : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private PoolManager textEfManager;
    
    private int curScore = 0;
    
    public int GetScore()
    {
        return curScore;
    }

    public void AddScore(int score, bool fever)
    {
        curScore += score;
        ScoreUpdate();
        
        var textEffect = textEfManager.ObjectDequeue("ScoreTextEffect");
        textEffect.GetComponent<ScoreTextEffect>().InitScore(textEfManager, score, fever);
    }

    public void SetScore(int score)
    {
        curScore = score;
        ScoreUpdate();
    }

    private void ScoreUpdate()
    {
        scoreText.text = curScore.ToString();
    }
}
