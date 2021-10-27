using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScene : MonoBehaviour
{
    bool _sceneLoaded;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Character")
        {
            if (_sceneLoaded == false)
            {
                SceneLoad();
                _sceneLoaded = true;
            }
        }
    }

    public void SceneLoad()
    {
        GameSceneManager.GSM.SetGameOverType(GameOverType.¾À3½Ã°£ÃÊ°ú);
        GameSceneManager.GSM.LoadSceneAsync("5_GameOver");
        GameSceneManager.GSM.UnLoadSceneAsync("SceneThree");
        GameSceneManager.GSM.UnLoadSceneAsync("SceneThreeShowThieve");
    }
}
