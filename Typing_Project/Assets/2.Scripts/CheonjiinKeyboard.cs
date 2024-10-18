using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheonjiinKeyboard : MonoBehaviour
{
    // ���¸� �����ϱ� ���� ���� ����
    private bool lPressed = false;  // l�� ���ȴ��� ���θ� ����
    private bool kReplaced = false; // k�� ��ü�ߴ��� ���θ� ����
    private bool iReplaced = false; // i�� ��ü�ߴ��� ���θ� ����
    private bool mPressed = false;  // m�� ���ȴ��� ���θ� ����
    private bool nReplaced = false; // n�� ��ü�ߴ��� ���θ� ����
    private bool consonant = false; // ���� ���� ���θ� ����
    private int dotCount = 0;       // �� ī��Ʈ
    private bool jReplaced = false; // j�� ��ü�ߴ��� ���θ� ����
    private bool uReplaced = false; // u�� ��ü�ߴ��� ���θ� ����

    // ���¸� �ʱ�ȭ�ϴ� �Լ�
    private void ResetState()
    {
        lPressed = false;
        kReplaced = false;
        mPressed = false;
        nReplaced = false;
        iReplaced = false;
        jReplaced = false;
        uReplaced = false;
        consonant = false;
        dotCount = 0;
    }

    // Unity�� ������Ʈ �Լ����� Ű �Է��� ó��
    void Update()
    {
        // �� Ű�� ���� ó��
        if (Input.GetKeyDown(KeyCode.L))
        {
            ProcessIN();
        }
        else if (Input.GetKeyDown(KeyCode.A)) // ARAE_A
        {
            ProcessAraeA();
        }
        else if (Input.GetKeyDown(KeyCode.J)) // JI
        {
            ProcessJI();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetState();
        }
        // ���� �Է� ó��
        else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) ||
                 Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.A) ||
                 Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) ||
                 Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) ||
                 Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V))
        {
            ResetState();
            consonant = true; // ���� ���� true
        }
    }

    private void ProcessIN()
    {
        if (kReplaced)
        {
            // i�� ����� o�� ��� (��)
            BackspaceAndType('o');
            ResetState();
        }
        else if (iReplaced)
        {
            // i�� ����� O�� ��� (��)
            BackspaceAndType('O', true); // �빮�� O
            ResetState();
        }
        else if (consonant && dotCount == 1)
        {
            // j�� ��� (��)
            TypeChar('j');
            ResetState();
            jReplaced = true;
        }
        else if (consonant && dotCount == 2)
        {
            // u�� ��� (��)
            TypeChar('u');
            ResetState();
            uReplaced = true;
        }
        else if (jReplaced)
        {
            // p�� ��� (��)
            BackspaceAndType('p');
            ResetState();
        }
        else if (uReplaced)
        {
            // P�� ��� (��)
            BackspaceAndType('P', true); // �빮�� P
            ResetState();
        }
        else if (dotCount == 0)
        {
            // l�� ��� (��)
            TypeChar('l');
            lPressed = true;
        }
    }

    private void ProcessAraeA()
    {
        if (lPressed && !kReplaced)
        {
            // l�� ����� k�� ��� (��)
            BackspaceAndType('k');
            kReplaced = true;
            dotCount = 0;
        }
        else if (lPressed && kReplaced)
        {
            // k�� ����� i�� ��� (��)
            BackspaceAndType('i');
            ResetState();
            iReplaced = true;
        }
        else if (mPressed && !nReplaced)
        {
            // m�� ����� n�� ��� (��)
            BackspaceAndType('n');
            nReplaced = true;
            dotCount = 0;
        }
        else if (mPressed && nReplaced)
        {
            // n�� ����� b�� ��� (��)
            BackspaceAndType('b');
            ResetState();
        }
        else if (consonant)
        {
            dotCount++;
        }
        else if (dotCount == 3)
        {
            dotCount = 0;
            consonant = false;
        }
    }

    private void ProcessJI()
    {
        if (dotCount == 0)
        {
            // m�� ��� (��)
            TypeChar('m');
            mPressed = true;
        }
        else if (consonant && dotCount == 1)
        {
            // h�� ��� (��)
            TypeChar('h');
            ResetState();
        }
        else if (consonant && dotCount == 2)
        {
            // y�� ��� (��)
            TypeChar('y');
            ResetState();
        }
    }

    // �齺���̽� �� ���ڸ� ����ϴ� �Լ�
    private void BackspaceAndType(char c, bool shift = false)
    {
        // �齺���̽� (����Ƽ������ InputField �Ǵ� Text�� ���� �����ؾ� ��)
        // InputField.text = InputField.text.Substring(0, InputField.text.Length - 1);

        if (shift)
        {
            // Shift Ű �Է�
            // Shift�� ������ �빮�� �Է� ó��
        }

        // �ش� ���ڸ� ���
        // InputField.text += c;
    }

    // ���ڸ� ����ϴ� �Լ�
    private void TypeChar(char c)
    {
        // �ش� ���ڸ� ���
        // InputField.text += c;
    }
}
