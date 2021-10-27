using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] RectTransform texts;
    [SerializeField] float speed;
    [SerializeField] GameObject end;
    [SerializeField] Image bg;
    Vector3 originalPos;
    // Start is called before the first frame update
    private void Awake()
    {
        originalPos = texts.localPosition;
    }
    void OnEnable()
    {
        bg.color = new Color(1f, 1f, 1f, 0f);
        texts.localPosition = originalPos;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        StartCoroutine(Fade());
        StartCoroutine(GoUP());
    }
    IEnumerator GoUP()
    {
        while(end.GetComponent<RectTransform>().position.y < 0)
        {
            var tmp = texts.transform.position;
            tmp.y = tmp.y + speed;
            texts.transform.position = tmp;
            /*float size = end.GetComponent<RectTransform>().position.y * -3000 / 10000;
            size = Mathf.Clamp(size, 0f, 1f);
            audioSource.volume = size;*/
            yield return new WaitForSeconds(speed);
        }
    }
    IEnumerator Fade()
    {
        while(bg.color.a <= 1)
        {
            var tmp = bg.color;
            tmp.a += 0.005f;
            bg.color = tmp;
            yield return new WaitForSeconds(0.005f);
        }
    }
    private void OnDisable()
    {
        audioSource.Stop();
    }
    private void Update()
    {
        if(!audioSource.isPlaying && Application.isFocused)
        {
            FindObjectOfType<Button>().onClick.Invoke();
        }

    }
}
