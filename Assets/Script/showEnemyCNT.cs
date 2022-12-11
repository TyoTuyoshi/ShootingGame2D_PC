using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class showEnemyCNT : MonoBehaviour
{
    private int enemycbt = 0;
    void Start()
    {
        if (GameObject.Find("Enemy C"))
        {
            Debug.Log("いる!");
        }
        else
        {
            Debug.Log("いない...");
        }
    }
    void Update()
    {
        
    }
}
