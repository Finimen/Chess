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
using FinimenSniperC.UI;

namespace FinimenSniperC
{
    public enum FigureType
    {
        Pawn = 0,
        Rook = 1,
        Bishop = 2,
        Knight = 3,
        Queen = 4
    }

    public enum TransformationState
    {
        Start = 0,
        End = 1
    }

    [DisallowMultipleComponent]
    internal class TransformationUI : MonoBehaviour
    {
        private UIAlphaController[] alphaControllers;

        private Figure figureToDestroy;

        private FigureType figureToCreate;

        private void Awake()
        {
            alphaControllers = GetComponentsInChildren<UIAlphaController>();
        }

        private void Start()
        {
            EventHolder.OnPawnTransformation += SetActveTransformation;
        }

        private void SetActveTransformation(Figure figureToDestroy)
        {
            this.figureToDestroy = figureToDestroy;

            SetButtonsAlpha(1);
        }

        public void OnClick(int figureType)
        {
            figureToCreate = (FigureType)figureType;

            FigureCreator.Instance.CreateFigure(figureToDestroy.Location, figureToDestroy.ColorID, figureToCreate);

            FigureDestoyer.Instance.DestroyFigure(figureToDestroy, 1);

            SetButtonsAlpha(0);
        }

        private void SetButtonsAlpha(float alpha)
        {
            foreach (UIAlphaController alphaController in alphaControllers)
            {
                alphaController.SetColorAlpha(alpha);
            }
        }
    }
}