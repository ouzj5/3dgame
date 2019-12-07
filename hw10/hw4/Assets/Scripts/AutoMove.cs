using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AutoMove
{
    public static AutoMove autoMove = new AutoMove();
	public MySceneController scenecontroller;
    private int devilNum;
    private int priestNum;
    private int BoatCoast; // -1 -> left, 1 -> right.
    //P：船上有一个牧师
    //D：船运载一个恶魔
    //PP：船运载两个牧师
    //DD：船运载两个恶魔
    //PD：船运载一个牧师，一个恶魔
    private enum BoatAction {empty, P, D, PP, DD, PD }
    private bool tipFinished = true;
    private BoatAction nextAction;
    private int tipStep = 0;
    private int chMoveNum = 0;

    private AutoMove() {
		scenecontroller = (MySceneController)Director.get_Instance().curren;
	}

    public void move()
    {
        if (tipFinished)
        {
            tipFinished = false;
            Debug.Log(tipStep);
			int[] fromCount = scenecontroller.coast_from.getCoastModel().getCharacterNum();
			priestNum = fromCount[0];
			devilNum = fromCount[1];
			BoatCoast = scenecontroller.boat.getModel().getTFflag();
            if (tipStep == 0)
            {
                nextAction = getNextAction();
                if ((int)nextAction >= 3)
                {
                    chMoveNum = 2;
                }
                else if ((int)nextAction > 0) chMoveNum = 1;
                else chMoveNum = 0;
                tipStep++;
            }
            Debug.Log("next state is " + nextAction);
            DoAction();
        }
    }

    private void DoAction()
    {
        if (tipStep == 1 && chMoveNum != 0)
        {
            if (nextAction == BoatAction.D)
            {
                devilOnBoat();
            }
            else if (nextAction == BoatAction.DD)
            {
                devilOnBoat();
            }
            else if (nextAction == BoatAction.P)
            {
                priestOnBoat();
            }
            else if (nextAction == BoatAction.PP)
            {
                priestOnBoat();
            }
            else if (nextAction == BoatAction.PD)
            {
                priestOnBoat();
            }
            tipStep++;
        }
        else if (chMoveNum == 2 && tipStep == 2)
        {
            if (nextAction == BoatAction.DD)
            {
                devilOnBoat();
            }
            else if (nextAction == BoatAction.PP)
            {
                priestOnBoat();
            }
            else if (nextAction == BoatAction.PD)
            {
                devilOnBoat();
            }
            tipStep++;
        }
        else if((chMoveNum == 1 && tipStep == 2) || (chMoveNum == 2 && tipStep == 3) || (chMoveNum == 0 && tipStep == 1))
        {
			scenecontroller.moveboat();
            tipStep++;
        }
        else if ((chMoveNum == 1 && tipStep >= 3) || (chMoveNum == 2 && tipStep >= 4) || (chMoveNum == 0 && tipStep>=2))
        {
            GetOffBoat();
        }
        tipFinished = true;
    }

    private void GetOffBoat()
    {
        if((priestNum == 0 && devilNum == 2) || (priestNum == 0 && devilNum == 0))
        {
			if (scenecontroller.boat.getModel().getTFflag() == -1)
            {
				foreach (var x in scenecontroller.boat.getModel().passenger)
                {
                    if (x != null)
                    {
						scenecontroller.isClickChar(x);
                        break;
                    }
                }
				if (scenecontroller.boat.getModel().isEmpty()) tipStep = 0;
            }
            else tipStep = 0;
        }
		else if (((priestNum == 0 && devilNum == 1)) && scenecontroller.boat.getModel().getTFflag() == 1)
        {
            tipStep = 0;
        }
        else
        {
			foreach (var x in scenecontroller.boat.getModel().passenger)
            {
				if (x != null && x.getCharacterModel().getType() == 1)
                {
					scenecontroller.isClickChar(x);
                    tipStep = 0;
                    break;
                }
            }
            if (tipStep != 0)
            {
				foreach (var x in scenecontroller.boat.getModel().passenger)
                {
                    if (x != null)
                    {
						scenecontroller.isClickChar(x);
                        tipStep = 0;
                        break;
                    }
                }
            }
        }
    }

    private void priestOnBoat()
    {
        if(BoatCoast == 1)
        {
			foreach(var x in scenecontroller.coast_from.getCoastModel().character)
            {
				if (x!=null && x.getCharacterModel().getType() == 0)
                {
					scenecontroller.isClickChar(x);
                    return;
                }
            }
        }
        else
        {
			foreach (var x in scenecontroller.coast_to.getCoastModel().character)
            {
				if (x != null && x.getCharacterModel().getType() == 0)
                {
					scenecontroller.isClickChar(x);
                    return;
                }
            }
        }
    }

    private void devilOnBoat()
    {
        if (BoatCoast == 1)
        {
			foreach (var x in scenecontroller.coast_from.getCoastModel().character)
            {
				if (x != null && x.getCharacterModel().getType() == 1)
                {
					scenecontroller.isClickChar(x);
                    return;
                }
            }
        }
        else
        {
			foreach (var x in scenecontroller.coast_to.getCoastModel().character)
            {
				if (x != null && x.getCharacterModel().getType() == 1)
                {
					scenecontroller.isClickChar(x);
                    return;
                }
            }
        }
    }

    private BoatAction getNextAction()
    {
		Debug.Log("dn" + devilNum + "  pn:" + priestNum);
        BoatAction next = BoatAction.empty;
        if (BoatCoast == 1)
        {
            if (devilNum == 3 && priestNum == 3)//3P3DR
            {
                next = BoatAction.PD;
            }
            else if (devilNum == 2 && priestNum == 3)//3P2DR
            {
                next = BoatAction.DD;
            }
            else if (devilNum == 1 && priestNum == 3)//3P1DR
            {
                next = BoatAction.PP;
            }
            else if (devilNum == 2 && priestNum == 2)//2P2DR
            {
                next = BoatAction.PP;
            }
            else if (devilNum == 3 && priestNum == 0)//0P3DR
            {
                next = BoatAction.DD;
            }
            else if (devilNum == 1 && priestNum == 1)//1P1DR
            {
                next = BoatAction.PD;
            }
            else if (devilNum == 2 && priestNum == 0)//0P2DR
            {
                next = BoatAction.D;
            }
            else if (devilNum == 1 && priestNum == 2)//2P1DR
            {
                next = BoatAction.P;
            }
            else if (devilNum == 2 && priestNum == 1)//1P2DR
            {
                next = BoatAction.P;
            }
            else if (devilNum == 1 && priestNum == 0)//0P1DR
            {
                next = BoatAction.D;
            }
            else if(devilNum == 3 && priestNum == 2)//2P3DR
            {
                next = BoatAction.D;
            }
            else next = BoatAction.empty;
        }
        else
        {
            if (devilNum == 2 && priestNum == 2)//2P2DL
            {
                next = BoatAction.empty;
            }
            else if (devilNum == 1 && priestNum == 3)//3P1DL
            {
                next = BoatAction.empty;
            }
            else if (devilNum == 2 && priestNum == 3)//3P2DL
            {
                next = BoatAction.D;
            }
            else if (devilNum == 0 && priestNum == 3)//3P0DL
            {
                next = BoatAction.empty;
            }
            else if (devilNum == 1 && priestNum == 1)//1P1DL
            {
                next = BoatAction.D;
            }
            else if (devilNum == 2 && priestNum == 0)//0P2DL
            {
                next = BoatAction.D;
            }
			else if (devilNum == 1 && priestNum == 0)//0P1DL
            {
                next = BoatAction.empty;
            }
            else next = BoatAction.empty;
        }
        return next;
    }

    public void restart()
    {
        tipStep = 0;
        chMoveNum = 0;
    }
}
