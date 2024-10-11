using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float moveSpeed = 2f;            // 移動速度
    public float waveAmplitude = 1f;        // 波の振幅
    public float waveFrequency = 1f;        // 波の周波数
    public float LeftBoundary = 2f;         // 左端のX座標
    public float RightBoundary = 7f;        // 右端のX座標
    private bool movingRight = true;        // 初期状態は右に移動
    private bool isVisible = true;          // 現在表示されているかどうか（アニメーションに基づく）
    private Vector3 facingDirection = new Vector3(-1, 1, 1);  // 左向きが正の向き

    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (isVisible)
        {
            // 波移動
            WaveMove();
        }
    }

  

    // 波移動の処理
    private void WaveMove()
    {
        // 波の高さを計算
        float waveOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

        if (movingRight)
        {
            // 右に移動中
            rb.velocity = new Vector2(moveSpeed, waveOffset);

            // RightBoundaryに到達したら左向きに反転
            if (transform.position.x >= RightBoundary)
            {
                movingRight = false;
                transform.localScale = facingDirection; // 左向きに反転
            }
        }
        else
        {
            // 左に移動中
            rb.velocity = new Vector2(-moveSpeed, waveOffset);

            // LeftBoundaryに到達したら右向きに反転
            if (transform.position.x <= LeftBoundary)
            {
                movingRight = true;
                transform.localScale = new Vector3(1, 1, 1); // 右向きに反転
            }
        }
    }

    // プレイヤーとの衝突処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーが上からGhostを踏んだ場合
        if (collision.gameObject.CompareTag("Player") && collision.contacts[0].point.y > transform.position.y)
        {
            StartCoroutine(HandleHit());  // ヒット時の処理を呼び出し
        }
    }

    // 踏まれた際の処理を行うコルーチン
    private IEnumerator HandleHit()
    {
        anim.SetTrigger("Hit");           // ヒットアニメーションを再生
        rb.velocity = Vector2.zero;       // 速度をゼロにして停止
        yield return new WaitForSeconds(0.5f);  // 少し待機してから消す
        gameObject.SetActive(false);      // Ghostを非表示にする
    }
}