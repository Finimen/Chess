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
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace FinimenSniperC
{
    internal abstract class Figure : FigureBase
    {
        public event Action<Location[], Figure> OnSelected;
        public event Action OnUnselected;

        public Action<Vector2> OnStartMoving;

        [SerializeField] private ColorID colorID;
        [SerializeField] private Location location;

        public override ColorID ColorID
        {
            get
            {
                return colorID;
            }
            set
            {
                colorID = value;
            }
        }

        public override Location Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;

                LeanTween.moveLocal(gameObject, BoardFacade.ConvertCoordinates(this), .5f).setEaseInOutBack();
            }
        }

        public abstract MoveType MoveType { get;}

        public abstract Location[] Steps { get; }

        public virtual Location[] Attacks { get; }

        private void Start()
        {
            transform.localPosition = BoardFacade.ConvertCoordinates(this);

            StepsAndAttacksAplayColorID();
        }

        private void OnMouseDown()
        {
            if (Teams.IsMyTeamMoving(ColorID))
            {
                OnSelected.Invoke(FindAvaibleCells(), this);
            }
        }

        private void OnDestroy()
        {
            if(EventHolder.OnFigureDestroyed != null)
            {
                EventHolder.OnFigureDestroyed.Invoke(this);
            }
        }

        private void StepsAndAttacksAplayColorID()
        {
            foreach (Location step in Steps)
            {
                if (colorID == ColorID.White)
                {
                    step.Position = new Vector2(step.Position.x, step.Position.y * -1);
                }
            }

            if (Attacks != null)
            {
                foreach (Location attack in Attacks)
                {
                    if (colorID == ColorID.White)
                    {
                        attack.Position = new Vector2(attack.Position.x, attack.Position.y * -1);
                    }
                }
            }
        }

        public abstract void Move(Location target);

        protected virtual void IsTransformation()
        {

        }

        public override Location[] FindAvaibleCells()
        {
            List<Location> avaibleCells = new List<Location>();

            if (Attacks != null)
            {
                avaibleCells.AddRange(FindAvaibleAttacks());
                avaibleCells.AddRange(FindAvaibleMoves());
            }
            else
            {
                avaibleCells = GetAllMoves();
            }

            return avaibleCells.ToArray();
        }

        private List<Location> FindAvaibleAttacks()
        {
            List<Location> avaibleAttacks = new List<Location>();

            for (int i = 0; i < Attacks.Length; i++)
            {
                if(BoardFacade.SuqareIsEnemy(location + Attacks[i], colorID))
                {
                    avaibleAttacks.Add(location + Attacks[i]);
                }
            }

            return avaibleAttacks;
        }

        private List<Location> FindAvaibleMoves()
        {
            List<Location> avaibleMoves = new List<Location>();

            for (int i = 0; i < Steps.Length; i++)
            {
                if (BoardFacade.SuqareIsEmpty(location + Steps[i]))
                {
                    avaibleMoves.Add(location + Steps[i]);
                }
            }

            for (int i = avaibleMoves.Count - 1; i > -1; i--)
            {
                if (!avaibleMoves[i].PositionIsCorrect())
                {
                    avaibleMoves.RemoveAt(i);
                }
            }

            return avaibleMoves;
        }

        private List<Location> GetAllMoves()
        {
            List<Location> allMoves = new List<Location>(Steps.Length);

            int length = Steps.Length;

            for (int i = 0; i < length; i++)
            {
                allMoves.Add(location + Steps[i]);
            }

            for (int i = length - 1; i > -1; i--)
            {
                if (!allMoves[i].PositionIsCorrect() || !BoardFacade.CanMove(allMoves[i], this))
                {
                    allMoves.RemoveAt(i);
                }
            }

            return allMoves;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = ColorID == ColorID.White ? new Color(1, 1, 1, .5f) : new Color(0, 0, 0, .5f);

            Gizmos.DrawCube(transform.position + transform.up, new Vector3(1, 2, 1));

            transform.localPosition = BoardFacade.ConvertCoordinates(this);

            Gizmos.color = new Color(0, 8, .1f, .5f);

            foreach (Location step in Steps)
            {
                Gizmos.DrawCube(transform.position + transform.up + new Vector3(step.Position.x, 0, step.Position.y) * 1.5f, new Vector3(1, 2, 1));
            }
        }
    }
}