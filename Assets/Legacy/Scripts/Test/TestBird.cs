using UnityEngine;

public class TestBird: MonoBehaviour
{
    public float xMove; // Y軸方向の移動速度
    public float zMove; // Y軸方向の移動範囲

    private float originalY;
    private float verticalDirection = 1f;

    void Start()
    {
        // 初期Y座標の保存
        originalY = transform.position.y;
    }

    void FixedUpdate()
    {
        // Z軸方向への移動
        transform.position += new Vector3(xMove, 0 , zMove);
    }
}
