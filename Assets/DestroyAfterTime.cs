using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    [SerializeField] float _time = 1.0f;

    void Start()
    {
        Destroy(this.gameObject, _time);
    }

}
