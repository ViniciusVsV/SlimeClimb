using System.Collections;
using UnityEngine;

public class DivisionRecoil : BaseState
{
    [SerializeField] private Transform slimeTransform;
    [SerializeField] private float recoilSpeed;

    public override void StateEnter()
    {
        //Aplicar movimento para "baixo" em relação ao slime
        rb.linearVelocity = -1 * recoilSpeed * slimeTransform.up;

        //Chamar corrotina
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        while (rb.linearVelocity != Vector2.zero)
            yield return null;

        isComplete = true;

        controller.SetIdleState();
    }
}