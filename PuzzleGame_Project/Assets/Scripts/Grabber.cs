using UnityEngine;

public class Grabber : MonoBehaviour {

    private GameObject selectedObject;
    public Transform huecoPieza;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {

            if (selectedObject == null) {
                RaycastHit hit = CastRay();

                if(hit.collider != null) {
                    if (!hit.collider.CompareTag("drag")) {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            } else {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, 0f, worldPosition.z);

                if (selectedObject.transform.position.x <= -4.5 && selectedObject.transform.position.x >= -5.5)
                {
                    if (selectedObject.transform.position.z <= 5 && selectedObject.transform.position.z >= 3)
                    {
                        Debug.Log(selectedObject.transform.rotation.eulerAngles.y);
                        if (selectedObject.transform.rotation.eulerAngles.y <=91 && selectedObject.transform.rotation.eulerAngles.y >= 89)
                        {
                            Debug.Log("Objeto posicionado correctamente");
                            selectedObject.transform.position = huecoPieza.position;
                            selectedObject.GetComponent<Renderer>().material.color = Color.green;
                            selectedObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
                            huecoPieza.gameObject.SetActive(false);
                        }
                    }
                }


                selectedObject = null;
                Cursor.visible = true;

    

            }
        }

        if(selectedObject != null) {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, .25f, worldPosition.z);

            if (Input.GetMouseButtonDown(1)) {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                    selectedObject.transform.rotation.eulerAngles.x,
                    selectedObject.transform.rotation.eulerAngles.y + 90f,
                    selectedObject.transform.rotation.eulerAngles.z));
            }
        }
    }

    private RaycastHit CastRay() {
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
