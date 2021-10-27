using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HitThief : MonoBehaviour
{
    private readonly float RECOVERY_IMG = 0.5f;
    
    [SerializeField] private Transform[] hitPosList;
    [SerializeField] private PoolManager hitEffectPool;

    [SerializeField] private SpriteRenderer sprRend;

    private float curTime;
    private bool isPain = false;
    
    [SerializeField] Sprite[] sprite0 = new Sprite[3];
    [SerializeField] Sprite[] sprite1 = new Sprite[3];
    [SerializeField] Sprite[] sprite2 = new Sprite[3];
    [SerializeField] Sprite[] sprite3 = new Sprite[3];

    private int charId = 0;
    
    private enum faceID
    {
        IDLE = 0,
        HIT,
        FEVER
    };

    private void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();

        var sps = charId = (int)GameSceneManager.GSM.GetPlayerSelectObjectType();
        switch (charId)
        {
            case 0:
                sprRend.sprite = sprite0[0];
                break;
            case 1:
                sprRend.sprite = sprite1[0];
                break;
            case 2:
                sprRend.sprite = sprite2[0];
                break;
            case 3:
                sprRend.sprite = sprite3[0];
                break;
        }
        curTime = Time.realtimeSinceStartup;
    }
    
    private void Update()
    {
        if (isPain)
        {
            if (Time.realtimeSinceStartup - curTime >= RECOVERY_IMG)
            {
                switch (charId)
                {
                    case 0:
                        sprRend.sprite = sprite0[0];
                        break;
                    case 1:
                        sprRend.sprite = sprite1[0];
                        break;
                    case 2:
                        sprRend.sprite = sprite2[0];
                        break;
                    case 3:
                        sprRend.sprite = sprite3[0];
                        break;
                }
                
                isPain = false;
                curTime = Time.realtimeSinceStartup;
            }
        }
    }

    /// <summary>
    /// 캐릭터를 때립니다.
    /// </summary>
    /// <param name="fever"></param>
    public void OnHit(bool fever)
    {
        isPain = true;
        curTime = Time.realtimeSinceStartup;
        
        switch (charId)
        {
            case 0:
                sprRend.sprite = sprite0[1];
                break;
            case 1:
                sprRend.sprite = sprite1[1];
                break;
            case 2:
                sprRend.sprite = sprite2[1];
                break;
            case 3:
                sprRend.sprite = sprite3[1];
                break;
        }
        int hitIndex = Random.Range(0, hitPosList.Length);

        GameObject punchEffect = hitEffectPool.ObjectDequeue("PunchEffect");
        punchEffect.GetComponent<PunchEffect>().Init(hitEffectPool, fever);
        punchEffect.transform.position = hitPosList[hitIndex].position;
    }
    
}
