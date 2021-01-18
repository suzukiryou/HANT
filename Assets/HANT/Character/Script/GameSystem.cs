using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    private ayumu_sakai.Controller CC;
    private PlayerStatus PS;
    private float HP_Management = 0f;
    GameObject HPSlider;

    private bool Check = true;//確認用変数　※後で削除 
    // Start is called before the first frame update
    void Start()
    {
        CC = FindObjectOfType<ayumu_sakai.Controller>();
        PS = FindObjectOfType<PlayerStatus>();
        HPSlider = GameObject.Find("PlayerHP");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSystem();
        EnemySystem();

    }

    void PlayerSystem()
    {
        HP_Management = CC.PassHp / 100f;
        //Debug.Log(HP_Management);
        HPSlider.GetComponent<Image>().fillAmount = HP_Management;

        if (CC.PassHp <= 0 && Check)
        {
            Debug.Log("ゲームオーバー");
            Check = false;
            SceneManager.LoadScene("Title");
        }

    }

    void EnemySystem()
    {
        //ここにエネミーシステム関連を記入

    }
}
