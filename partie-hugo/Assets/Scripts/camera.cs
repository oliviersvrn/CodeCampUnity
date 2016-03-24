using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

 private int i;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        Vector2 playerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
        Vector3 cameraPos = transform.position;
        cameraPos.x = playerPos.x;
        cameraPos.y = playerPos.y;
//        if (cameraPos.x > 10)
//            cameraPos.x = 10;
//        if (cameraPos.x < -10)
//            cameraPos.x = -10;
//        if (cameraPos.y > 2.5f)
//            cameraPos.y = 2.5f;
        if (cameraPos.y < -2)
            cameraPos.y = -2;
        transform.position = cameraPos;
    }
}
