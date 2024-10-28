//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class SelectHandType : MonoBehaviour
//{
//    // Singleton instance
//    private static SelectHandType instance;

//    // Variables
//    public static string selectedHandType = "rightHand";

//    [SerializeField] private Button[] buttons;

//    // Awake()
//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }

//        Debug.Log(selectedHandType);
//    }

//    // TitleScene에 왔을 때 buttons를 가져오는 메서드
//    private void Start()
//    {
//        if (IsInTitleScene())
//        {
//            GameObject selectHandPanel = GameObject.Find("Canvas/Select Hand Type Panel");
//            if (selectHandPanel != null)
//            {
//                buttons = selectHandPanel.GetComponentsInChildren<Button>();
//                if (buttons != null && buttons.Length >= 2)
//                {
//                    Debug.Log("Buttons successfully found.");
//                }
//                else
//                {
//                    Debug.LogError("Buttons not found or insufficient buttons in Select Hand Type Panel.");
//                }
//            }
//            else
//            {
//                Debug.LogError("Select Hand Type Panel not found.");
//            }
//        }
//    }

//    // 왼손 키보드 선택
//    public void SelectLeftHand()
//    {
//        selectedHandType = "leftHand";
//        Debug.Log(selectedHandType);
//    }

//    // 오른손 키보드 선택
//    public void SelectRightHand()
//    {
//        selectedHandType = "rightHand";
//        Debug.Log(selectedHandType);
//    }

//    // 어느 씬에서든 손 타입 정보를 가져올 수 있게하는 함수
//    public static string GetSelectedHandType()
//    {
//        return selectedHandType;
//    }

//    // 현재 씬이 TitleScene인지 확인하는 함수
//    private bool IsInTitleScene()
//    {
//        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TitleScene";
//    }

//}
