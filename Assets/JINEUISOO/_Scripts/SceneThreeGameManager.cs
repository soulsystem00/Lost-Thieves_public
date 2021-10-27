using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneThreeGameManager : MonoBehaviour
{
    [SerializeField] bool _gameInitialization;

    void Start()
    {
        if(_gameInitialization)
        GameSceneManager.GSM.LoadSceneAsync("SceneThreeShowThieve", setLoadSceneToCenter : false);
    }


}
