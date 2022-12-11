using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
   //Unity側で設定
    [Header("プレイヤー")] [SerializeField] private GameObject Machine; //元となるプレハブ

    [Header("残基")] [SerializeField, Range(0, 3)]
    private int residue = 3; //残基

    private Vector2 nowPos; //プレイヤーの座標
    private GameObject player; //プレイヤーのプレハブ

    private void Start()
    {
        //プレハブからプレイヤーを生成
        player = Instantiate(Machine) as GameObject;
        //初期座標
        nowPos = new Vector2(0, -3.0f);
        player.transform.position = nowPos;
    }

    private void Update()
    {
        
    }
}
