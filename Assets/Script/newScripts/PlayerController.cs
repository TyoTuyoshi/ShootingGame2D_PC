using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    //Unity側で設定
    [Header("ミサイル")] [SerializeField] private GameObject myMissile; //発射するミサイル

    [Header("ライフポイント")] [SerializeField, Range(0, 100)]
    public int lifePoint = 30; //ライフポイントHP

    [Header("移動速度")] [SerializeField, Range(0, 1)]
    private float speed = 0.3f; //スピード

    [Header("当たり判定(半径)")] [SerializeField, Range(0, 1)]
    private float radius = 0.5f; //半径

    [Header("                 +------------------+(MaxPos)")]
    [Header("                  |------------------|")]
    [Header("                  |------------------|")]
    [Header("                  |------------------|")]
    [Header("(MinPos)+------------------+")]
    [Header("移動制限範囲1")]
    [SerializeField]
    //右上 
    private Vector2 minPos = new Vector2();

    [Header("移動制限範囲2")] [SerializeField] //左下
    private Vector2 maxPos = new Vector2();

    private int energyPoint = 0; //必殺チャージ
    private Vector2 nowPos; //プレイヤーの座標
    private GameObject player; //プレイヤーのプレハブ

    private float distance; //距離

    [Header("射撃間隔")] [SerializeField, Range(0.0f, 1.0f)]
    private float interval = 0.500f; //射撃間隔

    //HP表示
    [SerializeField] private TextMeshPro HP_text;
    
    private AudioSource _audioSource;
    
    private float time = 0f;
    private GameObject missile;
    private bool msilflag = false;

    private TranceformCollision trcollision;

    //---------------------------------------------------------------------//
    // Start()とは，Unity実行時に最初の1回に呼ばれる関数です．
    // パラメータの初期化とか準備に使います．
    //---------------------------------------------------------------------//
    private void Start()
    {
        player =  this.gameObject;// Instantiate(Machine) as GameObject;
        //初期座標
        nowPos = new Vector2(0, -3.0f);
        player.transform.position = nowPos;
        distance = speed * 0.1f; //距離を修正

        _audioSource = GetComponent<AudioSource>();
        trcollision = new TranceformCollision();
    }
    
    //---------------------------------------------------------------------//
    // Update()とは，Unity実行時に常に呼ばれる関数です．
    // 処理が終わるとまたUpdate()の最初から実行されます．
    // (´·ω·`)これなしでゲームは作れません!!
    //---------------------------------------------------------------------//
    private void Update()
    {
        HP_text.text = "HP:" + lifePoint;
        //射撃
        //ミサイルのプレハブを生成します．
        time += Time.deltaTime;
        if (time >= interval)
        {
            time = 0;
            for (int i = 0; i < 5; i++)
            {
                _audioSource.Play();
                missile = Instantiate(myMissile) as GameObject;
                missile.transform.position =
                    new Vector2(nowPos.x + Random.Range(-1.0f, 1.0f), nowPos.y + Random.Range(0.1f, 1.5f));
            }
        }

        //入力
        //---------------------------------------------------------------------//
        //  Input.GetKey()とは，キーボードを"押しているか"どうかが分かります．
        //  ()の中にはKeyCode.キーの名前を指定します．
        //  InputGetKeyDown()とは，キーボードを"押した"かどうかが分かります．
        //  InputGetKeyUp()とは，キーボードを押して"離した"ことが分かります．
        //  ちなみにInputではキーボードだけでなくマウスやゲームコントローラの入力も取得できます．
        //  ここでは，AWSDキーと十字キーでの移動をInput.GetKey()で実装します．
        //
        //  Unityリファレンス https://docs.unity3d.com/ja/2019.4/ScriptReference/Input.html
        //  分かりやすそうな記事 https://qiita.com/yando/items/c406690c9ad87ecfc8e5
        //---------------------------------------------------------------------//

        //左方向
        //和訳：もし、Aキーと左矢印キーが押されたなら...
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.position = new Vector2(nowPos.x -= distance, nowPos.y);
        }

        //右方向
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position = new Vector2(nowPos.x += distance, nowPos.y);
        }

        //上方向
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            player.transform.position = new Vector2(nowPos.x, nowPos.y += distance);
        }

        //下方向
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.position = new Vector2(nowPos.x, nowPos.y -= distance);
        }

        //当たり判定
        if (trcollision.CollisionEnterDetection(player, "Enemy", 0.5f, 0.5f))
        {
           //Debug.Log("touch");
            lifePoint--;
        }
        //死亡判定
        else if (lifePoint <= 0)
        {
            lifePoint = 0;
            //_audioSource.PlayOneShot(_audioSource.clip);
            //Destroy(enemy, 0.75f);
        }

        Debug.Log(lifePoint);
        //画面の範囲に収める．
        player.transform.position = PositionClamp(player.transform.position, minPos, maxPos);
    }

    private Vector2 PositionClamp(Vector2 _pos, Vector2 _min, Vector2 _max)
    {
        Vector2 clampPos = new Vector2();
        clampPos.x = Math.Clamp(_pos.x, _min.x, _max.x);
        clampPos.y = Math.Clamp(_pos.y, _min.y, _max.y);
        return clampPos;
    }
}
