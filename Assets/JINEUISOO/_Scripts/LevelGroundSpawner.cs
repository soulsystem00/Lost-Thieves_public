using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject _basicPlatform;

    [SerializeField] GameObject[] _difficulty01PlatformPrefab;
    [SerializeField] GameObject[] _basicObjects;
    [SerializeField] Vector3 _movePlatformPos;
    
    [SerializeField] string _platformName;

    [SerializeField] Vector2 _cameraSize;

    Vector2 _colliderSize;

    void Start()
    {
        Initialization();
        //InstantiatePlatform();
    }

    void Initialization()
    {
        SetDifficulty();
        _colliderSize = this.transform.GetComponent<BoxCollider2D>().size;
        SetBasicObjectsMoveSpeed();

        void SetBasicObjectsMoveSpeed()
        {
            foreach(GameObject go in _basicObjects)
            {
                go.GetComponent<TransfromMoveTo>().SetMovePos(_movePlatformPos);
            }
        }
    }

    void SetDifficulty()
    {
        _movePlatformPos = GameSceneManager.GSM.GetLevelSetting(GameSceneManager.GSM.GetHowManyWeLooped()).PlatformSpeed;
    }

    void Update()
    {
        
    }
    // 만약 트리거에 엔터하면
    // 랜덤하게 하나의 프리팹을 결정한다.(난이도 수정 용이하도록)
    // 만들어지는 위치는 스케일.x / 2 + 만들 프리팹의 크기 / 2 이다.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == _platformName)
            InstantiatePlatform();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == _platformName)
            Destroy(collision.gameObject);
    }

    void InstantiatePlatform()
    {
        int tempIntPrefabNumber = GetRandomPrefabNumber(_difficulty01PlatformPrefab);

        Vector2 tempVector2PlatformColiderSize = GetBoxCollider2DSize(tempIntPrefabNumber);
        float tempFloatInstPosX = tempVector2PlatformColiderSize.x * 1.5f + _colliderSize.x * 0.5f;
        float tempFloatInstPosY = (_cameraSize.y * 0.5f - tempVector2PlatformColiderSize.y * 0.5f) * -1f;
        Vector2 tempVector2InstantiatePosition = new Vector2(tempFloatInstPosX, tempFloatInstPosY);
         
        InnerFInstantiatePlatform(tempIntPrefabNumber, tempVector2InstantiatePosition);
    }

    void InstantiatePlatform(Vector2 adjustPosition, GameObject platform)
    {
        Vector2 tempVector2PlatformColiderSize = platform.GetComponent<BoxCollider2D>().size;

        InnerFInstantiatePlatform(adjustPosition, platform);
    }

    int GetRandomPrefabNumber(GameObject[] difficultySet)
    {
        return Random.Range(0, difficultySet.Length);
    }

    Vector2 GetBoxCollider2DSize(int prefabNumber) => _difficulty01PlatformPrefab[prefabNumber].GetComponent<BoxCollider2D>().size;

    void InnerFInstantiatePlatform(int prefabNumber, Vector2 instantiatePos)
    {
        GameObject tempGOPlatfrom = Instantiate(_difficulty01PlatformPrefab[prefabNumber], instantiatePos, Quaternion.identity, this.transform);
        tempGOPlatfrom.name = _platformName;
        tempGOPlatfrom.GetComponent<TransfromMoveTo>().SetMovePos(_movePlatformPos);
    }

    void InnerFInstantiatePlatform(Vector2 instantiatePos, GameObject platform)
    {
        GameObject tempGOPlatfrom = Instantiate(platform, instantiatePos, Quaternion.identity, this.transform);
        tempGOPlatfrom.name = _platformName;
        tempGOPlatfrom.GetComponent<TransfromMoveTo>().SetMovePos(_movePlatformPos);
    }
}
