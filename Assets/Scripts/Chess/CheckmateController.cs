/*               
            ░███████╗██╗███╗░░██╗██╗██╗░░░░░██╗███████╗███╗░░██╗   ░██████╗███╗░░██╗██╗██████╗░███████╗██████╗░░
			░██╔════╝██║████╗░██║██║████░░████║██╔════╝████╗░██║   ██╔════╝████╗░██║██║██╔══██╗██╔════╝██╔══██╗░
			░███████╗██║██╔██╗██║██║██║░██░░██║█████╗░░██╔██╗██║   ╚█████╗░██╔██╗██║██║██████╔╝█████╗░░██████╔╝░
			░██╔════╝██║██║╚████║██║██║░░░░░██║██╔══╝░░██║╚████║   ░╚═══██╗██║╚████║██║██╔═══╝░██╔══╝░░██╔══██╗░
			░██║░░░░░██║██║░╚███║██║██║░░░░░██║███████╗██║░╚███║   ██████╔╝██║░╚███║██║██║░░░░░███████╗██║░░██║░
			░╚═╝░░░░░╚═╝╚═╝░░╚══╝╚═╝╚═╝░░░░░╚═╝╚══════╝╚═╝░░╚══╝   ╚═════╝░╚═╝░░╚══╝╚═╝╚═╝░░░░░╚══════╝╚═╝░░╚═╝░
____________________________________________________________________________________________________________________________________________
                █▀▀▄ █──█ 　 ▀▀█▀▀ █──█ █▀▀ 　 ░█▀▀▄ █▀▀ ▀█─█▀ █▀▀ █── █▀▀█ █▀▀█ █▀▀ █▀▀█ 
                █▀▀▄ █▄▄█ 　 ─░█── █▀▀█ █▀▀ 　 ░█─░█ █▀▀ ─█▄█─ █▀▀ █── █──█ █──█ █▀▀ █▄▄▀ 
                ▀▀▀─ ▄▄▄█ 　 ─░█── ▀──▀ ▀▀▀ 　 ░█▄▄▀ ▀▀▀ ──▀── ▀▀▀ ▀▀▀ ▀▀▀▀ █▀▀▀ ▀▀▀ ▀─▀▀
____________________________________________________________________________________________________________________________________________
*/
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace FinimenSniperC
{
    public enum BoarStateType
    {
        Defuat = 0,
        Mate = 1,
        GameOver = 2
    }

    public struct BoardState
    {
        public TeamState BlackTeam;

        public TeamState WhiteTeam;
    }

    public struct TeamState
    {
        public ColorID ColorID { get; set; }

        public BoarStateType BoarStateType { get; set; }
    }

    [DisallowMultipleComponent]
    internal class CheckmateController : MonoBehaviour
    {
        private static King kingBlack;
        private static King kingWhite;

        private void Awake()
        {
            King[] kings = FindObjectsOfType<King>();

            if(kings[0].ColorID == ColorID.Black)
            {
                kingBlack = kings[0];
                kingWhite = kings[1];
            }
            else
            {
                kingBlack = kings[1];
                kingWhite = kings[0];
            }
        }

        private static bool CheckGameOver(Figure king, List<Location> allAttackSuqares)
        {
            List<Location> allMoves = king.FindAvaibleCells().ToList();

            foreach (Location attackSuqare in allAttackSuqares)
            {
                for(int i = allMoves.Count - 1; i >= 0; i --)
                {
                    if(attackSuqare.Position == allMoves[i].Position)
                    {
                        allMoves.Remove(allMoves[i]);
                    }
                }
            }

            if(allMoves.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void CheckMatOrGameOver()
        {
            CheckMatOrGameOverForKing(kingBlack, BoardFacade.GetAllAttackSuqaresForTeam(ColorID.Black));
            CheckMatOrGameOverForKing(kingWhite, BoardFacade.GetAllAttackSuqaresForTeam(ColorID.White));
        }

        public static BoarStateType CheckMatOrGameOver(Figure figureSelected, Location figureNewLocation)
        {
            if(figureSelected.ColorID == ColorID.Black)
            {
                return CheckMatOrGameOverForKing(kingBlack, Board.GetAllAttackSuqaresForTeam(figureSelected, figureNewLocation));
            }
            else
            {
                return CheckMatOrGameOverForKing(kingWhite, Board.GetAllAttackSuqaresForTeam(figureSelected, figureNewLocation));
            }
        }

        private static BoarStateType CheckMatOrGameOverForKing(King king, List<Location> allAttackSuqares)
        {
            foreach (Location attackSuqares in allAttackSuqares)
            {
                if (king.Location.Position == attackSuqares.Position)
                {
                    if (CheckGameOver(king, allAttackSuqares))
                    {
                        EventHolder.OnGameOver.Invoke(king.ColorID);

                        BoardFacade.GameOver(king.ColorID);

                        return BoarStateType.GameOver;
                    }
                    else
                    {
                        EventHolder.OnMate.Invoke(king.ColorID);

                        BoardFacade.Mat(king.ColorID);

                        return BoarStateType.Mate;
                    }
                }
            }
            
            return BoarStateType.Defuat;
        }
    }
}