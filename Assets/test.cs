using UnityEngine;
public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.Translate(-0.01f,0,0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.Translate(0,0.01f,0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.Translate(0,-0.01f,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Translate(0.01f,0,0);
        }
    }
}
