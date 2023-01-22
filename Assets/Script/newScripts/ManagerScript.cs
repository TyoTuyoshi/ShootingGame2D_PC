using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

    [SerializeField] private TextMeshProUGUI PointText; //ライフポイントGUI

    [SerializeField] private GameObject Enemy;
    private GameObject enemy;
    
    private void Start()
    {
        //プレハブからプレイヤーを生成
        player = Instantiate(Machine) as GameObject;
        END.SetActive(false);
        //初期座標
        nowPos = new Vector2(0, -3.0f);
        player.transform.position = nowPos;
    }

    private const float interval = 3.0f;
    private const float enemy_interval = 3.0f;

    private float wait_t = 0;
    private float enemy_wait_t = 8.0f;

    public int point = 0;

    //倒した敵の数
    public int Point
    {
        set { point = value;}
        get { return point; }
    }
    
    private void Update()
    {
        //PointText.text = "Point:" + point;
        //敵生成
        enemy_wait_t += Time.deltaTime;
        if (enemy_wait_t > enemy_interval)
        {
            enemy_wait_t = 0;
            for (int i = 0; i < Random.Range(6, 12); i++)
            {
                enemy = Instantiate(Enemy) as GameObject;
                enemy.transform.position = new Vector2(5 - i, i / 2 + 18.0f);
            }
        }
        
        //例外処理try-catch文
        try
        {
            //残基チェック
            if (residue == 0)
            {
                Debug.Log("END");
                END.SetActive(true);
                return;
            }
            
            awakeTimer.text = "";
            int life = player.GetComponent<PlayerController>().lifePoint;
            //GUIにライフポイントを反映
            lifeText.text = "LIFE:" + life.ToString();
            resdText.text = "RESIDEUE:" + residue.ToString();
            if (life <= 0 && residue >= 0)
            {
                Destroy(player);
                //アイコンを暗くする。
                machineIcon[residue - 1].color = new Color32(0x3f, 0x3f, 0x3f, 0xff);
                residue--;
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
}
