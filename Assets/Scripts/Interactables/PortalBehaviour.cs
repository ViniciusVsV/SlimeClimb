using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    private bool isTeleporting;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerCopy") && !isTeleporting)
        {
            GameObject targetPortal = FindOtherPortal();
            if (targetPortal != null)
            {
                StartCoroutine(TeleportPlayer(other, targetPortal));
            }
        }
    }

    private GameObject FindOtherPortal()
    {
        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
        foreach (GameObject portal in portals)
        {
            if (portal != gameObject)
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

        if(other.transform.parent != null)
            other.transform.parent.position = transform.transform.position;
        else
            other.transform.position = targetPortal.transform.position;

        Debug.Log("Player teleportado!");
        yield return new WaitForSeconds(0.5f);
        isTeleporting = false;
    }
}