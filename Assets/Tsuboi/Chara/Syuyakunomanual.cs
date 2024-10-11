using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Syuyakunomanual : MonoBehaviour
{
    //インスペクターで設定する
    public float speed;
    public float gravity;
    public float dashSpeed = 10f;   
    public Hanteikunnnomanual Jimen;

    //プライベート変数
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private bool isDashing = false;
    private bool isJimen = false;
   
    
    // Start is called before the first frame update
    void Start()
    {
        //コンポーネントのインスタンスを捕まえる
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //接地判定を作る
        isJimen = Jimen.IsJimen();


        //キー入力されたら行動する
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;

        //ダッシュ動作
        Dash();



        if (horizontalKey > 0)
       {
                transform.localScale = new Vector3(1, 1, 1);
                anim.SetBool("Run", true);
                xSpeed = speed;
       }
       else if (horizontalKey < 0)
       {
                transform.localScale = new Vector3(-1, 1, 1);
                anim.SetBool("Run", true);
                xSpeed = -speed;
       }
       else
       {
                anim.SetBool("Run", false);
                xSpeed = 0.0f;
       }

        

        rb.velocity = new Vector2(xSpeed, rb.velocity.y);
    }

    // ダッシュ処理
    private void Dash()
    {
        // スペースキーと右キーを同時押しで右ダッシュ
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.RightArrow))
        {
            isDashing = true;
            rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
        }
        // スペースキーと左キーを同時押しで左ダッシュ
        else if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftArrow))
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
