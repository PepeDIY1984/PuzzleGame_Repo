using UnityEngine;

public class RotateByDistance : MonoBehaviour
{
    [Header("References")]
    public Transform target; // El otro GameObject

    [Header("Settings")]
    public Vector3 rotationAxis = Vector3.up; // Eje de rotaci�n
    public float rotationFactor = 10f; // Cu�nto influye la distancia en la rotaci�n
    private float rotationSpeed;

    void Update()
    {
        if (target == null) return;

        // Calcular la distancia entre este objeto y el target
        float distance = Vector3.Distance(transform.position, target.position);
        Debug.Log(distance);

        if (distance > 5)
        {
            rotationSpeed = 0;
        }
        else if (distance < 5)
        {
            rotationSpeed = 5 - distance;
        }

            // Calcular el �ngulo de rotaci�n proporcional a la distancia
            float angle = rotationSpeed * rotationFactor * Time.deltaTime;

        // Rotar el objeto
        transform.Rotate(rotationAxis, angle, Space.Self);
    }
}