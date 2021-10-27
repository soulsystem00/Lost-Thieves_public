using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour
{
    private readonly float DELAY_TIME = 4f;

    [SerializeField] private GameObject resultPanel;
    
    [SerializeField] private Text theifNumText;
    [SerializeField] private Text scoreText;

    private bool isGetPress = false;

    [SerializeField] private SoundManager soundManager;
    
    private void Start()
    {
        soundManager.PlaySFX("GameOverSFX");
        Invoke(nameof(OnResultPanel), DELAY_TIME);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGetPress)
            {
                GameSceneManager.GSM.LoadSceneAsync("0_Title");
                GameSceneManager.GSM.UnLoadSceneAsync("5_GameOver");
            }
        }
    }

    /// <summary>
    /// 결과창을 보여줍니다.
    /// </summary>
    public void OnResultPanel()
    {
        soundManager.PlaySFX("ResultSFX");

        resultPanel.SetActive(true);
        StartCoroutine(CountNumber(theifNumText, 0, 5)); 
        StartCoroutine(CountNumber(scoreText, 0, GameSceneManager.GSM.GetTotalScore())); 
      //StartCoroutine(CountNumber(0, GameSceneManager.GSM.sce()));
      //StartCoroutine(CountNumber(0, GameSceneManager.GSM.GetTotalScore()));
        isGetPress = true;
    }
    
    /// <summary>
    /// 글자를 카운팅하는 효과를 보여줍니다.
    /// </summary>
    /// <param name="curNum">현재 숫자</param>
    /// <param name="targetNum">원하는 목표 값</param>
    /// <returns></returns>
    private IEnumerator CountNumber(Text targetText, float curNum, float targetNum)
    {
        float speed = 0.7f;
        float offset = (targetNum - curNum) / speed;

        while (curNum < targetNum)
        {
            curNum += offset * Time.deltaTime;
            targetText.text = ((int) curNum).ToString();
            yield return null;
        }

        curNum = targetNum;
        targetText.text = ((int) curNum).ToString();
    }
}
