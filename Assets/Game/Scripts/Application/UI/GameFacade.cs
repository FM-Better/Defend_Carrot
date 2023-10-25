using PureMVC.Patterns.Facade;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : Facade
{
    // Ϊ�˷���ʹ��Facade���� �Լ�дһ������
    public static GameFacade Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameFacade();
            }
            return instance as GameFacade;
        }
    }

    /// <summary>
    /// ��ʼ�����Ʋ�
    /// </summary>
    protected override void InitializeController()
    {
        base.InitializeController();
        // �������֪ͨ��
    }

    /// <summary>
    /// ����
    /// </summary>
    public void StartUp()
    {
        SendNotification(MVCNotification.START_UP);
    }
}
