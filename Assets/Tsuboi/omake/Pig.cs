using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pig : MonoBehaviour
{
    public float moveSpeed = 2f;  // 移動速度
    private bool movingLeft = true; // 初期状態では左に移動
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // 左向きが正の向きなので、左に進むときは (1, 1, 1)
        transform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        Move(); // 移動処理
    }

    private void Move()
    {
        // 左に進んでいる場合
        if (movingLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else
        {
            // 右に進んでいる場合
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

    // 壁にぶつかった時の処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Groundタグを持つ壁にぶつかった場合のみ処理
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 接触点がPigの左右にあるかどうかを確認
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (Mathf.Abs(contact.point.y - transform.position.y) < 0.3f) // 接触点がほぼ同じ高さであるか
                {
                    // 移動方向を反転
                    movingLeft = !movingLeft;

                    // 向きを反転
                    if (movingLeft)
                    {
                        transform.localScale = new Vector3(1, 1, 1); // 左向き
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1, 1, 1); // 右向き
                    }
                    break;
                }
            }
        }

        // プレイヤーが上からPigを踏んだ場合
        if (collision.gameObject.CompareTag("Player") && collision.contacts[0].point.y > transform.position.y)
        {
            StartCoroutine(HandleHit()); // ヒット時の処理を呼び出し
        }
    }

    // 踏まれた際の処理を行うコルーチン
    private IEnumerator HandleHit()
    {
        anim.SetTrigger("Hit");           // ヒットアニメーションを再生
        rb.velocity = Vector2.zero;       // 速度をゼロにして停止
        yield return new WaitForSeconds(0.5f);  // 少し待機してから消す
        gameObject.SetActive(false);      // Pigを非表示にする
    }
}
