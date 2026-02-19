using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwitchManager : MonoBehaviour
{
    public PlayerMovementRB[] players;
    public int activeIndex = 0;

    InputAction switchAction;

    void Awake()
    {
        switchAction = new InputAction("SwitchCharacter", InputActionType.Button);
        switchAction.AddBinding("<Keyboard>/tab"); // tecla para cambiar
    }

    void OnEnable()
    {
        switchAction.Enable();
    }

    void OnDisable()
    {
        switchAction.Disable();
    }

    void Start()
    {
        ActivatePlayer(activeIndex);
    }

    void Update()
    {
        if (switchAction.WasPressedThisFrame())
        {
            activeIndex = (activeIndex + 1) % players.Length;
            ActivatePlayer(activeIndex);
        }
    }

    void ActivatePlayer(int index)
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetActive(i == index);

            PlayerSelectionVisual visual =
                players[i].GetComponent<PlayerSelectionVisual>();

            if (visual != null)
                visual.SetSelected(i == index);
        }
        //for (int i = 0; i < players.Length; i++)
        //{
        //    players[i].SetActive(i == index);
        //}
    }

    public PlayerMovementRB GetActivePlayer()
    {
        return players[activeIndex];
    }
}
