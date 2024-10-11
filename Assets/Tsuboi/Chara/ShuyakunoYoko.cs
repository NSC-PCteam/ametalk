using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuyakunoYoko : MonoBehaviour
{
    // 移動速度とダッシュ速度（Inspectorで調整可能）
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public Hanteikunnnomanual Jimen;

    // Rigidbody2Dの参照
    private Rigidbody2D rb;

    // 地上にいるかの判定（後ほどジャンプなどにも使える）
    private bool isGrounded = true;

    // ダッシュ中かどうかのフラグ
    private bool isDashing = false;

    private bool isJimen = false;

    private Animator anim = null;

    void Start()
    {
        //コンポーネントのインスタンスを捕まえる
        anim = GetComponent<Animator>();
        // Rigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //接地判定を作る
        isJimen = Jimen.IsJimen();

        // 通常の歩行処理
        Walk();

        // ダッシュ処理
        Dash();
    }

    // 通常の歩行処理
    private void Walk()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("Run", true);
            
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("Run", true);
            
        }
        else
        {
            anim.SetBool("Run", false);
           
        }
    }

    // ダッシュ処理
    private void Dash()
    {
        // スペースキーと右キーまたはDキーを同時押しで右ダッシュ
        if (Input.GetKey(KeyCode.Space) && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
        {
            isDashing = true;
            rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
        }
        // スペースキーと左キーまたはAキーを同時押しで左ダッシュ
        else if (Input.GetKey(KeyCode.Space) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
        {
            isDashing = true;
            rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
        }
        // ダッシュキーを離したら通常の移動に戻す
        else
        {
            isDashing = false;
        }
    }
}
