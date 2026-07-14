using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMoverandSound : MonoBehaviour
{
    public float speed = 5f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime); //tạo chuyển động theo phương thẳng đứng hướng xuống với tốc độ trên theo thời gian
    }

    void OnTriggerEnter2D(Collider2D other) //Hàm này Unity tự động gọi khi có collider khác chạm vào trigger của viên ngọc. Tham số other chính là "người vừa đụng vào tôi".
    {
        if (other.gameObject.CompareTag("Player")) // thiết lập điều kiện kiểm tra thông tin của OTHER
                                                   // nếu, phương thức so sánh gameobject tag của other với nhãn "Player" là đúng thì
        {
            AudioSource audioSource = other.GetComponent<AudioSource>(); //Khai báo biến tên audioSource để gán thông tin và các hàm của audio component từ lệnh other.GetComponent<AudioSource>()
            audioSource.Play(); //play âm thanh từ component đó
            //  âm thanh phát từ Player chứ không phải từ ngọc. Đây là thiết kế thông minh — vì nếu loa gắn trên ngọc, Destroy(gameObject) sẽ xóa luôn cả loa trước khi kịp phát hết tiếng. Giống như chuông cửa: chuông gắn trong nhà (Player), không gắn trên ngón tay khách (gem) — khách đi rồi chuông vẫn kêu xong.
            Destroy(gameObject); //xóa gameObject đang gắn collider này. (Không phải là other)
                                 //nghĩa là xóa viên ngọc này, không phải xóa người chơi đang va chạm
            ScoreManager.AddScore(1);
        }
        else if (other.gameObject.CompareTag("Ground")) // còn không thì, nếu là mặt đất thì
        {
            Destroy(gameObject); //xóa gameObject đang gắn collider này. (Không phải là other)
                                 //nghĩa là xóa viên ngọc này, không phải xóa mặt đất
        }
    }
}
