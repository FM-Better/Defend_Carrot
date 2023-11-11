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
            #region 开始场景
            case Consts.V_Start:
                //  没有该面板的Mediator 则注册
                if (!Facade.HasMediator(StartViewMediator.NAME))
                {
                    Facade.RegisterMediator(new StartViewMediator());
                }
                // 得到该面板的Mediator
                StartViewMediator startViewMediator = Facade.RetrieveMediator(StartViewMediator.NAME) as StartViewMediator;                
                startViewMediator.SetView(GameObject.Find(Consts.V_Start).GetComponent<StartView>());
                break;
            #endregion

            #region 选关场景
            case Consts.V_Select:
                //  没有该面板的Mediator 则注册
                if (!Facade.HasMediator(SelectViewMediator.NAME))
                {
                    Facade.RegisterMediator(new SelectViewMediator());
                }
                // 得到该面板的Mediator
                SelectViewMediator selectViewMediator = Facade.RetrieveMediator(SelectViewMediator.NAME) as SelectViewMediator;
                selectViewMediator.SetView(GameObject.Find(Consts.V_Select).GetComponent<SelectView>());
                break;
            #endregion

            #region 关卡场景
            case Consts.V_Board:
                //  没有该面板的Mediator 则注册
                if (!Facade.HasMediator(BoardViewMediator.NAME))
                {
                    Facade.RegisterMediator(new BoardViewMediator());
                }
                // 得到该面板的Mediator
                BoardViewMediator boardViewMediator = Facade.RetrieveMediator(BoardViewMediator.NAME) as BoardViewMediator;
                boardViewMediator.SetView(GameObject.Find(Consts.UI_Level).transform.Find(Consts.V_Board).GetComponent<BoardView>());
                break;
            case Consts.V_CountDown:
                //  没有该面板的Mediator 则注册
                if (!Facade.HasMediator(CountDownViewMediator.NAME))
                {
                    Facade.RegisterMediator(new CountDownViewMediator());
                }
                // 得到该面板的Mediator
                CountDownViewMediator countDownViewMediator = Facade.RetrieveMediator(CountDownViewMediator.NAME) as CountDownViewMediator;
                countDownViewMediator.SetView(GameObject.Find(Consts.UI_Level).transform.Find(Consts.V_CountDown).GetComponent<CountDownView>());
                break;
            case Consts.V_Menu:
                //  没有该面板的Mediator 则注册
                if (!Facade.HasMediator(MenuViewMediator.NAME))
                {
                    Facade.RegisterMediator(new MenuViewMediator());
                }
                // 得到该面板的Mediator
                MenuViewMediator menuViewMediator = Facade.RetrieveMediator(MenuViewMediator.NAME) as MenuViewMediator;
                menuViewMediator.SetView(GameObject.Find(Consts.UI_Level).transform.Find(Consts.V_Menu).GetComponent<MenuView>());
                break;
            #endregion

            #region 通关场景
            case Consts.V_Complete:
                //  没有该面板的Mediator 则注册
                if (!Facade.HasMediator(CompleteViewMediator.NAME))
                {
                    Facade.RegisterMediator(new CompleteViewMediator());
                }
                // 得到该面板的Mediator
                CompleteViewMediator completeViewMediator = Facade.RetrieveMediator(CompleteViewMediator.NAME) as CompleteViewMediator;
                completeViewMediator.SetView(GameObject.Find(Consts.V_Complete).GetComponent<CompleteView>());
                break;
            #endregion
            default:
                break;
        }
    }
}
