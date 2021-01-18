using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapA : MonoBehaviour
{
    private ayumu_sakai.Controller Ac;
    private GameObject effctPrefab;
    private bool ota;

    public bool OnTrapA
        {
            get { return ota; }
        }
    private int damage = 5;
    public int PassDamage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void Start()
    {
        Ac = FindObjectOfType<ayumu_sakai.Controller>();
        int ReceiveHp = Ac.PassHp;
        float ReceivePIC = Ac.PIC;
        Debug.Log(Ac.PassHp);
    }

    void OnCollisionEnter(Collision other)
    {

        //トラップに当たったらプレイヤーにダメージ
        ota = true;
        Debug.Log("プレイヤーにダメージ！");
        Debug.Log("Player HP" + Ac.PassHp);
        Debug.Log( Ac.PIC);
        if (Ac.PIC > 0)
        {
            if (Ac.PIC <= 0)
                {
                    ota = false;

                }            
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // Trapを画面から消す。
            // DestroyメソッドだとInvokeメソッドを使えない。
            this.gameObject.SetActive(false);
            // プレーヤーの動きを止める。
            //Ac = other.GetComponent<ayumu_sakai.Controller>();
            //Ac.enabled = false;

            // 2秒後にReleaseメソッドを呼び出す。
            Invoke("Release", 2.0f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        ota = false;
    }
    void Release()
    {
        //TM.enabled = true;
    }
}