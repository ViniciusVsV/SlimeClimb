using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private bool isTeleporting;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                GameObject targetPortal = FindOtherPortal();
                if (targetPortal != null)
                {
                    StartCoroutine(TeleportPlayer(player, targetPortal));
                }
            }
        }
    }

    private GameObject FindOtherPortal()
    {
        GameObject[] portals = GameObject.FindGameObjectsWithTag("portal");
        foreach (GameObject portal in portals)
        {
            if (portal != this.gameObject)
            {
                return portal;
            }
        }
        return null;
    }

    private IEnumerator TeleportPlayer(PlayerController player, GameObject targetPortal)
    {
        isTeleporting = true;
        player.transform.position = targetPortal.transform.position;
        Debug.Log("Player teleportado!");
        yield return new WaitForSeconds(0.5f);
        isTeleporting = false;
    }
}

