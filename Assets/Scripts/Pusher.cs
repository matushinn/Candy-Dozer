using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    Vector3 startPositon;

    [SerializeField] float amlitude; //移動量パラメータ
    [SerializeField] float speed; //移動速度パラメータ


    // Start is called before the first frame update
    void Start()
    {
        startPositon = transform.localPosition; //初期値の保存
        
    }

    // Update is called once per frame
    void Update()
    {
        //変位を計算
        float z = amlitude * Mathf.Sin(Time.time * speed); //移動量の計算

        //zを変異させたポジションに再設定
        transform.localPosition = startPositon + new Vector3(0, 0, z);
        
    }
}
