using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTopDownTest : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade do player
    public GameObject bulletPrefab; // Prefab da bullet
    public Transform firePoint; // Da onde a bala sai
    public float bulletSpeed = 10f; // Velocidade da bullet

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 mousePosition;

    public GameObject pauseMenuUI; // Referência para o menu de pausa no Canvas
    private bool isPaused = false; // Estado do jogo: pausado ou não

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        // Certifica-se de que o menu de pausa está desativado no início
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // Movimento do jogador
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Pega a posição do mouse
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Atirar com o botão do mouse
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        // Verifica se o jogador pressionou a tecla de pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    void FixedUpdate()
    {
        // Movimentação do jogador
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

        // Rotação do jogador baseada no mouse
        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Shoot()
    {
        // Instancia a bala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

        // Define a direção da bala
        Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;

        // Aplica força à bala
        rbBullet.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
    }

    // Pausar o jogo
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // Para o tempo no jogo
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true); // Ativa o menu de pausa
    }

    // Retomar o jogo
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // Retorna o tempo ao normal
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false); // Desativa o menu de pausa
    }

    // Reiniciar a cena
    public void RestartScene()
    {
        Time.timeScale = 1f; // Garante que o tempo esteja normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Carrega novamente a cena atual
    }

    // Sair do jogo (usado para builds)
    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
