using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [Header("Jump Settings")] // để đẹp inspector
    public float jumpForce = 12f; // khai báo lực nhảy số thực 
    public int maxJump = 1; // khai báo số lần nhảy số nguyên dương

    [Header("Ground Check")]
    public Transform groundCheck; // 1 điểm kiểm tra dưới chân cha (KO PHẢI GROUND)
    public float checkRadius = 0.2f; // bán kính kiểm tra, nếu vòng trọn này chạm Ground -> isGrounded = true
    public LayerMask groundLayer; // layer cần kiểm tra, nếu object thuộc groundlayer thì mới tính là ground

    private Rigidbody2D rb; // Rigid nhân vật

    private int jumpCount; // đếm số lần nhảy
    private bool isGrounded; // điều kiện chạm đất đứng = true, bay = false
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // gán rb và Rigid của Cha, sau này chỉ cần gọi rb là hiểu Rigid
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround(); // mỗi frame đều phải kiểm tra Ground

        if (Input.GetKeyDown(KeyCode.Space)) // nếu nhấn space thì nhảy
        {
            Jump();
        }
    }
    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle( // hàm kiểm tra va chạm
            groundCheck.position, // tọa độ kiểm tra
            checkRadius, // bán kính kiểm tra
            groundLayer); // chỉ kiểm layer ground

        if (isGrounded) // nếu chạm đất reset số lần nhảy
        {
            jumpCount = 0;
        }
    }

    void Jump()
    {
        if (jumpCount < maxJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // reset vận tốc
            // Nếu đang rơi rất nhanh rồi bật tiếp, cú bật sẽ không đều. Đặt velocity.y = 0 giống như "xóa" vận tốc cũ trước khi tạo cú nhảy mới, nên cả lần nhảy thứ nhất và thứ hai đều có độ cao ổn định.

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Addforce đưa lực vào Rigid, Vector2.up lực hướng lên trên
            // ForceMode2D.Impulse có nghĩa đẩy mạnh ngay lập tức, nếu chỉ có Force thì lực sẽ tăng từ từ tùy trường hợp mà dùng

            jumpCount++;
        }
    }

    void OnDrawGizmosSelected() // Hàm này chỉ hoạt động trong cửa sổ Scene của Unity, không ảnh hưởng khi chạy game. Mục đích là vẽ vòng tròn để nhìn thấy vùng kiểm tra mặt đất.
    {
        if (groundCheck == null)
            return; // Nếu chưa kéo GroundCheck vào Inspector thì thoát ngay, tránh lỗi.

        Gizmos.color = Color.green; // Vòng tròn sẽ có màu xanh lá
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius); // hàm vẽ vòng tròn 
    }
}
