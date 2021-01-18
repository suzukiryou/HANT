using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ayumu_sakai
{
    public class Controller : MonoBehaviour
    {
        //GameObject status;
        CharacterController controller;
        AudioSource audioSource;

        //効果音の設定
        //効果音元のサイトURL：https://soundeffect-lab.info/sound/various/various3.html
        public AudioClip sound1;
        public AudioClip sound2;

        private Vector3 moveDirection;
        private PlayerStatus player_status;
        private EnemyAI enemyAI;
        private TrapA trap_A;

        private bool OnSE = true;
        private bool WaitSE = false;

        private bool Check;//確認用変数　※後で削除

        [SerializeField] private float Invincible;
        public float PIC    //読み取り専用変数　※変数名はPass_Invincible_Countの略
        {
            get { return Invincible; }
        }

        public float InvincibleCount = 200f;
        public int emhp;
        //        
        [SerializeField] private int playerhp;

        public int PassHp
        {
            get { return playerhp; }
            set { playerhp = value; }
        }

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

        //アニメーション
        private Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            //アニメーションコンポーネントを取得
            anim = GetComponent<Animator>();

            //AudioSourceコンポーネントを取得（効果音）
            audioSource = GetComponent<AudioSource>();

            //PrayerStatusからプレイヤーのステータス情報を取得（最大体力と攻撃力）
            player_status = FindObjectOfType<PlayerStatus>();
            {
                this.playerhp = player_status.PlayerMAX_HP; //体力
                this.playerpow = player_status.PlayerPOW;   //攻撃力
            }

            enemyAI = FindObjectOfType<EnemyAI>();
            {
                //Enemyの体力情報の保存（変数名はEnemy_HP_Saveの略）
                int EHS = enemyAI.EMHP;
            }

            trap_A = FindObjectOfType<TrapA>();
            {
                //トラップ接触情報の保存（変数名はOnTrapAの略）
                bool OTA = trap_A.OnTrapA;
            }


            run_speed = speed + 1.0f;
            //rb = GetComponent<Rigidbody>();
            defalt_speed = speed;
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            CharaAttack();  //攻撃
            CharaJump();    //動作（ジャンプ）
            CharaMove();    //動作（移動）
            DamageCalculation();    //ダメージ計算
            GameOver(); //ゲームオーバー
        }

        //プレイヤー攻撃
        public void CharaAttack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //マウスをクリックしたら攻撃ログが出る　※テスト用

                enemyAI.EMHP = enemyAI.EMHP - playerpow;

                //攻撃アニメーション
                anim.SetBool("Attack1",true);

                Debug.Log("Playerの攻撃"); //プレイヤーの攻撃通知
                Debug.Log("Enemy.HP:" + enemyAI.EMHP);  //敵残り体力表示

                //攻撃効果音再生
                audioSource.PlayOneShot(sound1);    //効果音１（通常攻撃：斬撃）
            }
            else
            {
                anim.SetBool("Attack1", false);
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
            if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.A))
            {
                anim.SetBool("Run", true);
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
            }
            //非操作時アニメーション停止
            else anim.SetBool("Run", false);

            //
            if (OnSE && !WaitSE && Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.A))
            {
                StartCoroutine("MoveSE");
                //audioSource.PlayOneShot(sound2);    //フィールド移動効果音
            }

            if (Input.GetKeyUp(KeyCode.W) | Input.GetKeyUp(KeyCode.S) | Input.GetKeyUp(KeyCode.D) | Input.GetKeyUp(KeyCode.A))
            {
                audioSource.Stop();
                WaitSE = true;
                OnSE = true;
            }
            else WaitSE = false;

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

        IEnumerator MoveSE()
        {
            //if (Input.GetKeyUp(KeyCode.W) | Input.GetKeyUp(KeyCode.S) | Input.GetKeyUp(KeyCode.D) | Input.GetKeyUp(KeyCode.A))
            //{
            //    audioSource.Stop();
            //}
            OnSE = false;
            //if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.A)){ }
            //else
            //{
            //    OnSE = true;
            //}
                audioSource.PlayOneShot(sound2);
            yield return new WaitForSeconds(8.176f);
            OnSE = true;
        }

        void DamageCalculation()
        {
            //
            //
            if (trap_A.OnTrapA && Invincible == 0)
            {
                playerhp -= trap_A.PassDamage;
                Invincible = InvincibleCount;   //無敵時間を設定　※調整可能
                Debug.Log("Player HP:" + playerhp);
            }
            else if (!Check)
            {
                Debug.Log("トラップAに接触していません");
                Check = true;
            }

            if (Invincible > 0)
            {
                --Invincible;
                if(Invincible <= 0)
                {
                    Invincible = 0;
                    
                }
            }
        }

        void GameOver()
        {
            
        }

        void OnCollisionEnter(Collision collision)
        {
            //ジャンプ可能かどうかを知らせる（Debug.Logは後に削除）
            //Debug.Log("ジャンプ可能");
            Ground = true;
        }
    }
}
