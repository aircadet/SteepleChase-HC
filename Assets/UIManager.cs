using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] GameObject _tapToPlay, _finish, _tryAgain;
    

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // TAP TO PLAY //
        if (GameManager.playerState == GameManager.PlayerState.Preparing)
        {
            _tapToPlay.SetActive(true);
        }
        else 
        {
            _tapToPlay.SetActive(false);
        }

        // FINISH //

        if (GameManager.playerState == GameManager.PlayerState.Finish)
        {
            _finish.SetActive(true);
        }
        else
        {
            _finish.SetActive(false);
        }



    }
}
