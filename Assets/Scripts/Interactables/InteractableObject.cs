using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string objectName = "Lamp";

    private void OnMouseDown()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            player.MoveToTarget(transform.position, GetComponent<Collider2D>(), () =>
            {
                Debug.Log("Llegué al " + objectName);
                // Abrir UI del minijuego aquí
            });
        }
    }
}