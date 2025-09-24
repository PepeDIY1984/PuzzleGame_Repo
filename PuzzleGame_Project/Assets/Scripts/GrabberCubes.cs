using UnityEngine;

public class GrabberCubes : MonoBehaviour
{

    private GameObject selectedObject;
    public GameObject piezaPosicionCorrecta;
    public Transform posicionCorrecta;

    private void Start()
    {
        piezaPosicionCorrecta.SetActive(false);
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Si no hay nada seleccionado
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("drag"))
                    {
                        return;
                    }
                    selectedObject = hit.collider.gameObject;
                    selectedObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
                    selectedObject.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.white;
                    selectedObject.transform.GetChild(2).GetComponent<Renderer>().material.color = Color.white;
                    Cursor.visible = false;
                }
            }
            //Si  hay algo seleccionado y se pulsa el boton izq del ratón
            else
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, 1.2f, worldPosition.z);

                //Si la pieza se situa dentro de las coordenadas comprendidas con el giro correcto
                if (selectedObject.transform.position.x <= -2 && selectedObject.transform.position.x >= -4)
                {
                    if (selectedObject.transform.position.z <= 3 && selectedObject.transform.position.z >= 2)
                    {
                        Debug.Log(selectedObject.transform.rotation.eulerAngles.y);
                        if (selectedObject.transform.rotation.eulerAngles.y <= 181 && selectedObject.transform.rotation.eulerAngles.y >= 179)
                        {
                            Debug.Log("Objeto posicionado correctamente");
                            selectedObject.transform.position = new Vector3(-3.277f, 1.33f, 2.491f);
                            selectedObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
                            selectedObject.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.green;
                            selectedObject.transform.GetChild(2).GetComponent<Renderer>().material.color = Color.green;
                            piezaPosicionCorrecta.SetActive(false);
                        }
                    }
                }
                selectedObject = null;
                Cursor.visible = true;
            }
        }

        //Si no hay nada seleccionado y se pulsa el boton derecho
        if (selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, 1.25f, worldPosition.z);

            //Si la distancia a la posicion correcta es menor que 3 con el giro correcto, aparece la pieza semitrasparente
            if (selectedObject.transform.rotation.eulerAngles.y <= 181 && selectedObject.transform.rotation.eulerAngles.y >= 179)
            {
                float distance = Vector3.Distance(selectedObject.transform.position, posicionCorrecta.position);
                if (distance < 3)
                {
                    piezaPosicionCorrecta.SetActive(true);
                }
                else
                {
                    piezaPosicionCorrecta.SetActive(false);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                    selectedObject.transform.rotation.eulerAngles.x,
                    selectedObject.transform.rotation.eulerAngles.y + 90f,
                    selectedObject.transform.rotation.eulerAngles.z));
            }
        }
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}

