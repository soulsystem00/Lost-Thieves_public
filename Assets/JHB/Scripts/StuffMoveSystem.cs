using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StuffMoveSystem : MonoBehaviour
{
    [SerializeField] Transform rightUp;
    [SerializeField] Transform leftDown;
    [SerializeField] Transform rightUpLim;
    [SerializeField] Transform leftDownLim;
    [SerializeField] Text timeText;
    [SerializeField] Text startTimer;
    [SerializeField] List<Stuff> stuffs;
    [SerializeField] List<StuffMove> moves;
    [SerializeField] float LimTime;
    [SerializeField] int level;
    [SerializeField] Animator timer;

    [SerializeField] AudioSource _BGM;
    [SerializeField] AudioSource _TicTok;

    string message;
    public Action OnMoveOver;
    float curTime;
    List<Stuff> stuffsList = new List<Stuff>();
    List<Stuff> answerList = new List<Stuff>();

    bool waitTime = true;
    float waitTimeInt = 4f;

    public string Message { get => message; set => message = value; }

    // Start is called before the first frame update
    public void HandleStart()
    {
        SetLevel();
        timeText.text = "10";
        startTimer.gameObject.SetActive(true);
        for (int i = 0; i < level; i++)
        {
            int index = UnityEngine.Random.Range(0, stuffs.Count);
            Stuff tmp = new Stuff(level);
            tmp.Init(stuffs[index].Base);
            stuffsList.Add(tmp);
        }
        while (stuffsList.Count != 0)
        {
            int index = UnityEngine.Random.Range(0, moves.Count);
            if (moves[index].MyStuff == null)
            {
                var x = UnityEngine.Random.Range(leftDown.position.x, rightUp.position.x);
                var y = UnityEngine.Random.Range(leftDown.position.y, rightUp.position.y);
                moves[index].Init(x, y, stuffsList[0]);
                //Debug.Log(stuffsList[0].Base.Type);
                answerList.Add(stuffsList[0]);
                stuffsList.RemoveAt(0);
            }
        }
        curTime = LimTime;
        timer.speed = 0;
        
    }
    public void MakeAnswer()
    {
        timeText.text = "10";
        startTimer.gameObject.SetActive(true);
        answerList = answerList.OrderBy(a => Guid.NewGuid()).ToList();

        var answer = answerList[0];
        answerList.RemoveAt(0);

        SetAnswer(answer);

        while (answerList.Count != 0)
        {
            int index = UnityEngine.Random.Range(0, moves.Count);
            if (moves[index].MyStuff == null)
            {
                var x = UnityEngine.Random.Range(leftDown.position.x, rightUp.position.x);
                var y = UnityEngine.Random.Range(leftDown.position.y, rightUp.position.y);
                moves[index].Init(x, y, answerList[0]);
                
                answerList.RemoveAt(0);
            }
        }
        curTime = LimTime;
    }
    // Update is called once per frame
    public void HandleUpdate()
    {
        if(waitTime)
        {
            waitTimeInt -= Time.deltaTime;
            if(waitTimeInt > 3)
            {
                startTimer.text = message;
            }
            else
            {
                startTimer.text = Mathf.Ceil(waitTimeInt).ToString("N0");
            }
            if (waitTimeInt <= 0)
            {
                waitTime = false;
                timer.speed = 1;
                startTimer.gameObject.SetActive(false);
            }
                
        }
        else
        {
            curTime -= Time.deltaTime;
            timeText.text = curTime.ToString("N0");
            if (Convert.ToInt32(timeText.text) <= 3)
                timer.speed = 2;
            foreach (var move in moves)
            {
                if (move.MyStuff != null && curTime <= move.MyStuff.Time)
                {
                    move.Move();
                    LimitCheck(move);
                }
            }
            if (curTime <= 0f)
            {
                foreach (var move in moves)
                {
                    move.Init();
                }
                curTime = LimTime;
                waitTimeInt = 6f;
                waitTime = true;
                OnMoveOver();
                timer.speed = 0;
            }
        }
    }
    void LimitCheck(StuffMove stuffMove)
    {
        if (stuffMove.transform.position.x <= leftDownLim.position.x || stuffMove.transform.position.x >= rightUpLim.position.x || stuffMove.transform.position.y <= leftDownLim.position.y || stuffMove.transform.position.y >= rightUpLim.position.y)
        {
            stuffMove.ChangeDir();
        }
    }
    void SetLevel()
    {
        // Load from singleton class
        level += GameSceneManager.GSM.GetHowManyWeLooped();
    }
    void SetAnswer(Stuff s)
    {
        Debug.Log($"{gameObject.name} : answer {s.Base.Type}");
        GameSceneManager.GSM.SetAnswerObjectType(s.Base.Type);
        // Set Answer into singleton class
    }
    public void SetSound(AudioClip bgm, AudioClip tictok = null)
    {
        _BGM.clip = bgm;
        _BGM.Play();

        _TicTok.clip = tictok;
        if(tictok == null)
        {
            _TicTok.Stop();
        }
        else
            _TicTok.Play();
    }

}
