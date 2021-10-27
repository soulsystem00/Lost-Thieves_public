using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchEffect : MonoBehaviour
{
    private PoolManager poolManager;
    private SpriteRenderer sprRend;
    private Color beforeColor;
    
    private bool isInit = false;
    private bool isFever = false;
    
    public void Init(PoolManager pool, bool fever)
    {
        if (!isInit)
        {
            poolManager = pool;
            sprRend = GetComponent<SpriteRenderer>();
            beforeColor = sprRend.color;
            isInit = true;
        }

        if (fever)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
        isFever = fever;
        sprRend.color = beforeColor;
        Invoke(nameof(DestroyTextEffect), 1.0f);
    }

    private void Update()
    {
        sprRend.color = new Color(sprRend.color.r, sprRend.color.g, sprRend.color.b, sprRend.color.a - 0.01f);
    }

    private void DestroyTextEffect()
    {
        poolManager.ObjectEnqueue("PunchEffect", gameObject);
    }
}
