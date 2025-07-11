using UnityEngine;
using UnityEngine.InputSystem;

public class SlimeInputHandler : MonoBehaviour
{
    [SerializeField] private SlimeController[] slimeControllers;

    private Vector2 aux;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            aux = context.ReadValue<Vector2>();

            foreach (SlimeController controller in slimeControllers)
                controller.Jump(aux);
        }
    }

    public void Divide(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            aux = context.ReadValue<Vector2>();

            slimeControllers[0].Divide(aux);
        }
    }
}