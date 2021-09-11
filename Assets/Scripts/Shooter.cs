using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    const int SphereCandyFrequency = 3;

    const int MaxShotPower = 5;
    const int RecoverySeconds = 3;

    int sampleCandyCount;
    int shotPower = MaxShotPower;

    AudioSource shotSound;

    //キャンディーパラメータの配列化
    public GameObject[] candyPrefabs;
    public GameObject[] candySquarePrefabs;
    public CandyHolder candyHolder;

    public float shotSpeed;
    public float shotTorque;

    public float baseWidth;

    private void Start()
    {
        shotSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //入力の検知
        if (Input.GetButtonDown("Fire1")) Shot();
    }

    //キャンディのプレファブからランダムに一つ選ぶ(prefabの抽選)
    GameObject SampleCandy()
    {
        GameObject prefab = null;

        //特定の回数に一回丸いキャンディを選択する
        if (sampleCandyCount % SphereCandyFrequency == 0)
        {
            int index = Random.Range(0, candyPrefabs.Length);
            prefab = candyPrefabs[index];
        }
        else
        {
            int index = Random.Range(0, candySquarePrefabs.Length);
            prefab = candySquarePrefabs[index];
        }

        sampleCandyCount++;

        return prefab;
    }

    //発射位置の計算
    Vector3 GetInstantiatePosition()
    {
        //画面のサイズとInputの割合からキャンディ生成のポジションを計算
        float x = baseWidth * (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }

    public void Shot()
    {
        //キャンディを生成できる条件外ならShotしない
        if (candyHolder.GetCandyAmount() <= 0) return;

        //ショットパワーのチェック
        if (shotPower <= 0) return;

        //プレファブからCandyオブジェクトを生成　
        GameObject candy = (GameObject)Instantiate(
            SampleCandy(),
            GetInstantiatePosition(),
            Quaternion.identity
            ); //オブジェクトの生成

        //生成したCandyオブジェクトの親をCandyHolderに設定する
        candy.transform.parent = candyHolder.transform; //親オブジェクトの設定

        //CandyオブジェクトのRigidBodyを取得し、力と回転を加える
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        candyRigidBody.AddForce(transform.forward * shotSpeed);
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));

        //candyのストックを消費
        candyHolder.ConsumeCandy();

        //shotpowerを消費
        ConsumePower();

        //サウンド再生
        shotSound.Play();
    }

    // ショットパワーの表示
    private void OnGUI()
    {
        GUI.color = Color.black;

        //Shotpowerの残数を＋の数で表示
        string label = "";
        for (int i = 0; i < shotPower; i++) label = label + "+";

        GUI.Label(new Rect(0, 15, 100, 30), label);
    }

    //ショットパワーの消費処理
    void ConsumePower()
    {
        //Shotpowerを消費すると同時に回復のカウントをスタート
        shotPower--;
        StartCoroutine(RecoveryPower());
    }

    //ショットパワーの回復コルーチン
    IEnumerator RecoveryPower()
    {
        //一定秒数まった後にShotpowerを回復
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;
    }
}
