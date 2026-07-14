using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f; // tốc độ, hiện ở Inspector, chỉnh được không cần sửa code
    private Animator animator; // tham chiếu tới bộ điều khiển animation
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Lấy component Animator gắn trên cùng GameObject này để lát nữa bật/tắt animation. 
    }
    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        bool isMoving = moveHorizontal != 0; // khai báo biến isMoving, !=0 khác 0
        animator.SetBool("isMoving", isMoving);

        if (isMoving) // Nếu nhân vật đang duy chuyển
        {
            transform.position += new Vector3(moveHorizontal * speed * Time.deltaTime, 0f, 0f); // transform.position += di chuyển trực tiếp, bỏ qua hệ vật lý. 0f, 0f biểu thị cho trục Y, Z vì Vector3 có ba trục (X, Y, Z)
        }
    }
}
