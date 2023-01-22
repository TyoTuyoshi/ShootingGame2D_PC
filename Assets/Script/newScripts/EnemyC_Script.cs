using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
public class EnemyC_Script : MonoBehaviour
{
    //敵のコード
    //---------------------------------------------------------------------//
    //  敵を実装しています．
    //---------------------------------------------------------------------//
    private GameObject enemy;
    private Vector2 nowPos;
    private int ramble;

    private Animator _animator;
    private float time = 0;
    private float Theta = 0;
    
    //射撃関連 (追加)
    //------------------------------------------------------------------------------
    [Header("ミサイル")] [SerializeField] private GameObject myMissile; //発射する弾
    private GameObject missile;//弾
    [Header("射撃間隔")] [SerializeField, Range(0.0f, 10.0f)]
    private float interval = 0.500f; //射撃間隔
    [Header("ミサイル飛行速度")] [SerializeField, Range(0.0f, 1.0f)]
    private float msilspeed = 0.500f; //ミサイルの速度
    //------------------------------------------------------------------------------
    
    
    [SerializeField, Range(0, 10)] private int lifePoint = 10;
    [SerializeField] private GameObject damage; 
    private AudioSource _audioSource;
    private TranceformCollision trCollision;
    void Start()
    {
        ramble = Random.Range(0, 36);
        enemy = this.gameObject;
        Theta += ramble * 10;

        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        trCollision = new TranceformCollision();
        nowPos = enemy.transform.position;
    }
   
    void Update()
    {
        //射撃
        //ミサイルのプレハブを生成します．
        time += Time.deltaTime;
        if (time >= interval)
        {
            if (time >= interval)
            {
                time = 0;
                for (int i = 0; i < 18; i++)
                {
                    missile = Instantiate(myMissile) as GameObject;
                    missile.transform.position =
                        new Vector2(Mathf.Cos(3.14f / 180 * i * 20) + nowPos.x,
                            Mathf.Sin(3.14f / 180 * i * 20) + nowPos.y);
                }
            }
        }

        enemy.transform.position = new Vector2(MathF.Sin(Theta / 180 * Mathf.PI)*5 , nowPos.y);
        Theta += 0.5f;
        Theta %= 360;
        nowPos = enemy.transform.position;
        
        //ダメージ判定
        if (trCollision.CollisionEnterDetection(enemy, "Player", 0.5f, 0.5f))
        {
            _animator.SetTrigger("hitTrigger");
            GameObject damageText = Instantiate(damage) as GameObject;
            TextMeshPro text = damageText.GetComponent<TextMeshPro>();
            damageText.GetComponent<RectTransform>().position = new Vector3(nowPos.x, nowPos.y - 1.5f, -3);
            text.text = Random.Range(99, 150).ToString();
            Destroy(text, 0.2f);
            lifePoint--;
        }
        
        //死亡判定
        else if (lifePoint <= 0)
        {
            lifePoint = 0;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            //_audioSource.PlayOneShot(_audioSource.clip);
            Destroy(enemy, 0.75f);
        }
    }
}
