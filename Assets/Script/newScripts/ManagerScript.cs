using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ManagerScript : MonoBehaviour
{
   //Unity側で設定
    [Header("プレイヤー")] [SerializeField] private GameObject Machine; //元となるプレハブ

    //残基のアイコン
    [Header("残基アイコン")] [SerializeField] private List<Image> machineIcon = new List<Image>();
    [Header("残基")] [SerializeField, Range(0, 3)]
    private int residue = 3; //残基

    [SerializeField] private GameObject END;
    
    private Vector2 nowPos; //プレイヤーの座標
    private GameObject player; //プレイヤーのプレハブ

    [SerializeField] private TextMeshProUGUI lifeText; //ライフポイントGUI
    [SerializeField] private TextMeshProUGUI resdText; //残基GUI
    [SerializeField] private TextMeshProUGUI awakeTimer; //復活カウントダウン
    [SerializeField] private GameObject BackTitleButton; //タイトルバック

    [SerializeField] private GameObject EnemyB;
    [SerializeField] private GameObject EnemyC;

    private GameObject enemy;
    
    private void Start()
    {
        //プレハブからプレイヤーを生成
        player = Instantiate(Machine) as GameObject;
        END.SetActive(false);
        ClearText.SetActive(false);

        BackTitleButton.SetActive(false);
        //初期座標
        nowPos = new Vector2(0, -3.0f);
        player.transform.position = nowPos;
    }

    private const float interval = 3.0f;
    private const float enemy_interval = 3.0f;

    private float wait_t = 0;
    private float enemy_wait_t = 8.0f;
    
    
    //ゲーム耐久時間
    private float gameover_time = 60.0f;
    [SerializeField] private TextMeshProUGUI gameTimer;
    [SerializeField] private GameObject ClearText;

    private bool gameOverFlag = false;
    private bool gameClearFlag = false;


    //倒した敵の数
    private void Update()
    {
        gameover_time -= Time.deltaTime;
        gameTimer.text = "Time:" + ((int)gameover_time).ToString();

        if (gameover_time < 0 && !gameOverFlag)
        {
            gameover_time = 0;
            gameClearFlag = true;
            ClearText.SetActive(true);
            BackTitleButton.SetActive(true);
        }

        //敵生成
        enemy_wait_t += Time.deltaTime;
        if (enemy_wait_t > enemy_interval && !gameOverFlag && !gameClearFlag)
        {
            enemy_wait_t = 0;
            for (int i = 0; i < Random.Range(6, 12); i++)
            {
                enemy = Instantiate(EnemyB) as GameObject;
                enemy.transform.position = new Vector2(5 - i, i / 2 + 18.0f);
            }
            enemy = Instantiate(EnemyC) as GameObject;
            enemy.transform.position = new Vector2(7.5f, 7.5f);
        }
        
        //例外処理try-catch文
        try
        {
            //残基チェック
            if (residue == 0)
            {
                BackTitleButton.SetActive(true);
                END.SetActive(true);
                gameover_time = 0;
                gameOverFlag = true;
                return;
            }
            
            awakeTimer.text = "";
            int life = player.GetComponent<PlayerController>().lifePoint;
            //GUIにライフポイントを反映
            lifeText.text = "LIFE:" + life.ToString();
            resdText.text = "RESIDEUE:" + residue.ToString();
            if (life <= 0 && residue > 0 && !gameClearFlag)
            {
                Destroy(player);
                //アイコンを暗くする。
                machineIcon[residue - 1].color = new Color32(0x3f, 0x3f, 0x3f, 0xff);
                --residue;
            }
        }
        
        catch (Exception e)
        {
            //wait_tは待機用
            wait_t += Time.deltaTime;
            //復活カウントダウン
            awakeTimer.text = ((int)interval - (int)wait_t).ToString();
            
            if (wait_t > interval)
            {
                wait_t = 0;
                player = Instantiate(Machine) as GameObject;
            }
        }
    }

    public void BackTitle()
    {
        SceneManager.LoadScene("TITLE");
    }
}
