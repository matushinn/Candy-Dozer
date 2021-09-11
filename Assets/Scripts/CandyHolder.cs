/*Candyオブジェクトが無制限に放出できるので、これに制限を設け、一つ生成すると手持ちのストックを１消費するようにする。
 * 
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyHolder : MonoBehaviour
{
    const int DefaultCandyAmout = 30;

    const int RecoverySeconds = 10;


    //現在のキャンディのストック数
    int candy = DefaultCandyAmout;
    //ストック回復までの残り秒数
    int counter;

    public void ConsumeCandy()
    {
        if (candy > 0) candy--;
    }

    public int GetCandyAmount()
    {
        return candy;
    }

    public void AddCandy(int amount)
    {
        candy += amount;
    }

    private void OnGUI()
    {
        GUI.color = Color.black;

        //キャンディのストック数を表示
        string label = "Candy : " + candy;

        //回復している時だけ秒数を表示
        if (counter > 0) label = label + "(" + counter + "s)";

        GUI.Label(new Rect(0, 0, 100, 30), label);


    }

    private void Update()
    {
        //キャンディのストックがデフォルトより少なく、
        //回復カウントをしていない時にカウントをスタートさせる
        if (candy < DefaultCandyAmout && counter <= 0) //回復カウントスタートの判定
        {
            //コルーチンのスタート
            StartCoroutine(RecoveryCandy());
        }
    }

    IEnumerator RecoveryCandy()
    {
        counter = RecoverySeconds;

        //一秒ずつカウントを進める
        while (counter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            counter--;
        }

        candy++;
    }
}
