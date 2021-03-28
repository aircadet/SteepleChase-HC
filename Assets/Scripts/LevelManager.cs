using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject _ground, _ground1, _ground2, _obstacle, _finish;
    [SerializeField] Material _groundMat, _ground1Mat, _ground2Mat;
    [SerializeField] int _level, _zPosPlus = 10;

    int _obsNumber, _groundNumber, _currentObs, _currentGround;
    Vector3 _startPos, _startRot;

    private void Awake()
    {
        // Ground material renk değişim işlemleri //

        _groundMat.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        _ground1Mat.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        _ground2Mat.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        ////////////////////////////////////////////////////////////////

        _level = PlayerPrefs.GetInt("Level", 1);

        _obsNumber = ((_level  + 1) * 2) - 2;
        _groundNumber = (_level +2) *2 ;

        // Ground Yerleşim İşlemleri //

        _startPos = new Vector3(2.5f, 0, 0);

        while (_currentGround <= _groundNumber) 
        {
            GameObject ground = Instantiate(_ground, _startPos, Quaternion.Euler(_startRot));
            ground.transform.parent = GameObject.Find("Map").transform;

            _startPos += new Vector3(0, 0, _zPosPlus);

            _currentGround++;
        }

        Instantiate(_finish, _startPos - new Vector3(0, 0, 4), Quaternion.Euler(_startRot));

        // AI -1- için ground yerleşimleri //

        _startPos = new Vector3(-1.5f, 0, 0);
        _currentGround = 0;

        while (_currentGround <= _groundNumber)
        {            
            GameObject ground = Instantiate(_ground1, _startPos, Quaternion.Euler(_startRot));
            ground.transform.parent = GameObject.Find("Map").transform;

            _startPos += new Vector3(0, 0, _zPosPlus);

            _currentGround++;
        }
        Instantiate(_finish, _startPos - new Vector3(0, 0, 4), Quaternion.Euler(_startRot));

        // AI -2- için ground yerleşimleri //

        _startPos = new Vector3(-5.5f, 0, 0);
        _currentGround = 0;

        while (_currentGround <= _groundNumber)
        {
            GameObject ground = Instantiate(_ground2, _startPos, Quaternion.Euler(_startRot));
            ground.transform.parent = GameObject.Find("Map").transform;

            _startPos += new Vector3(0, 0, _zPosPlus);

            _currentGround++;
        }
        Instantiate(_finish, _startPos - new Vector3(0,0,4), Quaternion.Euler(_startRot));

        // Obstacle Yerleşim İşlemleri //

        _startPos = new Vector3(2.5f, 1, 0);
        
        while (_currentObs <= _obsNumber)
        {
            float zPlus = (10 * _groundNumber) / _obsNumber;
            GameObject obstacle = Instantiate(_obstacle, _startPos, Quaternion.Euler(_startRot));
            obstacle.transform.parent = GameObject.Find("Map").transform;

            _startPos += new Vector3(0, 0, zPlus);

            _currentObs++;
        }

        // Obstacle -1- Yerleşim İşlemleri //

        _startPos = new Vector3(-1.5f, 1, 0);
        _currentObs = 0;

        while (_currentObs <= _obsNumber)
        {
            float zPlus = (10 * _groundNumber) / _obsNumber;
            GameObject obstacle = Instantiate(_obstacle, _startPos, Quaternion.Euler(_startRot));
            obstacle.transform.parent = GameObject.Find("Map").transform;

            _startPos += new Vector3(0, 0, zPlus);

            _currentObs++;
        }

        // Obstacle -2- Yerleşim İşlemleri //

        _startPos = new Vector3(-5.5f, 1, 0);
        _currentObs = 0;

        while (_currentObs <= _obsNumber)
        {
            float zPlus = (10 * _groundNumber) / _obsNumber;
            GameObject obstacle = Instantiate(_obstacle, _startPos, Quaternion.Euler(_startRot));
            obstacle.transform.parent = GameObject.Find("Map").transform;

            _startPos += new Vector3(0, 0, zPlus);

            _currentObs++;
        }

    }


    public void Replay() 
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel() 
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        GameManager.playerState = GameManager.PlayerState.Playing;
    }
}
