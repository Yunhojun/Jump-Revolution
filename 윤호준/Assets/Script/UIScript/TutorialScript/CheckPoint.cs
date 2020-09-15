using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    public static int tutorialSceneNum;
    public GameObject cameraObject;
    public Vector3[] cameraPositions = new Vector3[7];
    public Vector2[] characterPositions = new Vector2[7];
    private string[] textBoxes = new string[7];
    private string[] monsterText = new string[7];
    public Text text;
    public Text title;

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
        cameraPositions[0] = new Vector3(-31, 7.5f, -10);
        cameraPositions[1] = new Vector3(9, 7.5f,-10);
        cameraPositions[2] = new Vector3(49, 7.5f,-10);
        cameraPositions[3] = new Vector3(89, 7.5f,-10);
        cameraPositions[4] = new Vector3(129, 7.5f,-10);
        cameraPositions[5] = new Vector3(169, 7.5f,-10);
        cameraPositions[6] = new Vector3(209, 7.5f,-10);
    }

    private void CharacterPositionSetting()
    {
        characterPositions[0] = new Vector2(-41, 1.5f);
        characterPositions[1] = new Vector2(-1, 1.5f);
        characterPositions[2] = new Vector2(39, 1.5f);
        characterPositions[3] = new Vector2(79, 1.5f);
        characterPositions[4] = new Vector2(119, 1.5f);
        characterPositions[5] = new Vector2(159, 1.5f);
        characterPositions[6] = new Vector2(199, 1.5f);
    }

    private void TextSetting()
    {
        textBoxes[0] = "←↑→↓ : Move\nSpace : Jump\nL-Ctrl : Sit"; //튜토리얼 첫번째 텍스트
        textBoxes[1] = "Rabbit: Double Jump \nSlime: Tripple Jump "; //첫번째맵 2번
        textBoxes[2] = "Ghost: High Jump \nSnail: Low Jump \nRed Pig: Speed Up \nGreen Pig: Speed Down";
        textBoxes[3] = "Duck: Dash to Press 'X'";
        textBoxes[4] = "Green Bullet: Knock Back \nWhite Bullet: Teleport \nOrange Bullet: Freeze ";
        textBoxes[5] = "Chicken: General Monster \nBat: General Flying Monster ";
        textBoxes[6] = "End";

        monsterText[0] = "Manipulation";
        monsterText[1] = "Jump Monster";
        monsterText[2] = "Buff/Debuff";
        monsterText[3] = "Dash Monster";
        monsterText[4] = "Gun Monster";
        monsterText[5] = "Monster";
        monsterText[6] = "End";
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
            title.text = monsterText[tutorialSceneNum];
            text.text = textBoxes[tutorialSceneNum];
            
        }
    }
}
