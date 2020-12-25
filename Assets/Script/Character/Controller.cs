using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ayumu_sakai
{
    public class Controller : MonoBehaviour
    {
        //GameObject status;
        private CharacterController controller;
        private Vector3 moveDirection;
        private PlayerStatus player_status;
        private EnemyAI enemyAI;
        public int emhp;
        //        
        private int playerhp;
        private int playerpow;

        //プレイヤー移動速度
        public float speed = 2.0f;
        private float run_speed;
        private float defalt_speed;

        //プレイヤージャンプ力
        public float jump;

        //地面かどうかの判定変数
        public bool Ground;
        private Rigidbody rb;

        

        // Start is called before the first frame update
        void Start()
        {
            //PrayerStatusからプレイヤーのステータス情報を取得（最大体力と攻撃力）
            player_status = FindObjectOfType<PlayerStatus>();
            {
                this.playerhp = player_status.PlayerMAX_HP; //体力
                this.playerpow  = player_status.PlayerPOW;   //攻撃力
            }

            enemyAI = FindObjectOfType<EnemyAI>();
            {
                //Enemyの体力情報の保存（変数名はEnemy_HP_Saveの略）
                int EHS = enemyAI.EMHP;
            }

            run_speed = speed + 1.0f;
            //rb = GetComponent<Rigidbody>();
            defalt_speed = speed;
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            CharaAttack();
            CharaJump();
            CharaMove();


        }

        //プレイヤー攻撃
        public void CharaAttack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //マウスをクリックしたら攻撃ログが出る　※テスト用

                enemyAI.EMHP = enemyAI.EMHP - playerpow;

                Debug.Log("Playerの攻撃"); //プレイヤーの攻撃通知
                Debug.Log("Enemy.HP:" + enemyAI.EMHP);  //敵残り体力表示
            }
        }

        //プレイヤージャンプ
        public void CharaJump()
        {
            if (Ground)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    moveDirection.y = 5;
                    Ground = false; //地面判定OFF
                }
            }
            moveDirection.y -= 10 * Time.deltaTime; //重力計算
            controller.Move(moveDirection * Time.deltaTime);
        }

        //プレイヤー移動
        public void CharaMove()
        {
            //上方向
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            //下方向
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
            }
            //右方向
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            //左方向
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }

            //キャラクターダッシュ
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = run_speed;
            }
            else
            {
                speed = defalt_speed;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            //ジャンプ可能かどうかを知らせる（Debug.Logは後に削除）
            //Debug.Log("ジャンプ可能");
            Ground = true;
        }
    }
}
