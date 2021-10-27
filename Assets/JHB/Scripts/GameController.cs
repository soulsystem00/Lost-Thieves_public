using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Day1, Night1, Day2, Select }
public class GameController : MonoBehaviour
{
    [SerializeField] StuffMoveSystem stuffMoveSystem;
    [SerializeField] MyAnimation myAnimation;
    [SerializeField] SelectionSystem selectSystem;

    [SerializeField] AudioClip BGM1;
    [SerializeField] AudioClip TicTok1;
    [SerializeField] AudioClip BGM2;
    [SerializeField] AudioClip TicTok2;
    [SerializeField] AudioClip BGM3;
    [SerializeField] AudioClip BGM4;
    [SerializeField] AudioClip SFX;
    State state = State.Day1;
    // Start is called before the first frame update
    void Start()
    {
        stuffMoveSystem.SetSound(BGM1, TicTok1);
        stuffMoveSystem.HandleStart();
        stuffMoveSystem.OnMoveOver += MoveOver;
        myAnimation.OnAnimOver += AnimOver;
        selectSystem.OnSelectOver += SelectOver;
        stuffMoveSystem.Message = "사물을 기억하세요!";
    }
    // Update is called once per frame
    void Update()
    {
        if (state == State.Day1 || state == State.Day2)
            stuffMoveSystem.HandleUpdate();
        else if (state == State.Night1)
            myAnimation.HandleUpdate();
        else if (state == State.Select)
            selectSystem.HandleUpdate();
    }
    void MoveOver()
    {
        if(state == State.Day1)
        {
            state = State.Night1;
            stuffMoveSystem.gameObject.SetActive(false);
            myAnimation.gameObject.SetActive(true);
            myAnimation.SetSound(BGM3);
        }
        else if(state == State.Day2)
        {
            state = State.Select;
            selectSystem.gameObject.SetActive(true);
            stuffMoveSystem.SetSound(BGM4);
            StartCoroutine(selectSystem.DoScreenUp());
        }
    }
    void AnimOver()
    {
        if(state == State.Night1)
        {
            state = State.Day2;
            myAnimation.gameObject.SetActive(false);
            stuffMoveSystem.gameObject.SetActive(true);
            stuffMoveSystem.MakeAnswer();
            stuffMoveSystem.Message = "없어진 물건을 찾아보세요!";
            stuffMoveSystem.SetSound(BGM2, TicTok2);
        }
    }
    void SelectOver()
    {
        // TODO : goto battle scene
    }
}
