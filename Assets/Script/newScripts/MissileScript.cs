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
        if (nowPos.y > 8.5f)
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