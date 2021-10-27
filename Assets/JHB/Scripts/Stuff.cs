using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stuff
{
    [SerializeField] StuffBase _Base;
    float speed;
    float scale;
    float time;
    float rotate;
    public StuffBase Base { get => _Base; }
    public float Speed { get => speed; }
    public float Scale { get => scale; }
    public float Time { get => time; }
    public float Rotate { get => rotate; }

    public void Init(StuffBase Base)
    {
        this._Base = Base;
    }

    public Stuff(int level)
    {
        speed = Random.Range(5, 5 + level);
        scale = Random.Range(0.5f, 1.5f);
        time = Random.Range(7f, 10f);
        rotate = Random.Range(0f, 360f);
    }
}
