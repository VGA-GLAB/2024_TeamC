# VS-DM-RunGame  
# 概要  
Steamでリリース予定のゲームのリポジトリです。 
制作まとめページ:https://www.notion.so/DM_VS_Run-5076dc46997c490997dca7574aba72cd  
シーン遷移図:https://drive.google.com/file/d/1370xhMTO-ZA736jT99h-NXHVCMqDi4J6/view?usp=sharing  
# 命名規則
# パスカルケース　HogeHoge

- クラス
- プロパティ
- Enum
- publicでstaticな変数
- const

```csharp
using UnityEngine;

namespace Hoge
{
    public class Hoge
    {
				private float _hoge = 0;
				public static float Hogehoge = 0;
        /// <summary> このインスタンスを保有するオブジェクトのタイプ </summary>
        public float Hoge => _hoge;

        /// <summary>
        /// ダメージを適用する
        /// </summary>
        /// <param name="damage"></param>
        public void HogeHoge(float damage)
        {
						//何かしらの処理
        }
    }

    /// <summary>
		/// Hoge
    /// </summary>
    public enum Hoge
    {
        None,
        Hoge,
        Foo,
    }

}
```

# アンダースコア＋キャメルケース　_hogeHoge

- privateなメンバ変数
- SerializeField

```csharp
using UnityEngine;

namespace Hoge
{
    public class Hoge
    {
				private float _hoge = 0;
    }
}
```

# キャメルケース　hoge

- ローカル変数

```csharp
using UnityEngine;

namespace Hoge
{
    public class Hoge
    {
				void Hoge()
				{
						private float foo = 0;
				}
    }
}
```

# コメント

クラス、メソッドには必須

変数は変数名で分からないものは書く。

# 名前空間

スクリプトを入れるファイルに準拠する

![teae.png](https://prod-files-secure.s3.us-west-2.amazonaws.com/3f8e5084-0064-4a3d-9c0a-419b5c08cfff/7c65b1ec-600c-4b37-8ee8-0d6717cf38e7/teae.png)

例えばAsset/Project/Program/Script/SceneAのCoreにファイルを作る場合、

名前空間をProject.Program.Script.SceneAにしてください。
