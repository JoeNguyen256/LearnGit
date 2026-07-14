using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Text Mesh Pro
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // tạo một biến số điểm bắt đầu bằng 0 để lưu giá trị điểm của người chơi
    public static int score = 0; // static nghĩa là gì: biến này thuộc về class ScoreManager, không thuộc về 1 instance cụ thể nào. Dù trong scene có bao nhiêu object gắn script này, tất cả đều share chung 1 giá trị, có thể gọi trực típ ở script khác
    public TextMeshProUGUI scoreText; // tạo một biến thuộc kiểu TextMeshProUGUI tên scoreText và có thể truy cập từ Unity Editor, kéo text từ canvas vào
    public float remainingTime; // lưu thời gian còn lại, Unity xử lý thời gian theo số thực nên dùng float
    public GameObject gameOverPanel; //panel hiện khi thua
    public TextMeshProUGUI gameOverText;
    private bool isGameOver = false; // nhưng 1 công tắc ban đầu false -> game end true
    // khai báo một hàm dành cho lớp ScoreManager nhằm tăng số điểm của người chơi
    public void RestartGame() // hàm để đc button gọi
    {
        Time.timeScale = 1f;      // Chạy lại thời gian vì torn gameover có Time.timeScale = 0f
        score = 0;                // Reset điểm
        isGameOver = false;       // Reset trạng thái game over

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Load lại chính Scene hiện tại.
    }
    public static void AddScore(int amount) //(int amount) nghĩa là hàm sẽ chỉ nhận giá trị là integer, và giá trị này sẽ được gán vào biến có tên amount
    {
        score += amount; //tăng điểm theo giá trị của amount được truyền vào tại sự kiện gọi AddScore
    }
    private void GameOver()
    {
        if (isGameOver) return; // biến này giúp gameover chạy đúng 1 lần khi end, ko sẽ chạy hàng trăm lần , nếu game đã GO thoát lun

        isGameOver = true; // thông báo game end
        gameOverText.text = "Game Over!\nScore: " + score; // hiện text \n là xún dòng
        gameOverPanel.SetActive(true); // hiện panel

        Time.timeScale = 0f; // Dừng game
    }
    void Start() // chạy 1 lần khi game start
    {
        Time.timeScale = 1f; // mở thời gian
        score = 0; // reset điểm
        isGameOver = false; // reset Gameover
        gameOverPanel.SetActive(false); // ẩn panel
        remainingTime = 20f; //thời gian còn lại tại thời điểm bắt đầu bằng 30s (thời lượng của trò chơi)
        StartCoroutine(CountdownTimer());
        // Điều này tạo một "luồng đếm giờ" chạy song song với game. Trong khi người chơi vẫn điều khiển nhân vật và nhặt gem, Coroutine cứ mỗi giây sẽ giảm remainingTime đi 1.
    }
    // Update is called once per frame
    void Update()
    {
        // cập nhật điểm hiển thị trên UI
        scoreText.text = "Score: " + score + " | Time: " + Mathf.CeilToInt(remainingTime); // -> Score: 10 hoặc Score: 5 ...
        //Mathf.CeilToInt luôn làm tròn số nguyên dương
        if (remainingTime <= 0 && !isGameOver) // ! đã ngược giá trị = !isGameOver = !false = true
        {
            GameOver();
        }
    }
    private IEnumerator CountdownTimer() // IEnumerator dùng tạo Coroutine (chạy, tạm dùng, chạy tiếp)
    {
        while (remainingTime > 0) // loop khi nào còn time vẫn chạy
        {
            yield return new WaitForSeconds(1f); // trả lại giá trị mới sau khi chờ 1s
            remainingTime--; // giảm thời gian
        }
    }
}
