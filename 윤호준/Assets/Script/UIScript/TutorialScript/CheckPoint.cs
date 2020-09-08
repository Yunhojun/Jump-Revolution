using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    public static int tutorialSceneNum;
    public GameObject cameraObject;
    public Vector3[] cameraPositions = new Vector3[6];
    public Vector2[] characterPositions = new Vector2[6];
    private string[] textBoxes = new string[6];
    public Text text;

    private void Start()
    {
        tutorialSceneNum = 0;
        CameraPositionSetting();
        CharacterPositionSetting();
        TextSetting();
        text.text = textBoxes[0];
        Debug.Log("튜토리얼 시작");
    }

    private void CameraPositionSetting()
    {
        cameraPositions[0] = new Vector3(9, 7.5f,-10);
        cameraPositions[1] = new Vector3(49, 7.5f,-10);
        cameraPositions[2] = new Vector3(89, 7.5f,-10);
        cameraPositions[3] = new Vector3(129, 7.5f,-10);
        cameraPositions[4] = new Vector3(169, 7.5f,-10);
        cameraPositions[5] = new Vector3(209, 7.5f,-10);
    }

    private void CharacterPositionSetting()
    {
        characterPositions[0] = new Vector2(-1, 1.5f);
        characterPositions[1] = new Vector2(39, 1.5f);
        characterPositions[2] = new Vector2(79, 1.5f);
        characterPositions[3] = new Vector2(119, 1.5f);
        characterPositions[4] = new Vector2(159, 1.5f);
        characterPositions[5] = new Vector2(199, 1.5f);
    }

    private void TextSetting()
    {
        textBoxes[0] = "text1";
        textBoxes[1] = "text2";
        textBoxes[2] = "text3";
        textBoxes[3] = "text4";
        textBoxes[4] = "text5";
        textBoxes[5] = "End";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            tutorialSceneNum += 1;
            Debug.Log(tutorialSceneNum);
            collision.gameObject.GetComponent<PlayerMove>().Stun(0.5f);
            collision.gameObject.GetComponent<Rigidbody2D>().position = characterPositions[tutorialSceneNum];
            cameraObject.transform.position = cameraPositions[tutorialSceneNum];
            text.text = textBoxes[tutorialSceneNum];
        }
    }
}
