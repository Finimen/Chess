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

namespace FinimenSniperC
{
    internal class Pawn : Figure
    {
        [NonSerialized] protected readonly Location[] steps = new Location[] { new Location(new Vector2(0, 1)) };

        [NonSerialized] protected readonly Location[] attacks = new Location[] { new Location(new Vector2(1, 1)), new Location(new Vector2(-1, 1)) };

        public override MoveType MoveType
        {
            get
            {
                return MoveType.Pawn;
            }
        }

        public override Location[] Steps
        {
            get
            {
                return steps;
            }
        }

        public override Location[] Attacks
        {
            get
            {
                return attacks;
            }
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
        }

        public override void Move(Location target)
        {
            Location = target;

            IsTransformation();
        }

        protected override void IsTransformation()
        {
            if(ColorID == ColorID.Black && Location.Position.y == 7 || ColorID == ColorID.White && Location.Position.y == 0)
            {
                if(EventHolder.OnPawnTransformation != null)
                {
                    EventHolder.OnPawnTransformation.Invoke(this);
                }
            }
        }
    }
}