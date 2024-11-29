using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCheckpoint : MonoBehaviour{
    private static LoadCheckpoint Instance;

    public int index;
    private CheckpointBox[] checkpoints;
    private CheckpointController checkpointController;

    private void Awake() {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);    

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void Load(){
        checkpoints = FindObjectsOfType<CheckpointBox>();
        checkpoints = checkpoints.OrderBy(c => c.id).ToArray();

        CheckpointBox aux = checkpoints[index];

        Vector3 newPos = aux.savedPosition;
        Quaternion newRot = aux.savedRotation;
        CinemachineVirtualCamera newCam = aux.savedCamera;
        GameObject[] newObjcts = aux.savedObjects;

        checkpointController = FindFirstObjectByType<CheckpointController>();

        checkpointController.SaveStartPosition(newPos, newRot, newCam, newObjcts);
        checkpointController.StartPosition();

        //Fazer tratamento dos casos
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(scene.name == "Levels")
            Load();
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}