using UnityEngine;

public class EnemyC : MonoBehaviour
{
    [SerializeField] private int speed = 50;//Y軸の移動スピード 
    private float sita = 0;//角度
    private float time = 0;//時間
    void Update()
    {
        this.gameObject.transform.position =
            new Vector2(Mathf.Sin(Mathf.PI * sita / 45) * 2 + transform.position.x,
                -time * speed + transform.position.y);
        time += Time.deltaTime;
        sita += 0.5f;
    }
}
