using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActivationControl : MonoBehaviour
{

    [SerializeField] Button _dailyQuest;
    [SerializeField] Button _craokQuest;
    [SerializeField] Button _disConnectButoon;
    [SerializeField] Button _connectButton;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("WAddress").GetComponent<TextMeshProUGUI>().text.Length < 20)
        {
            _dailyQuest.interactable = false;
            _craokQuest.interactable = false;
        }
        else
        {
            //  _connectButton.gameObject.SetActive(false);
            //  _disConnectButoon.gameObject.SetActive(true);
            _dailyQuest.interactable = true;
            _craokQuest.interactable = true;

        };
    }
}
