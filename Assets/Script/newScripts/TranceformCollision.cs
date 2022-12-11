using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TranceformCollision : MonoBehaviour
{
    private float radius = 0.5f;
    private GameObject[] enemys;// = GameObject.FindGameObjectsWithTag("探したいオブジェクトに設定されたタグ");
    //当たり判定
    //---------------------------------------------------------------------//
    //  当たり判定を実装しています．
    //  ちょっと難しいので今回は考えなくても大丈夫です．
    //  CollisionEnterDetection() 当たり判定の中に入った瞬間
    //  CollisionOutDetection() 当たり判定の外に出た瞬間
    //  CollisionStayDetection() 当たり判定の中にいる間
    //---------------------------------------------------------------------//
    private int cnt = 0;
    private int cnt2 = 0;
    private bool beforeFlag = false;
    private bool beforeFlag2 = false;
    
    //メインEnter関数
    public bool CollisionEnterDetection(GameObject _obj, string _tagName, float _radius1, float _radius2)
    {
        bool hit = false;
        GameObject[] _enemys = GameObject.FindGameObjectsWithTag(_tagName);
        for (int i = 0; i < _enemys.Length; i++)
        {
            bool flag = CollisionStayDetection(_obj, _enemys[i], _radius1, _radius2);
            if (flag)
            {
                hit = true;
            }
        }

        if (hit == beforeFlag2) return false;
        else
        {
            cnt2++;
            if (cnt2 % 2 != 0)
            {
                beforeFlag2 = hit;
                return true;
            }
            else
            {
                beforeFlag2 = hit;
                return false;
            }
        }
    }

    //ベースEnter関数
    public bool CollisionEnterDetection(GameObject _obj, GameObject _obj2, float _radius1, float _radius2)
    {
        bool flag = CollisionStayDetection(_obj, _obj2, _radius1, _radius2);
        if (flag == beforeFlag) return false;
        else
        {
            cnt++;
            if (cnt % 2 != 0)
            {
                beforeFlag = flag;
                return true;
            }
            else
            {
                beforeFlag = flag;
                return false;
            }
        }
    }

    //ベースOut関数
    public bool CollisionOutDetection(GameObject _obj, GameObject _obj2,float _radius1,float _radius2)
    {
        bool flag = CollisionStayDetection(_obj, _obj2, _radius1, _radius2);
        if (flag == beforeFlag) return false;
        else
        {
            cnt++;
            if (cnt % 2 == 0)
            {
                beforeFlag = flag;
                return true;
            }
            else
            {
                beforeFlag = flag;
                return false;
            }
        }
    }

    //ベースStay関数
    public bool CollisionStayDetection(GameObject _obj, GameObject _obj2,float _radius,float _radius2)
    {
        Vector2 objpos = _obj.transform.position;
        Vector2 objpos2 = _obj2.transform.position;
        float x_distance = objpos.x - objpos2.x;
        float y_distance = objpos.y - objpos2.y;
        float xy_distance = Mathf.Sqrt(x_distance * x_distance + y_distance * y_distance);
        if (xy_distance <= _radius + _radius2)
        {
            return true;
        }
        return false;
    }
}
