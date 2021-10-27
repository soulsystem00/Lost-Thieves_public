using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextEffect : MonoBehaviour
{
    private PoolManager poolManager;
    private Text scoreText;
    private Vector2 startPos;
    private RectTransform rectTrans;

    private int curScore = 0;
    private bool isInit = false;

    [SerializeField] private Color feverColor;
    [SerializeField] private Color normalColor;

    public void InitScore(PoolManager pool, int score, bool fever)
    {
        if (!isInit)
        {
            scoreText = GetComponent<Text>();
            isInit = true;
            poolManager = pool;
        }

        scoreText.rectTransform.anchoredPosition = Vector2.zero;
        curScore = score;
        
        scoreText.color = fever ? feverColor : normalColor;
        scoreText.text = "+" + curScore.ToString();
        
        Invoke(nameof(DestroyTextEffect), 1.0f);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z);
        scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, scoreText.color.a - 0.01f);
    }

    private void DestroyTextEffect()
    {
        poolManager.ObjectEnqueue("ScoreTextEffect", gameObject);
    }
}