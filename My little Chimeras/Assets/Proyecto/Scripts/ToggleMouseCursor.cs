//using UnityEngine;

//public class ToggleMouseCursor : MonoBehaviour
//{
//    [Header("Configuración")]
//    [SerializeField] private KeyCode toggleKey = KeyCode.Escape;

//    [Header("Estado (Solo lectura)")]
//    [SerializeField] private bool cursorVisible;

//    void Start()
//    {
//        SetCursor(false);
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(toggleKey))
//        {
//            SetCursor(!cursorVisible);
//        }
//    }

//    void SetCursor(bool visible)
//    {
//        cursorVisible = visible;

//        Cursor.visible = visible;
//        Cursor.lockState = visible
//            ? CursorLockMode.None
//            : CursorLockMode.Locked;
//    }
//}
