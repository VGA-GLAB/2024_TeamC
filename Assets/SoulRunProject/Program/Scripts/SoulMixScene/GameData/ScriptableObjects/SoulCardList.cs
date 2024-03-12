using System.Collections.Generic;
using UnityEngine;


/*
 **ステータス**(すべてプレイヤーに加算される値)

**画像**

**名前**

**レベル**

**テキスト(ソウルの説明文)**

**ステータス**(すべてプレイヤーに加算される値)

- HP
- 攻撃力
- 防御力
- クールタイム
- 範囲
- 弾速
- 効果時間
- 弾数
- 貫通力
- 移動スピード(前と横両方)
- 成長速度(経験値ジェム取得時のEXP上昇率増加)
- 運(クリティカル、お金のドロップ数上昇、ソウルドロップ率UP)

**特性：**1ソウル1特性でステータスやインゲームで獲得する特定のスキルに効果を与える

- 効果
- 効果テキスト

**ソウル技：**インゲームで特定のタイミングで発動できる技

- クールタイム
- 技効果
- 効果テキスト
 */

namespace SoulRunProject.SoulMixScene
{
    [CreateAssetMenu(fileName = "SoulCardList", menuName = "SoulRunProject/SoulCardList")]
    public class SoulCardList : ScriptableObject
    {
        [SerializeField] public List<SoulCardData> soulCardList;
    }
}