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
    [DisallowMultipleComponent, ExecuteInEditMode]
    internal class Cell : MonoBehaviour
    {
        [HideInInspector] public new bool enabled = true;

        [SerializeField] private Location location;

        private MeshRenderer meshRenderer;

        public Figure CurrentFigure;

        public Location Location {
            get
            {
                return location;
            }
            set
            {
                location = value;

                transform.localPosition = BoardFacade.ConvertCoordinates(transform, Location);
            }
        }

        public void ChangeColor()
        {
            if (BoardFacade.SuqareIsEnemy(this))
            {
                meshRenderer.material = BoardFacade.Red;
            }
            else
            {
                meshRenderer.material = BoardFacade.Green;
            }
        }

        private void OnMouseDown()
        {
            if (enabled && BoardFacade.IsKingSafe(CurrentFigure, location) == BoarStateType.Defuat)
            {
                if (BoardFacade.SuqareIsEnemy(this))
                {
                    Debug.Log("FIGURE_IS_ENEMY");

                    BoardFacade.DestroyFigure(location);
                }

                EventHolder.OnCellCelected.Invoke();

                BoardUI.Instance.OnClearUI.Invoke();

                CurrentFigure.Move(location);

                Teams.UpdateStep();
            }
        }

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
    }
}