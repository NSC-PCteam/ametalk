using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyuyakunoJump : MonoBehaviour
{
    private Animator anim = null;
   

    [SerializeField] GameObject groundCheckPos;
    [SerializeField] float fallMultiplier; //落ちるときの速度の乗数
    [SerializeField] float jumpMultiplier; //ジャンプして上がるときの速度の乗数
    [SerializeField] float jumpForce; //ジャンプの力
    [SerializeField] float jumpTime; //ジャンプしていられる時間
    [SerializeField] float checkRadius; //地面接地の取得範囲
    public LayerMask Ground;

    Vector2 vecGavity;
    Rigidbody2D rb;

    float jumpCounter;

    bool isJumping;
    bool doubleJump;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vecGavity = new Vector2(0, -Physics2D.gravity.y);
        anim = GetComponent<Animator>();
    }

    void Update()
    {


        JumpHandler();

        //キャラクターの落ちる速度を徐々に増加させる
        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGavity * fallMultiplier * Time.deltaTime;
        }

        //ボタンを押した時間によってジャンプの高さが変わるようにする
        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;

            if (jumpCounter > jumpTime)
            {
                isJumping = false;
            }

            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            //ジャンプの半分に達したら徐々に上がる速度を落とす
            if (t > 0.5)
            {
                currentJumpM = jumpMultiplier * (1 - t);
            }

            rb.velocity += vecGavity * currentJumpM * Time.deltaTime;
        }

    }

    //ジャンプ関数
    private void JumpHandler()
    {
        if (isGrounded() && (!Input.GetKey(KeyCode.Space) || !Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.UpArrow)))
        {
            doubleJump = false;
            
            anim.SetTrigger("Jump");
        }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
             {
                if (isGrounded() || doubleJump)
                {
                doubleJump = !doubleJump;

                isJumping = true;

                jumpCounter = 0;

                Jump();

                anim.SetTrigger("double jump");
                 }
              }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;

        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    //地面との接地をチェック1
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPos.transform.position, checkRadius, Ground);
    }
}