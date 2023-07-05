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

namespace FinimenSniperC
{
    [DisallowMultipleComponent]
    internal class FigureCreator : MonoBehaviour
    {
        [SerializeField] private Transform board;

        [Space(15)]
        [SerializeField] private Knight blackKnight;
        [SerializeField] private Bishop blackBishop;
        [SerializeField] private Rook blackRook;
        [SerializeField] private Queen blackQueen;

        [Space(15)]
        [SerializeField] private Knight whiteKnight;
        [SerializeField] private Bishop whiteBishop;
        [SerializeField] private Rook whiteRook;
        [SerializeField] private Queen whiteQueen;

        public static FigureCreator Instance { get; private set; }

        public void CreateFigure(Location location, ColorID colorID, FigureType figureType)
        {
            Figure newFigure = null;

            switch (figureType)
            {
                case FigureType.Queen:
                    newFigure = CreateQueen(colorID);
                    break;
                case FigureType.Bishop:
                    newFigure = CreateBishop(colorID);
                    break;
                case FigureType.Rook:
                    newFigure = CreateRook(colorID);
                    break;
                case FigureType.Knight:
                    newFigure = CreateKnight(colorID);
                    break;
            }

            Board.AllFigures.Add(newFigure);

            LeanTween.scale(newFigure.gameObject, Vector3.zero, 0).setOnComplete(() => LeanTween.scale(newFigure.gameObject, new Vector3(1,1,1), 1).setDelay(.5f).setEaseInOutBack());

            EventHolder.OnNewFigureCreated.Invoke(newFigure);

            SetLocationAndColorID(newFigure, location, colorID);
        }

        private Figure CreateQueen(ColorID colorID)
        {
            Queen queen;

            if (colorID == ColorID.Black)
            {
                queen = Instantiate(blackQueen, board);
            }
            else
            {
                queen = Instantiate(whiteQueen, board);
            }

            return queen;
        }

        private Figure CreateKnight(ColorID colorID)
        {
            Knight knight;

            if (colorID == ColorID.Black)
            {
                knight = Instantiate(blackKnight, board);
            }
            else
            {
                knight = Instantiate(whiteKnight, board);
            }

            return knight;
        }

        private Figure CreateRook(ColorID colorID)
        {
            Rook rook;

            if (colorID == ColorID.Black)
            {
                rook = Instantiate(blackRook, board);
            }
            else
            {
                rook = Instantiate(whiteRook, board);
            }

            return rook;
        }

        private Figure CreateBishop(ColorID colorID)
        {
            Bishop bishop;

            if (colorID == ColorID.Black)
            {
                bishop = Instantiate(blackBishop, board);
            }
            else
            {
                bishop = Instantiate(whiteBishop, board);
            }

            return bishop;
        }

        private void SetLocationAndColorID(Figure figure, Location location, ColorID colorID)
        {
            figure.Location = location;

            figure.ColorID = colorID;
        }

        private void Awake()
        {
            Instance = this;
        }
    }
} 