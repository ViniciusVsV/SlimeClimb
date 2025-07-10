using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlimeInputs : MonoBehaviour
{
    private List<SlimeController> slimeControllers = new();

    private GameObject[] temp;
    private Vector2 aux;

    private void Start()
    {
        slimeControllers.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<SlimeController>());

        temp = GameObject.FindGameObjectsWithTag("PlayerCopy");
        foreach (GameObject obj in temp)
            slimeControllers.Add(obj.GetComponent<SlimeController>());
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            aux = context.ReadValue<Vector2>();

            foreach (SlimeController controller in slimeControllers)
            {
                if (!controller.isInactive)
                    controller.Jump(aux);
            }
        }
    }

    public void Divide(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            aux = context.ReadValue<Vector2>();

            if (!slimeControllers[0].isInactive)
                slimeControllers[0].Divide(aux);
        }
    }
}