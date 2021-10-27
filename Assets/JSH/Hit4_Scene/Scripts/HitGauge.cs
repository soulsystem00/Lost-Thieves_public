using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class HitGauge : MonoBehaviour
{
    private readonly int MAX_GAUGE = 100;
    private readonly int MIN_GAUGE = 0;
    private readonly int ADD_SCORE = 100;
    private readonly float FEVER_ADD_RATIO = 2.2f;

    [SerializeField] private Color normalGaugeColor;
    [SerializeField] private Color feverGaugeColor;

    [SerializeField] private Transform feverLineTrans;
    private bool isFever = false;

    [SerializeField] private Image hitGaugeInImg;
    [SerializeField] private Image hitGaugeOutImg;

    [Range(0, 1)] public float FeverRatio;
    private float hitGaugeValue = 0;

    [SerializeField] private GameObject feverEffect;
    private float curTime;

    private float increaseGauge = 5;
    private float decreaseGauge = 0.5f;

    private HitScore hitScore;
    private TimeGauge timeGauge;
    private HitThief hitThief;

    public SoundManager soundManager;

    [SerializeField] private GameObject punchMotion;
    private bool isPunch = false;
    
    

    private void Start()
    {
        hitScore = FindObjectOfType<HitScore>();
        timeGauge = FindObjectOfType<TimeGauge>();
        soundManager = FindObjectOfType<SoundManager>();
        hitThief = FindObjectOfType<HitThief>();
        curTime = Time.realtimeSinceStartup;
        punchTime = Time.realtimeSinceStartup;
        punchMotion.SetActive(false);
        soundManager.PlayBGM("HitBGM");
        //SetFeverLine();
    }

    private void SetFeverLine()
    {
        Vector3 feverPos = UtilLib.GetImagesize(hitGaugeOutImg.gameObject);
        feverLineTrans.position = new Vector3(feverLineTrans.position.x + (feverPos.x * FeverRatio),
            feverLineTrans.position.y, feverLineTrans.position.z);
    }

    private void Update()
    {
        GaugeUpdate();
        FeverCheck();
        DecreaseGauge();
        PunchVFX();
        
        if (!timeGauge.GetTimeOver())
            IncreaseGauge();
    }

    private void FeverCheck()
    {
        if (hitGaugeValue >= (MAX_GAUGE * FeverRatio))
        {
            isFever = true;
            hitGaugeInImg.color = feverGaugeColor;
            feverEffect.SetActive(true);
            decreaseGauge = Mathf.Lerp(decreaseGauge, 4f, 0.3f * Time.deltaTime);
        }
        else
        {
            isFever = false;
            hitGaugeInImg.color = normalGaugeColor;
            feverEffect.SetActive(false);
            decreaseGauge = 0.75f;
        }
    }


    private void DecreaseGauge()
    {
        if (Time.realtimeSinceStartup - curTime >= 0.025f)
        {
            if (hitGaugeValue - decreaseGauge >= 0)
            {
                hitGaugeValue -= decreaseGauge;
            }
            else
            {
                hitGaugeValue = MIN_GAUGE;
            }

            curTime = Time.realtimeSinceStartup;
        }
    }

    public void IncreaseGauge()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (hitGaugeValue + increaseGauge <= MAX_GAUGE)
            {
                hitGaugeValue += increaseGauge;
            }
            else
            {
                hitGaugeValue = MAX_GAUGE;
            }

            if (isFever)
            {
                soundManager.PlaySFX("FeverPunchSFX");
            }
            else
            {
                soundManager.PlaySFX("NormalPunchSFX");
            }
            
            hitThief.OnHit(isFever);
            int addScore = (int) (isFever ? ADD_SCORE * FEVER_ADD_RATIO : ADD_SCORE);
            hitScore.AddScore(addScore, isFever);
            punchMotion.SetActive(true);
            isPunch = true;
            punchTime = Time.realtimeSinceStartup;
        }
    }

    private float punchTime = 0;
    private void PunchVFX()
    {
        if (Time.realtimeSinceStartup - punchTime >= 0.05f)
        {
            punchMotion.SetActive(false);
            isPunch = false;
        }
    }
    
    private void GaugeUpdate()
    {
        hitGaugeInImg.fillAmount = (float) hitGaugeValue / (float) MAX_GAUGE;
    }
}