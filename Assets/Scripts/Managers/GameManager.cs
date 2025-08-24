using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        // Si ya hay un GameManager, destruir el anterior y quedarse con este
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("House");
    }

    public void GoToVictory()
    {
        SceneManager.LoadScene("Victory");
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
    public void MovePlayerToObject(Transform target)
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();

        if (player != null)
        {
            player.MoveToTarget(target.position, GetComponent<Collider2D>(), () =>
            {
                Debug.Log("Llegué al " + target.name);
                // Aquí abrís la UI del minijuego
            });
        }
    }
}