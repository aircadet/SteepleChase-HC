using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{
    [SerializeField] Vector3 _playerSpeed, _playerJumpSpeed, _impactSpeed;
    [SerializeField] float _waitForFall, _stopDistance;

    GameManager gameManager;
    Rigidbody _rigidBody;
    Animator _anim;
    int samba = 0;

    bool _isGround;
    bool _isFall;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        _rigidBody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

    }
    private void Update()
    {

        int randomNum = Random.Range(0, 4);
        _anim.SetInteger("Jump", -1);

        // BAŞLANGIÇ ANİMASYONU İŞLEMLERİ // 
        if (GameManager.playerState == GameManager.PlayerState.Preparing)
        {
            if (samba == 0)
            {
                _anim.SetTrigger("Samba");
                samba++;
            }
        }


        if (GameManager.playerState == GameManager.PlayerState.Playing)
        {
            _anim.SetTrigger("Run");

            // HAREKET İŞLEMLERİ //
            if (!_isFall)
            {
                if (_isGround)
                {
                    _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, _playerSpeed.z);
                }
                else
                {
                    _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, _playerSpeed.z / 2f);
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            RaycastHitControl(_stopDistance);

        }
    }

    void RaycastHitControl(float _distance)
    {
        RaycastHit _hit;

        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.forward, out _hit, maxDistance: 10))
        {
            Debug.DrawRay(transform.position + new Vector3(0, 1, 0), Vector3.forward * 10);

            if (_hit.transform.tag == "Obstacle")
            {
                if (_hit.distance < _distance + 0.3f && _hit.distance > _distance - .3f)
                {
                    // ZIPLAMA KONTROL //

                    if (_isGround) 
                    {
                        _rigidBody.AddForce(_playerJumpSpeed, ForceMode.Impulse);
                        int randomNum = Random.Range(0, 4);
                        _anim.SetInteger("Jump", randomNum);
                    }
                   


                    ///////////////////////////////////////////////////////////////////////////////
                }

            }

        }
    }

    private IEnumerator WaitForFall(float time)
    {
        _isFall = true;

        yield return new WaitForSeconds(time);

        _isFall = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            _isGround = true;

        }

        // ÇARPTIKTAN SONRAKİ BEKLEME SÜRESİ //

        if (collision.transform.tag == "Obstacle")
        {
            _rigidBody.AddForce(_impactSpeed, ForceMode.Impulse);
            _anim.SetTrigger("Fall");
            StartCoroutine(WaitForFall(_waitForFall));
        }

        //OYUN BİTTİ Mİ KONTROLÜ //
        if (collision.transform.tag == "Finish")
        {
            // OYUN BİTTİ // 
            _rigidBody.isKinematic = true;

            _anim.SetTrigger("Victory");
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            _isGround = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            _isGround = false;
        }

    }
}
