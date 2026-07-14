using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public float speed = 5.0f; // Biến này có thể được sử dụng để kiểm soát tốc độ di chuyển của đối tượng. Điều chỉnh giá trị này từ giao diện Unity Editor 
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Lấy đầu vào từ bàn phím theo chiều ngang VD: A,D,trái,phải
        float moveHorizontal = horizontalInput * speed * Time.deltaTime; // Tìm vị trí mới của đối tượng dựa trên đầu vào và tốc độ
        transform.position = new Vector2(transform.position.x + moveHorizontal, transform.position.y); // cập nhật vị trí mới của đối tượng chuẩn bị để đc render ở khung hình tiếp theo
        Vector3 pos = transform.position; // Lấy vị trí hiện tại của GameObject này.

        pos.x = Mathf.Clamp(pos.x, -9f, 9f); // Mathf thư viện toán học, hàm Clamp giới hạn giá trị (pos.x, -9f,9f) giới hạn x nằm trong khoảng này

        transform.position = pos; // hàm giữ player lại trong khoản đã giới hạn
    }

}
