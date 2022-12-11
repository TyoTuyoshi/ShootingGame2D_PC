using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    private GameObject missile;
    [Header("ミサイル飛行速度")][SerializeField,Range(0.0f,1.0f)]
    private float msilspeed = 0.500f;//ミサイルの飛行速度

    private Vector2 nowPos;
    private float time = 0;
    private void Start()
    {
        missile = this.gameObject;
    }

    private void Update()
    {
        nowPos = missile.transform.position;
        //time += Time.deltaTime;
        //if (time > 1.3f)
        //{
        //    MissileDestory();
        //}
        if (nowPos.y > 10)
        {
            MissileDestory();
        }
        missile.transform.Translate(0, msilspeed * 0.1f, 0);
    }

    private void MissileDestory()
    {
        Destroy(missile);
        //missile.SetActive(false);
    }
}