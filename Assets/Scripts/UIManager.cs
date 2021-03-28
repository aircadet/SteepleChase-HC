using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject player ,_tapToPlay, _finish, _tryAgain;
    [SerializeField] Image _currentLevelIMG, _nextLevelIMG;
    [SerializeField] Text _currentLevelTXT, _nextLevelTXT;
    public Slider _slider;

    int level;
    
    

    private void Start()
    {
        level = PlayerPrefs.GetInt("Level");

        _currentLevelTXT.text = level.ToString();
        _nextLevelTXT.text = (level + 1).ToString();
    }

    void Update()
    {
        // Slider İşlemleri //

        float totalDistance = LevelManager._totalDistance;
        float playerZ = player.transform.position.z + 3.15f;
        totalDistance += 3.15f;

        _slider.value = playerZ / totalDistance;



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
