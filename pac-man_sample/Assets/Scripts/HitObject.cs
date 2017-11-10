using UnityEngine;
using System.Collections;

/*
 *	Collider を使ってヒットするものの基底クラス
 *	Maruchu
 */
public class HitObject : MonoBehaviour
{

    //当たり判定のグループ
    public enum HitGroup
    {
        Player1     //プレーヤー1のグループ
        , Player2       //プレーヤー2のグループ
        , Other         //それ以外(壁など)
    }

    public HitGroup m_hitGroup = HitGroup.Player1;      //自分の当たり判定グループ

    /*
     *	Collider がヒットして大丈夫か確認
     */
    protected bool IsHitOK(GameObject hittedObject)
    {

        //相手が同じスクリプトを持っているか確認
        HitObject hit = hittedObject.GetComponent<HitObject>();
        //持ってなければ当たらなくていい
        if (null == hit)
        {
            return false;
        }

        //同じグループの者同士は判定を無視する
        if (m_hitGroup == hit.m_hitGroup)
        {
            return false;
        }

        //違うグループのものにあたった
        return true;
    }
}
