using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPuzzle : MonoBehaviour
{
    [SerializeField] GameObject[] _buttons;
    [SerializeField] int _nextButton = 1;

    public bool _isComplated = false;

    [SerializeField]GameObject player;

   


    private void OnEnable()
    {
        ShuffleButtons();

        player.GetComponent<PlayerController>().enabled = false;
        
        
    }

    private void OnDisable()
    {
        player.GetComponent<PlayerController>().enabled = true;
    }

    void ShuffleButtons() 
    {
        for (int i = 0; i < _buttons.Length; i++) 
        {
            _buttons[i].transform.SetSiblingIndex(Random.Range(0, 7));
        }
    }

    public void CheckButton(int buttonNumber) 
    {
        if (buttonNumber == _nextButton)
        {
            // BULMACA HENÜZ TAMAMLANMADIYSA 
            _buttons[buttonNumber - 1].GetComponent<Image>().color = Color.green;
            _nextButton++;

            if (_nextButton == 7)
            {
                // BULMACA ÇÖZÜLMÜŞ DEMEKTİR
                _buttons[buttonNumber - 1].GetComponent<Image>().color = Color.green;
                _nextButton = 1;
                _isComplated = true;

                StartCoroutine(WaitForCorrectAnswer(.2f));
            }


        }
        else 
        {
            // YANLIŞ CEVAP //
            _nextButton = 1;
            StartCoroutine(WaitForWrongAnswer(.2f));
        }
    }

    IEnumerator WaitForWrongAnswer(float time) 
    {
        // KIRMIZI YAKTIK //
        foreach (GameObject button in _buttons)
        {
            button.GetComponent<Image>().color = Color.red;
        }

        yield return new WaitForSeconds(time);

        // SÖNDÜRDÜK //
        foreach (GameObject button in _buttons)
        {
            button.GetComponent<Image>().color = Color.white;
        }

    }

    IEnumerator WaitForCorrectAnswer(float time)
    {
        // SÖNDÜRDÜK //
        foreach (GameObject button in _buttons)
        {
            button.GetComponent<Image>().color = Color.white;
        }

        new WaitForSeconds(time);

        // YEŞİL  YAKTIK //
        foreach (GameObject button in _buttons)
        {
            button.GetComponent<Image>().color = Color.green;
        }

        new WaitForSeconds(time);

        foreach (GameObject button in _buttons)
        {
            button.GetComponent<Image>().color = Color.white;
        }

        new WaitForSeconds(time);

        gameObject.SetActive(false);

        yield return new WaitForSeconds(time);

        _isComplated = false;

    }
}
