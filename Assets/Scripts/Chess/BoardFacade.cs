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

namespace FinimenSniperC
{
    internal class BoardFacade : Board
    {
        public static new Vector3 ConvertCoordinates(Figure figure)
        {
            return Board.ConvertCoordinates(figure);
        }

        public static new Vector3 ConvertCoordinates(Transform figure, Location location)
        {
            return Board.ConvertCoordinates(figure, location);
        }

        public static new void TransformFigure(Figure figure)
        {
            Board.TransformFigure(figure);
        }

        public static new void DestroyFigure(Location suqare)
        {
            Board.DestroyFigure(suqare);
        }

        public static new bool SuqareIsEmpty(Location suqare)
        {
            return Board.SuqareIsEmpty(suqare);
        }

        public static new bool SuqareIsEnemy(Cell cell)
        {
            return Board.SuqareIsEnemy(cell);
        }

        public static new bool SuqareIsEnemy(Location suqare, ColorID colorID)
        {
            return Board.SuqareIsEnemy(suqare, colorID);
        }

        public static bool CanMove(Location suqareToMove, Figure figureSelected)
        {
            switch (figureSelected.MoveType)
            {
                case MoveType.Pawn:
                    return CanMovePawn(suqareToMove, figureSelected);

                case MoveType.Knight:
                    return CanMovePawn(suqareToMove, figureSelected);

                case MoveType.Bishop:
                    return CanMoveBishop(suqareToMove, figureSelected);

                case MoveType.Rook:
                    return CanMoveRook(suqareToMove, figureSelected);

                case MoveType.Queen:
                    return CanMoveQueen(suqareToMove, figureSelected);

                case MoveType.King:
                    return CanMovePawn(suqareToMove, figureSelected);
            }

            return true;
        }

        public static bool CanMovePawn(Location suqareToMove, Figure figureSelected)
        {
            foreach (Figure obstacle in AllFigures)
            {
                if (!IsSuqareBusyFriendlyFigure(suqareToMove, figureSelected, obstacle))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CanMoveQueen(Location suqareToMove, Figure figureSelected)
        {
            foreach (Figure obstacle in AllFigures)
            {
                if (!IsSuqareBusyFriendlyFigure(suqareToMove, figureSelected, obstacle) || !CanMoveBishop(suqareToMove,figureSelected) || !CanMoveRook(suqareToMove, figureSelected))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CanMoveRook(Location suqareToMove, Figure figureSelected)
        {
            foreach (Figure obstacle in AllFigures)
            {
                if (!IsSuqareBusyFriendlyFigure(suqareToMove, figureSelected, obstacle))
                {
                    return false;
                }

                if (obstacle.Location.Position.y == figureSelected.Location.Position.y && obstacle.Location.Position.x > figureSelected.Location.Position.x && suqareToMove.Position.x > obstacle.Location.Position.x)
                {
                    return false;
                }
                if (obstacle.Location.Position.y == figureSelected.Location.Position.y && obstacle.Location.Position.x < figureSelected.Location.Position.x && suqareToMove.Position.x < obstacle.Location.Position.x)
                {
                    return false;
                }
                if (obstacle.Location.Position.x == figureSelected.Location.Position.x && obstacle.Location.Position.y > figureSelected.Location.Position.y && suqareToMove.Position.y > obstacle.Location.Position.y)
                {
                    return false;
                }
                if (obstacle.Location.Position.x == figureSelected.Location.Position.x && obstacle.Location.Position.y < figureSelected.Location.Position.y && suqareToMove.Position.y < obstacle.Location.Position.y)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CanMoveBishop(Location suqareToMove, Figure figureSelected)
        {
            foreach (Figure obstacle in AllFigures)
            {
                if (!IsSuqareBusyFriendlyFigure(suqareToMove, figureSelected, obstacle))
                {
                    return false;
                }

                if (obstacle.Location.Position.y > figureSelected.Location.Position.y && suqareToMove.Position.y > obstacle.Location.Position.y)
                {
                    bool returnedResult = CheckDiagonals(obstacle, suqareToMove, figureSelected.Location, 1);

                    if (!returnedResult)
                    {
                        return returnedResult;
                    }
                }
                else if (obstacle.Location.Position.y < figureSelected.Location.Position.y && suqareToMove.Position.y < obstacle.Location.Position.y)
                {
                    bool returnedResult = CheckDiagonals(obstacle, suqareToMove, figureSelected.Location, -1);

                    if (!returnedResult)
                    {
                        return returnedResult;
                    }
                }
            }

            return true;
        }

        public static BoarStateType IsKingSafe(Figure figureSelected, Location figureNewLocation)
        {
            return CheckmateController.CheckMatOrGameOver(figureSelected, figureNewLocation);
        }

        private static bool IsSuqareBusyFriendlyFigure(Location suqareToMove, Figure figureSelected, Figure obstacle)
        {
            if (obstacle.ColorID == figureSelected.ColorID && obstacle.Location.Position == suqareToMove.Position)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static new bool CanMoveCanJumpFigures(Location suqare, ColorID colorID)
        {
            return Board.CanMoveCanJumpFigures(suqare, colorID);
        }

        public static new List<Location> GetAllAttackSuqares()
        {
            return Board.GetAllAttackSuqares();
        }

        public static new List<Location> GetAllAttackSuqaresForTeam(ColorID teamID)
        {
            return Board.GetAllAttackSuqaresForTeam(teamID);
        }

        public static new void GameOver(ColorID loseTeamID)
        {
            Board.GameOver(loseTeamID);
        }

        public static new void Mat(ColorID matTeamID)
        {
            Board.Mat(matTeamID);
        }
    }
}