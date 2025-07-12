using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CheckpointBox : MonoBehaviour
{
    [SerializeField] public Vector3 savedPosition;
    [SerializeField] public Quaternion savedRotation;
    [SerializeField] public CinemachineVirtualCamera savedCamera;
    [SerializeField] public GameObject[] savedObjects;
    [SerializeField] private Animator flagAnimator;

    private CheckpointController checkpointController;
    private BoxCollider2D trigger;
    public int id;

    void Start()
    {
        flagAnimator.enabled = false;

        trigger = GetComponent<BoxCollider2D>();

        checkpointController = FindFirstObjectByType<CheckpointController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerCopy"))
        {
            checkpointController.SaveData(savedPosition, savedRotation, savedCamera, savedObjects);

            AudioController.Instance.PlayCheckpointSound();
            flagAnimator.enabled = true;

            PlayerPrefs.SetInt($"checkpoint{id}", 1);

            StartCoroutine(PauseMenuController.Instance.PopUpEstatisticas());

            trigger.enabled = false;
        }
    }
}