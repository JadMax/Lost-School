using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    Camera cam;
    Vector3 camAngles;
    public static bool isGamePaused = false;
    public static bool isDoingOtherThings = false;
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject itemsBar;
    public Button continueGameBtn;
    public Button exitGameBtn;

	// Use this for initialization
	void Start () {
        cam = this.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if(isDoingOtherThings == false)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                //Game Paused
                isGamePaused = true;
            }
            /*if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                //Game Continue
                isGamePaused = false;
            }*/
        }
        
        if (isGamePaused == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenu.SetActive(false);
            itemsBar.SetActive(false);
            Time.timeScale = 1f;
            cameraControll();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;

            if (isDoingOtherThings == false)
            {
                pauseMenu.SetActive(true);
                itemsBar.SetActive(true);
            }

            continueGameBtn.onClick.AddListener(delegate ()
            {
                isGamePaused = false;
            });

            exitGameBtn.onClick.AddListener(delegate ()
            {
                Application.Quit();
            });
        }
        cam.transform.position = new Vector3(player.transform.position.x, 
            player.transform.position.y + 1, 
            player.transform.position.z);
    }
    void cameraControll()
    {
        float rotationX = Input.GetAxis("Mouse Y");
        float rotationY = Input.GetAxis("Mouse X");
        camAngles.x -= rotationX * 2;
        camAngles.y += rotationY * 2;
        cam.transform.eulerAngles = camAngles;
        player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, camAngles.y);
    }
}
