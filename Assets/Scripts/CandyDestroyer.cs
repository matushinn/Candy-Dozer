using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyHolder candyHolder;
    public int reward;

    //エフェクトプレファブパラメータ
    public GameObject effectPrefab;
    //エフェクトローテーションパラメータ
    public Vector3 effectRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Candy")
        {
            //指定数だけCandyのストックを増やす
            candyHolder.AddCandy(reward);

            //オブジェクトを削除
            Destroy(other.gameObject);

            //エフェクトプレファブの設定チェック
            if (effectPrefab != null)
            {
                //Candyのポジションにエフェクトを生成
                Instantiate(effectPrefab, other.transform.position, Quaternion.Euler(effectRotation));
            }
        }

    }
}
