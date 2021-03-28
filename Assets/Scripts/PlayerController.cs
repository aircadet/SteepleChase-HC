using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 _playerSpeed, _playerJumpSpeed, _impactSpeed;
    [SerializeField] float _stopDistance;
    [SerializeField] float _waitForFall;
    [SerializeField] GameObject[] _puzzles;

    Rigidbody _rigidBody;
    Animator _anim;

    bool _isGround;
    bool _isFall;
    int samba = 0;

    List<string> _jumps;

    private void Start()
    {   
        _rigidBody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        // JUMP SAYILARI EKLEME //
        _jumps.Add("Jump");
        _jumps.Add("Jump1");
        _jumps.Add("Jump2");
        _jumps.Add("Jump3");

    }
    private void Update()
    {
        // EĞER DÜŞERSE OYUNU BİTİR //

        if (transform.position.y < -10) 
        {
            FindObjectOfType<LevelManager>().Replay();
            GameManager.playerState = GameManager.PlayerState.Death;
        }
        
        // BAŞLANGIŞ ANİMASYONU İŞLEMLERİ //

        _anim.SetInteger("Jump", -1);

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
                    _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, _playerSpeed.z/2f);
                }
            }
            

            if (_isGround && Input.GetMouseButtonDown(0))
            {
                // ZIPLAMA VE ANİMASYONU //
                _rigidBody.AddForce(_playerJumpSpeed, ForceMode.Impulse);

                int randomNum = Random.Range(0, 4);
                _anim.SetInteger("Jump", randomNum);
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////


            // YAKLAŞMA KONTROL VE BULMACALARI GÖSTERME İŞLEMLERİ //

            //RaycastHitControl(_stopDistance);


        }
    }

    void RaycastHitControl(float _distance) 
    {
        RaycastHit _hit;

        if (Physics.Raycast(transform.position + new Vector3(0,1,0), Vector3.forward, out _hit, maxDistance: 10)) 
        {
            Debug.DrawRay(transform.position + new Vector3(0, 1, 0), Vector3.forward*10);

            if (_hit.transform.tag == "Obstacle" ) 
            {
                if (_hit.distance < _distance + 0.3f && _hit.distance > _distance - .3f) 
                {
                    // BULMACA GÖSTERME ALANI //
                    //_puzzles[0].SetActive(true);   
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
            GameManager.playerState = GameManager.PlayerState.Finish;

            _anim.SetBool("Victory", true);
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
