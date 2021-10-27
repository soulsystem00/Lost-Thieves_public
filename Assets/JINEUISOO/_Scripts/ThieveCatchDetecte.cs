using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThieveCatchDetecte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Character")
        {
            GameSceneManager.GSM.LoadSceneAsync("4_Hit");
            GameSceneManager.GSM.UnLoadSceneAsync("SceneThreeShowThieve");
            GameSceneManager.GSM.UnLoadSceneAsync("SceneThree");
        }
    }
}
