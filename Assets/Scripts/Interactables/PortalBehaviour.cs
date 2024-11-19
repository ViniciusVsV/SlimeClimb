using System.Collections;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    private bool isTeleporting;

    [SerializeField]
    public string portalName;

    [SerializeField]
    private string portalDirection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!string.IsNullOrEmpty(portalDirection)) // Verifica se a direção está definida
        {
            if ((other.CompareTag("Player") || other.CompareTag("PlayerCopy")) && !isTeleporting)
            {
                GameObject targetPortal = FindOtherPortal();
                if (targetPortal != null)
                {
                    StartCoroutine(TeleportPlayer(other, targetPortal));
                }
            }
        }
    }

    private GameObject FindOtherPortal()
    {
        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
        foreach (GameObject portal in portals)
        {
            PortalBehaviour portalBehaviour = portal.GetComponent<PortalBehaviour>();
            // Busca o portal com o nome que corresponde à direção
            if (portalBehaviour != null && portalBehaviour.portalName == this.portalDirection)
            {
                return portal;
            }
        }
        return null;
    }

    private IEnumerator TeleportPlayer(Collider2D other, GameObject targetPortal)
    {
        isTeleporting = true;
        AudioController.instance.PlayPortalSound();

        // Teleporta para a posição do portal de destino
        if (other.transform.parent != null)
            other.transform.parent.position = targetPortal.transform.position;
        else
            other.transform.position = targetPortal.transform.position;

        Debug.Log("Player teleportado!");
        yield return new WaitForSeconds(0.5f);
        isTeleporting = false;
    }
}
