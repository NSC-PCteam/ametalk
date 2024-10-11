using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Mush : MonoBehaviour
{
    public float chaseRadius = 5f;       // プレイヤーを追いかけ始める半径
    public float moveSpeed = 3f;         // プレイヤーを追いかける速度
    public float stopDelay = 2f;         // 範囲外に出てから停止するまでの時間

    private bool isChasing = false;      // プレイヤーを追いかけているかどうか
    private Transform player;            // プレイヤーの位置
    private Animator anim;
    private Rigidbody2D rb;
    private bool playerInRange = false;  // プレイヤーが範囲内にいるか
    private float stopTimer = 0f;        // 範囲外での停止までのタイマー
    private bool isDead = false;         // Mushが踏まれて非表示になったか

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // プレイヤーの位置を取得
    }

    void Update()
    {
        if (isDead) return; // 踏まれて消えた後は処理を行わない

        // プレイヤーとの距離を計算
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRadius)
        {
            // プレイヤーが範囲内にいる場合、追いかける
            playerInRange = true;
            isChasing = true;
            anim.SetBool("Dash", true);  // Dashアニメーションをオンにする
            stopTimer = 0f;              // タイマーをリセット
            ChasePlayer();
        }
        else
        {
            // プレイヤーが範囲外に出た場合
            if (playerInRange)
            {
                stopTimer += Time.deltaTime;

                // 一定時間経過すると停止
                if (stopTimer >= stopDelay)
                {
                    playerInRange = false;
                    isChasing = false;
                    anim.SetBool("Dash", false);  // Dashアニメーションをオフにする
                    rb.velocity = Vector2.zero;   // 移動を停止
                }
            }
        }
    }

    private void ChasePlayer()
    {
        // x軸方向のみに追いかける
        float directionX = (player.position.x - transform.position.x);
        directionX = Mathf.Sign(directionX); // 方向を取得して -1 または 1 に

        rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);

        // プレイヤーの位置に応じてキャラクターの向きを変更
        if (directionX > 0)
        {
            // 右に向かっている場合
            transform.localScale = new Vector3(-1, 1, 1); // 右向き
        }
        else if (directionX < 0)
        {
            // 左に向かっている場合
            transform.localScale = new Vector3(1, 1, 1); // 左向き
        }
    }

    // 衝突判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーが上からMushを踏んだ場合
        if (collision.gameObject.CompareTag("Player") && collision.contacts[0].point.y > transform.position.y)
        {
            StartCoroutine(HandleHit()); // ヒット時の処理を呼び出し
        }
    }

    // 踏まれた際の処理を行うコルーチン
    private IEnumerator HandleHit()
    {
        isDead = true;                   // 死亡フラグを設定
        anim.SetTrigger("Hit");          // ヒットアニメーションを再生
        rb.velocity = Vector2.zero;      // 速度をゼロにして停止
        yield return new WaitForSeconds(0.5f);  // 少し待機してから消す
        gameObject.SetActive(false);     // Mushを非表示にする
    }
}
