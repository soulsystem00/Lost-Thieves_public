using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSpawner : MonoBehaviour
{
    [SerializeField] Vector3 _backGroundMovePos;
    [SerializeField] Vector3 _adjustSpawnPos;

    [SerializeField] string _backGroundSetName;

    [SerializeField] GameObject _backGroundPrefab;

    [SerializeField] Vector3 _sizeOfPrefab;
    [SerializeField] int _instantiatingNumberOfBackGround = 5;

    private void Start()
    {
        InstantiateNewBackGround();
    }

    void Initialize()
    {
        for(int ia = 0; ia < _instantiatingNumberOfBackGround; ia++)
        {
            Vector3 tempVector3SizeChange;

            InstantiateNewBackGround(_sizeOfPrefab);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == _backGroundSetName)
        {
            InstantiateNewBackGround();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == _backGroundSetName)
        {
            Destroy(collision.gameObject);
        }
    }

    void InstantiateNewBackGround()
    {
        GameObject tempGO = Instantiate(_backGroundPrefab, _adjustSpawnPos, Quaternion.identity, this.transform);
        tempGO.name = _backGroundSetName;
        TransfromMoveTo tempTMTo = tempGO.GetComponent<TransfromMoveTo>();
        SetGroundMovePos(tempTMTo);
    }

    void InstantiateNewBackGround(Vector3 posAdjust)
    {
        GameObject tempGO = Instantiate(_backGroundPrefab, _adjustSpawnPos + posAdjust, Quaternion.identity, this.transform);
        tempGO.name = _backGroundSetName;
        TransfromMoveTo tempTMTo = tempGO.GetComponent<TransfromMoveTo>();
        SetGroundMovePos(tempTMTo);
    }

    void SetGroundMovePos(TransfromMoveTo tMTo)
    {
        tMTo.SetMovePos(_backGroundMovePos);
    }

}
