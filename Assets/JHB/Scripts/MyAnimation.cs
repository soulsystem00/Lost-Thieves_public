using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyAnimation : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] float passTime;
    [SerializeField] Image ui;
    [SerializeField] AudioSource _BGM;
    float curTime = 0;
    int curIndex = 0;

    public Action OnAnimOver;
    private void Start()
    {
        ui.sprite = sprites[curIndex];
    }
    public void HandleUpdate()
    {
        int prevIndex = curIndex;
        if (ui.color.a >= 1)
            curTime += Time.deltaTime;
        if(curTime >= passTime)
        {
            curIndex++;
            if (curIndex >= sprites.Count)
            {
                curIndex = 0;
                OnAnimOver();
            }
            else
            {
                curTime = 0;
                ChangeImage();
            }
            //ui.sprite = sprites[curIndex];
        }
    }

    /*public void Update()
    {
        int prevIndex = curIndex;
        if(ui.color.a >= 1)
            curTime += Time.deltaTime;
        if (curTime >= passTime)
        {
            curIndex++;
            if (curIndex >= sprites.Count)
            {
                curIndex = 0;
                OnAnimOver();
            }
            //ui.sprite = sprites[curIndex];

        }
        if (prevIndex != curIndex)
        {
            ChangeImage();
            curTime = 0;
        }
    }*/
    void ChangeImage()
    {
        StartCoroutine(Fadeaway());
    }
    IEnumerator Fadeaway()
    {
        while(ui.color.a > 0)
        {
            var tmp = ui.color.a - 0.01f;
            ui.color = new Color(ui.color.r, ui.color.g, ui.color.b, tmp);
            yield return new WaitForSeconds(0.01f);
        }

        ui.sprite = sprites[curIndex];

        while (ui.color.a < 1)
        {
            var tmp = ui.color.a + 0.01f;
            ui.color = new Color(ui.color.r, ui.color.g, ui.color.b, tmp);
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void SetSound(AudioClip bgm)
    {
        _BGM.clip = bgm;
        _BGM.Play();
    }
}
