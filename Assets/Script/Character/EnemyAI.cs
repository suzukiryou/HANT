using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private EnemyStatus enemy_status = null;

    GameObject status_manager;

    public GameObject target;
    private NavMeshAgent agent;
    private int enemyhp;
    private int enemypow;
    public float anemy_egion;
    private float nod;
    public bool check;

    public int EMHP
    {
        get { return this.enemyhp; }
        set { this.enemyhp = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //
        //enemyhp = enemy_status.EnemyMAX_HP ;
        //enemypow = enemy_status.EnemyPOW;
        enemy_status = FindObjectOfType<EnemyStatus>();
        {
            this.enemyhp = enemy_status.EnemyMAX_HP;
            this.enemypow = enemy_status.EnemyPOW;
            this.EMHP = enemyhp;
        }
        Debug.Log(EMHP);
        

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //enemyの座標を取得してpos1に代入
        Vector3 pos1 = GameObject.Find("Enemy").transform.position;
        //playerの座標を取得してpos2に代入
        Vector3 pos2 = GameObject.Find("Player").transform.position;

        //取得した座標をDistanceを使って計算＋表示
        Vector3.Distance(pos1, pos2);
        //x,y座標の計算（表示された値は”距離数”)        
        int nod = (int)(Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.z - pos2.z, 2)));

        //攻撃範囲内にプレイヤーが入ったかどうかチェック
        if (check)
        {
            
            if (nod <= anemy_egion)
            {
                Debug.Log("攻撃範囲内に侵入しました");
                check = false;
            }  
        }
        else if (nod > anemy_egion)
        {
            Debug.Log(nod);
            check = true;
        }
    }
}