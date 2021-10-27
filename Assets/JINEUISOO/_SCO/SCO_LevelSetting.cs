using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettingA", menuName = "ScriptableObject/Creat New LevelSetting")]
[System.Serializable]
public class SCO_LevelSetting : ScriptableObject
{
    [SerializeField] float _startTime;
    [SerializeField] float _moveSpeed;
    [Range(0f, 1f)]
    [SerializeField] float _separateBackAndGo;
    [SerializeField] Vector3 _movePlatformSpeed;











    public Vector3 PlatformSpeed => _movePlatformSpeed;
    public float StartTIme => _startTime;
    public float MoveSpeed => _moveSpeed;
    public float SeparateBackAndGo => _separateBackAndGo;
}
