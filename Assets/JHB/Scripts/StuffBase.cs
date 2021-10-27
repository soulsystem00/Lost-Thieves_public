using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stuff", menuName = "Stuff/Create new Stuff")]
public class StuffBase : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField] ObjectType type;

    public Sprite Sprite { get => sprite; }
    public ObjectType Type { get => type; }
}

[System.Serializable]
public enum ObjectType
{
    과제, 고양이, 가발, 타로카드
}
