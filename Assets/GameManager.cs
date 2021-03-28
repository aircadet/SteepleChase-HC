using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static PlayerState playerState;
    [SerializeField] GameObject _winParticle;
    public enum PlayerState 
    {
        Preparing,
        Playing,
        Death,
        Finish,
    }

    private void Start()
    {
        playerState = PlayerState.Preparing;    

    }

    private void Update()
    {
        if (playerState == PlayerState.Finish) 
        {
            _winParticle.SetActive(true);
        }
    }

}
