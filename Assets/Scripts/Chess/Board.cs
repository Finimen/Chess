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
    [DisallowMultipleComponent]
    internal class Board : MonoBehaviour
    {
        [SerializeField] protected Material red;
        [SerializeField] protected Material green;

        [SerializeField] private ColorID currentColor;

        public static List<Figure> AllFigures { get; private set; }

        public static Material Red;
        public static Material Green;

        private void Awake()
        {
            AllFigures = FindObjectsOfType<Figure>().ToList();

            Red = red;
            Green = green;
        }

        protected static Vector3 ConvertCoordinates(Figure figure)
        {
           return new Vector3(figure.Location.Position.x * 1.5f, figure.transform.localPosition.y, figure.Location.Position.y * 1.5f);
        }

        protected static Vector3 ConvertCoordinates(Transform figure, Location location)
        {
            return new Vector3(location.Position.x * 1.5f, figure.localPosition.y, location.Position.y * 1.5f);
        }

        protected static void TransformFigure(Figure figure)
        {

        }

        protected static bool SuqareIsEmpty(Location suqare)
        {
            bool suqareIsEmpty = true;

            foreach (Figure figure in AllFigures)
            {
                if (figure.Location.Position == suqare.Position)
                {
                    suqareIsEmpty = false;
                }
            }

            return suqareIsEmpty;
        }

        private static bool CanMoveDiagonal(Location suqare, Location figureMovment, ColorID colorID)
        {
            foreach (Figure obstacle in AllFigures)
            {
                if (obstacle.Location.Position == suqare.Position && colorID == obstacle.ColorID)
                {
                    return false;
                }

                if (obstacle.Location.Position.y > figureMovment.Position.y && suqare.Position.y > obstacle.Location.Position.y)
                {
                    bool returnedResult = CheckDiagonals(obstacle, suqare, figureMovment, 1);

                    if(!returnedResult)
                    {
                        return returnedResult;
                    }
                }
                else if (obstacle.Location.Position.y < figureMovment.Position.y && suqare.Position.y < obstacle.Location.Position.y)
                {
                    bool returnedResult = CheckDiagonals(obstacle, suqare, figureMovment, -1);

                    if (!returnedResult)
                    {
                        return returnedResult;
                    }
                }
            }

            return true;
        }

        protected static bool CheckDiagonals(Figure obstacle, Location suqare, Location figureMovment, int yCoeff)
        {
            bool stayOnDiagonal = false;

            for (int i = 0; i < 9; i++)
            {
                if (figureMovment.Position.x + i == obstacle.Location.Position.x && figureMovment.Position.y + i * yCoeff == obstacle.Location.Position.y)
                {
                    stayOnDiagonal = true;
                }
                else if (figureMovment.Position.x - i == obstacle.Location.Position.x && figureMovment.Position.y + i * yCoeff == obstacle.Location.Position.y)
                {
                    stayOnDiagonal = true;
                }
            }

            if (stayOnDiagonal)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (obstacle.Location.Position.x + i == suqare.Position.x && obstacle.Location.Position.y + i * yCoeff == suqare.Position.y)
                    {
                        return false;
                    }
                    else if (obstacle.Location.Position.x - i == suqare.Position.x && obstacle.Location.Position.y + i * yCoeff == suqare.Position.y)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        protected static bool CanMoveCanJumpFigures(Location suqare, ColorID colorID)
        {
            bool suqareIsEmpty = true;

            foreach (Figure figure in AllFigures)
            {
                if (figure.Location.Position == suqare.Position && colorID == figure.ColorID)
                {
                    suqareIsEmpty = false;
                }
            }

            return suqareIsEmpty;
        }

        protected static bool SuqareIsEnemy(Cell cell)
        {
            Figure figure = FindFigureByLocationOrReturnNull(cell.Location);

            if(figure && figure.ColorID != cell.CurrentFigure.ColorID)
            {
                if (FigureIsKing(figure))
                {
                    Debug.Log("KING_DANGER");
                }

                return true;
            }

            return false;
        }

        protected static bool SuqareIsEnemy(Location suqare, ColorID colorID)
        {
            Figure figure = FindFigureByLocationOrReturnNull(suqare);

            if (figure && figure.ColorID != colorID)
            {
                if (FigureIsKing(figure))
                {
                    Debug.Log("GAME_OVER");
                }

                return true;
            }

            return false;
        }

        protected static bool FigureIsKing(Figure figure)
        {
            if (!figure)
            {
                return false;
            }

            if (figure as King)
            {
                return true;
            }

            return false;
        }

        protected static void DestroyFigure(Location suqare)
        {
            Figure figure = FindFigureByLocationOrReturnNull(suqare);

            if(figure)
            {
                LeanTween.scale(figure.gameObject, Vector3.zero, .5f).setEaseInOutBack().destroyOnComplete = true;

                AllFigures.Remove(figure);

                Debug.Log("FIGURE_DESTROED");
            }
        }

        private static Figure FindFigureByLocationOrReturnNull(Location suqare)
        {
            foreach (Figure figure in AllFigures)
            {
                if (figure.Location.Position == suqare.Position)
                {
                    return figure;
                }
            }

            return null;
        }

        protected static List<Location> GetAllAttackSuqares()
        {
            List<Location> allAttackSuqares = new List<Location>();

            foreach(Figure figure in AllFigures)
            {
                allAttackSuqares.AddRange(figure.FindAvaibleCells());
            }

            return allAttackSuqares;
        }

        protected static List<Location> GetAllAttackSuqaresForTeam(ColorID teamID)
        {
            List<Location> allAttackSuqares = new List<Location>();

            foreach (Figure figure in AllFigures)
            {
                if(figure.ColorID != teamID)
                {
                    allAttackSuqares.AddRange(figure.FindAvaibleCells());
                }
            }

            return allAttackSuqares;
        }

        public static List<Location> GetAllAttackSuqaresForTeam(Figure figureSelected, Location figureNewLocation)
        {
            List<Location> allAttackSuqares = new List<Location>();

            foreach (Figure figure in AllFigures)
            {
                if(figureSelected == figure)
                {
                    Location lastPostion = figureSelected.Location;

                    figureSelected.Location = figureNewLocation;

                    allAttackSuqares.AddRange(figure.FindAvaibleCells());

                    figureSelected.Location = lastPostion;
                }
                else if (figure.ColorID != figureSelected.ColorID)
                {
                    allAttackSuqares.AddRange(figure.FindAvaibleCells());
                }
            }

            return allAttackSuqares;
        }

        protected static void GameOver(ColorID loseTeamID)
        {
            Debug.LogWarning("LOSE_TEAM:" + loseTeamID);
        }

        protected static void Mat(ColorID matTeamID)
        {
            Debug.LogWarning("Mat_TEAM:" + matTeamID);
        }

        public static void CreateFigure(Location location, FigureType figureType)
        {
            Figure figureToDestroy = FindFigureByLocationOrReturnNull(location);

            AllFigures.Remove(figureToDestroy);

            Destroy(figureToDestroy.gameObject);


        }
    }
}



/*
        private static bool CanMoveDiagonal(Location suqare, Location figureMovment, ColorID colorID)
        {
            foreach (Figure obstacle in AllFigures)
            {
                if (obstacle.Location.Position == suqare.Position && colorID == obstacle.ColorID)
                {
                    return false;
                }

                if (obstacle.Location.Position.y > figureMovment.Position.y && suqare.Position.y > obstacle.Location.Position.y)
                {
                    bool stayOnDiagonal = false;

                    for (int i = 0; i < 9; i++)
                    {
                        if (figureMovment.Position.x + i == obstacle.Location.Position.x && figureMovment.Position.y + i == obstacle.Location.Position.y)
                        {
                            stayOnDiagonal = true;
                        }
                        else if (figureMovment.Position.x - i == obstacle.Location.Position.x && figureMovment.Position.y + i == obstacle.Location.Position.y)
                        {
                            stayOnDiagonal = true;
                        }
                    }

                    if (stayOnDiagonal)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (obstacle.Location.Position.x + i == suqare.Position.x && obstacle.Location.Position.y + i == suqare.Position.y)
                            {
                                return false;
                            }
                            else if (obstacle.Location.Position.x - i == suqare.Position.x && obstacle.Location.Position.y + i == suqare.Position.y)
                            {
                                return false;
                            }
                        }
                    }
                }
                else if (obstacle.Location.Position.y < figureMovment.Position.y && suqare.Position.y < obstacle.Location.Position.y)
                {
                    bool stayOnDiagonal = false;

                    for (int i = 0; i < 9; i++)
                    {
                        if (figureMovment.Position.x + i == obstacle.Location.Position.x && figureMovment.Position.y - i == obstacle.Location.Position.y)
                        {
                            stayOnDiagonal = true;
                        }
                        else if (figureMovment.Position.x - i == obstacle.Location.Position.x && figureMovment.Position.y - i == obstacle.Location.Position.y)
                        {
                            stayOnDiagonal = true;
                        }
                    }

                    if (stayOnDiagonal)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (obstacle.Location.Position.x + i == suqare.Position.x && obstacle.Location.Position.y - i == suqare.Position.y)
                            {
                                return false;
                            }
                            else if (obstacle.Location.Position.x - i == suqare.Position.x && obstacle.Location.Position.y - i == suqare.Position.y)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
        */
