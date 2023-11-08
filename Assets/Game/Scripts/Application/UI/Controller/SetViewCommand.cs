using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetViewCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        string panelName = notification.Body.ToString();

        switch (panelName)
        {
            case Consts.V_Start:
                //  û�и�����Mediator ��ע��
                if (!Facade.HasMediator(StartViewMediator.NAME))
                {
                    Facade.RegisterMediator(new StartViewMediator());
                }
                // �õ�������Mediator
                StartViewMediator startViewMediator = Facade.RetrieveMediator(StartViewMediator.NAME) as StartViewMediator;                
                startViewMediator.SetView(GameObject.Find(Consts.V_Start).GetComponent<StartView>());
                break;
            case Consts.V_Select:
                //  û�и�����Mediator ��ע��
                if (!Facade.HasMediator(SelectViewMediator.NAME))
                {
                    Facade.RegisterMediator(new SelectViewMediator());
                }
                // �õ�������Mediator
                SelectViewMediator selectViewMediator = Facade.RetrieveMediator(SelectViewMediator.NAME) as SelectViewMediator;
                selectViewMediator.SetView(GameObject.Find(Consts.V_Select).GetComponent<SelectView>());
                break;
            case Consts.V_Board:
                //  û�и�����Mediator ��ע��
                if (!Facade.HasMediator(BoardViewMediator.NAME))
                {
                    Facade.RegisterMediator(new BoardViewMediator());
                }
                // �õ�������Mediator
                BoardViewMediator boardViewMediator = Facade.RetrieveMediator(BoardViewMediator.NAME) as BoardViewMediator;
                boardViewMediator.SetView(GameObject.Find(Consts.UI_Level).transform.Find(Consts.V_Board).GetComponent<BoardView>());
                break;
            default:
                break;
        }
    }
}
