using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float moveSpeed = 2f;           // 移動速度
    public float LeftBoundary = 2f;        // 移動範囲の左端のX座標
    public float RightBoundary = 7f;       // 移動範囲の右端のX座標

    private bool movingRight = true;       // 初期状態では右に移動
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 左右の往復移動
        Move();
    }

    private void Move()
    {
        // 移動方向に応じてX軸の速度を設定
        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            // 右端に到達したら方向を反転して左に移動する
            if (transform.position.x >= RightBoundary)
            {
                movingRight = false;
                transform.localScale = new Vector3(1, 1, 1);  // キャラの向きを左に
            }
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            // 左端に到達したら方向を反転して右に移動する
            if (transform.position.x <= LeftBoundary)
            {
                movingRight = true;
                transform.localScale = new Vector3(-1, 1, 1);   // キャラの向きを右に
            }
        }
    }

    // プレイヤーとの衝突判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーが上からSlimeを踏んだ場合
        if (collision.gameObject.CompareTag("Player") && collision.contacts[0].point.y > transform.position.y)
        {
            StartCoroutine(HandleHit());  // 踏まれた場合の処理
        }
    }

    // 踏まれた際の処理を行うコルーチン
    private IEnumerator HandleHit()
    {
        anim.SetTrigger("Hit");           // 踏まれた際のアニメーションを再生
        rb.velocity = Vector2.zero;       // 速度をゼロにして停止
        yield return new WaitForSeconds(0.5f);  // 少し待機してから消す
        gameObject.SetActive(false);      // Slimeを非表示にする
    }
}