using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransfromMoveTo : MonoBehaviour
{
    [SerializeField] Vector3 _movePos;

    // Update is called once per frame
    void Update()
    {
        MovePos();
    }

    void MovePos()
    {
        this.transform.Translate(_movePos * Time.deltaTime);
    }

    internal void SetMovePos(Vector3 pos) => _movePos = pos;
}
