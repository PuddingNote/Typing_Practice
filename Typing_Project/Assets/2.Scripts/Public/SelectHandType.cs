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

//    // TitleScene�� ���� �� buttons�� �������� �޼���
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

//    // �޼� Ű���� ����
//    public void SelectLeftHand()
//    {
//        selectedHandType = "leftHand";
//        Debug.Log(selectedHandType);
//    }

//    // ������ Ű���� ����
//    public void SelectRightHand()
//    {
//        selectedHandType = "rightHand";
//        Debug.Log(selectedHandType);
//    }

//    // ��� �������� �� Ÿ�� ������ ������ �� �ְ��ϴ� �Լ�
//    public static string GetSelectedHandType()
//    {
//        return selectedHandType;
//    }

//    // ���� ���� TitleScene���� Ȯ���ϴ� �Լ�
//    private bool IsInTitleScene()
//    {
//        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TitleScene";
//    }

//}
