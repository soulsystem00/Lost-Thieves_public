using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSystem : MonoBehaviour
{
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] GameObject scrollUI;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    AudioClip thiefSound;
    public Action OnSelectOver;
    int curSelection = 0;
    bool checker = false;
    private void Start()
    {
        scrollbar.value = -0.17f + (0.45f * (float)curSelection);
    }
    // Update is called once per frame
    public void HandleUpdate()
    {
        if (scrollUI.transform.position.y >= 0 && !source.isPlaying)
        {
            int prevSelection = curSelection;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                curSelection++;
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                curSelection--;
            else if (Input.GetKeyDown(KeyCode.Space) && !source.isPlaying)
            {
                SetSelection();
                OnSelectOver();
            }
            curSelection = Mathf.Clamp(curSelection, 0, 3);
            if (prevSelection != curSelection)
                UpdateScrollSmooth(-0.17f + (0.45f * (float)curSelection));
        }

        if(checker && !source.isPlaying)
        {
            LoadScene();
        }
    }
    /*public void Update()
    {
        if (scrollUI.transform.position.y >= 0)
        {
            int prevSelection = curSelection;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                curSelection++;
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                curSelection--;
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                SetSelection();
                OnSelectOver();
            }
            curSelection = Mathf.Clamp(curSelection, 0, 4);
            if (prevSelection != curSelection)
                UpdateScrollSmooth(-0.17f + (0.45f * (float)curSelection));
        }
    }*/
    public void UpdateScrollSmooth(float value)
    {
        if (value < scrollbar.value)
            StartCoroutine(UpdateDownScrollSmooth(value));
        else
            StartCoroutine(UpdateUpScrollSmooth(value));

    }
    public IEnumerator UpdateDownScrollSmooth(float value)
    {
        float curVal = scrollbar.value;
        float Diff = curVal - value;
        while (curVal - value > Mathf.Epsilon)
        {
            curVal -= Diff * Time.deltaTime * 2;
            scrollbar.value = curVal;
            yield return null;
        }
        scrollbar.value = value;

    }
    public IEnumerator UpdateUpScrollSmooth(float value)
    {
        float curVal = scrollbar.value;
        float Diff = curVal - value;
        while (value - curVal > Mathf.Epsilon)
        {
            curVal -= Diff * Time.deltaTime * 2;
            scrollbar.value = curVal;
            yield return null;
        }
        scrollbar.value = value;
    }
    public IEnumerator DoScreenUp()
    {
        while(scrollUI.transform.position.y <= 0)
        {
            Vector3 tmp = new Vector3(scrollUI.transform.position.x, scrollUI.transform.position.y + 0.1f, scrollUI.transform.position.z);
            scrollUI.transform.position = tmp;
            yield return new WaitForSeconds(0.1f);
        }
    }
    void SetSelection()
    {
        source.PlayOneShot(clip);
        checker = true;
        GameSceneManager.GSM.SetPlayerSelectObjectType((ObjectType)curSelection);
    }

    void LoadScene()
    {
        checker = false;    
        GameSceneManager.GSM.LoadSceneAsync("SceneThree");
        Debug.Log($"{gameObject.name} : playerselection {GameSceneManager.GSM.GetPlayerSelectObjectType()}");
        Debug.Log($"{gameObject.name} : stolenobject {GameSceneManager.GSM.GetAnswerObjectType()}");
        GameSceneManager.GSM.UnLoadSceneAsync("Scene2");
    }
}
