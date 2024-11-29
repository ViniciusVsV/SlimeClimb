using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour{
    [SerializeField] GameObject playerPrefab;
    private Vector3 savedPosition;
    private Quaternion savedRotation;
    private CinemachineVirtualCamera savedCamera;
    private GameObject[] savedObjects;
    private CounterController counterController;

    void Start(){
        counterController = FindFirstObjectByType<CounterController>();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            Reset();
        }
    }

    public void SaveStartPosition(Vector3 newPosition, Quaternion newRotation, CinemachineVirtualCamera newCamera, GameObject[] newObjects){
        savedPosition = newPosition;
        savedRotation = newRotation;
        savedCamera = newCamera;
        savedObjects = newObjects;
    }

    public void SaveData(Vector3 newPosition, Quaternion newRotation, CinemachineVirtualCamera newCamera, GameObject[] newObjects){
        savedPosition = newPosition;
        savedRotation = newRotation;
        savedCamera = newCamera;
        savedObjects = newObjects;

        counterController.ResetSection();
    }

    public void StartPosition(){
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");
        foreach(GameObject camera in cameras){
            camera.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        }
        savedCamera.Priority = 10;

        foreach(GameObject savedObject in savedObjects){
            savedObject.SetActive(true);
        }

        Instantiate(playerPrefab, savedPosition, savedRotation);
    }

    void Reset(){
        GameObject player = GameObject.FindWithTag("Player");
        GameObject[] copies = GameObject.FindGameObjectsWithTag("PlayerCopy");

        Destroy(player);
        foreach(GameObject copy in copies)
            Destroy(copy);

        GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");
        foreach(GameObject camera in cameras){
            camera.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        }
        savedCamera.Priority = 10;

        foreach(GameObject savedObject in savedObjects){
            savedObject.SetActive(true);
        }

        Instantiate(playerPrefab, savedPosition, savedRotation);

        counterController.IncreaseResets();
    }
}