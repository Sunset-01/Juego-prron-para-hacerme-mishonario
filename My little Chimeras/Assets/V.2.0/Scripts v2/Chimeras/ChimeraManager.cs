//using UnityEditor.Rendering;
//using UnityEngine;

//public class ChimeraManager : MonoBehaviour
//{
//    public static ChimeraManager Instance;

//    public Camera cam;
//    public float focusSpeed = 5f;
//    public Vector3 offset = new Vector3(0, 2, -5);

//    private Chimera currentChimera;

//    void Awake()
//    {
//        Instance = this;
//    }

//    public void SelectChimera(Chimera chimera)
//    {
//        currentChimera = chimera;

//        // Mostrar popup
//        ChimeraUI.Instance.ShowPopup(chimera);
//    }

//    void Update()
//    {
//        if (currentChimera != null)
//        {
//            Vector3 targetPos = currentChimera.transform.position + offset;
//            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPos, Time.deltaTime * focusSpeed);

//            cam.transform.LookAt(currentChimera.transform);
//        }
//    }
//}
using UnityEngine;
using UnityEngine.EventSystems;


public class ChimeraManager : MonoBehaviour
{
    public static ChimeraManager Instance;

    public Camera cam;

    [Header("Follow")]
    public float followSpeed = 5f;
    public Vector3 offset = new Vector3(0, 2, -5);

    [Header("Orbit")]
    public float orbitSpeed = 100f;
    public float zoomSpeed = 5f;
    public float minDistance = 2f;
    public float maxDistance = 10f;

    private Chimera currentChimera;
    private float currentDistance;
    private float yaw = 0f;
    private float pitch = 20f;

    void Awake()
    {
        Instance = this;
        currentDistance = offset.magnitude;
    }

    public void SelectChimera(Chimera chimera)
    {
        currentChimera = chimera;

        // Reset rotación
        yaw = 0f;
        pitch = 20f;

        ChimeraUI.Instance.ShowPopup(chimera);
    }

    void Update()
    {
        if (currentChimera == null) return;

        HandleOrbit();

        Vector3 target = currentChimera.transform.position;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 direction = rotation * new Vector3(0, 0, -currentDistance);

        cam.transform.position = target + direction;
        cam.transform.LookAt(target);
    }

    void HandleOrbit()
    {
        // Orbitar con click derecho
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;

            pitch = Mathf.Clamp(pitch, 10f, 80f);
        }

        // Zoom con scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= scroll * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
        if (EventSystem.current.IsPointerOverGameObject())
            return;
    }

    // -------- DEJAR DE SEGUIR --------

    public void ClearSelection()
    {
        currentChimera = null;
        ChimeraUI.Instance.HidePopup();
    }
}