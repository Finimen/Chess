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
    internal class Bishop : Figure
    {
        [NonSerialized]
        private readonly Location[] steps = new Location[] {
            new Location(new Vector2(1, 1)),
            new Location(new Vector2(2, 2)),
            new Location(new Vector2(3, 3)),
            new Location(new Vector2(4, 4)),
            new Location(new Vector2(5, 5)),
            new Location(new Vector2(6, 6)),
            new Location(new Vector2(7, 7)),
            new Location(new Vector2(-1, -1)),
            new Location(new Vector2(-2, -2)),
            new Location(new Vector2(-3, -3)),
            new Location(new Vector2(-4, -4)),
            new Location(new Vector2(-5, -5)),
            new Location(new Vector2(-6, -6)),
            new Location(new Vector2(-7, -7)),
            new Location(new Vector2(-1, 1)),
            new Location(new Vector2(-2, 2)),
            new Location(new Vector2(-3, 3)),
            new Location(new Vector2(-4, 4)),
            new Location(new Vector2(-5, 5)),
            new Location(new Vector2(-6, 6)),
            new Location(new Vector2(-7, 7)),
            new Location(new Vector2(1, -1)),
            new Location(new Vector2(2, -2)),
            new Location(new Vector2(3, -3)),
            new Location(new Vector2(4, -4)),
            new Location(new Vector2(5, -5)),
            new Location(new Vector2(6, -6)),
            new Location(new Vector2(7, -7)),
        };

        public override MoveType MoveType
        {
            get
            {
                return MoveType.Bishop;
            }
        }

        public override Location[] Steps
        {
            get
            {
                return steps;
            }
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
        }

        public override void Move(Location target)
        {
            Location = target;
        }
    }
}