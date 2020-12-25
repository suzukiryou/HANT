using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapA : MonoBehaviour
{
    private TankMovement TM;
    public GameObject effctPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Trapを画面から消す。
            // DestroyメソッドだとInvokeメソッドを使えない。
            this.gameObject.SetActive(false);
            // プレーヤーの動きを止める。
            TM = other.GetComponent<TankMovement>();
            TM.enabled = false;

            // 2秒後にReleaseメソッドを呼び出す。
            Invoke("Release", 2.0f);
        }
    }
    void Release()
    {
        TM.enabled = true;
    }
}