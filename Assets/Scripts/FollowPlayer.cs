using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector3 _offSet;
    [SerializeField] Transform _player;
    [SerializeField] bool _lookAt;

    private void Update()
    {

        transform.position = _player.position - _offSet;

        if (_lookAt) 
        {
            transform.LookAt(_player);
        }

        
    }
}
