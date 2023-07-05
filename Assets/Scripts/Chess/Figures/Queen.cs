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
    internal class Queen : Figure
    {
        [NonSerialized]
        private readonly Location[] steps = new Location[] {
            new Location(new Vector2(1, 0)),
            new Location(new Vector2(2, 0)),
            new Location(new Vector2(3, 0)),
            new Location(new Vector2(4, 0)),
            new Location(new Vector2(5, 0)),
            new Location(new Vector2(6, 0)),
            new Location(new Vector2(7, 0)),
            new Location(new Vector2(0, 1)),
            new Location(new Vector2(0, 2)),
            new Location(new Vector2(0, 3)),
            new Location(new Vector2(0, 4)),
            new Location(new Vector2(0, 5)),
            new Location(new Vector2(0, 6)),
            new Location(new Vector2(0, 7)),
            new Location(new Vector2(-1, 0)),
            new Location(new Vector2(-2, 0)),
            new Location(new Vector2(-3, 0)),
            new Location(new Vector2(-4, 0)),
            new Location(new Vector2(-5, 0)),
            new Location(new Vector2(-6, 0)),
            new Location(new Vector2(-7, 0)),
            new Location(new Vector2(0, -1)),
            new Location(new Vector2(0, -2)),
            new Location(new Vector2(0, -3)),
            new Location(new Vector2(0, -4)),
            new Location(new Vector2(0, -5)),
            new Location(new Vector2(0, -6)),
            new Location(new Vector2(0, -7)),
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
            new Location(new Vector2(7, -7))
        };

        public override MoveType MoveType
        {
            get
            {
                return MoveType.Queen;
            }
        }

        public override Location[] Steps
        {
            get
            {
                return steps;
            }
        }

        public override void Move(Location target)
        {
            Location = target;
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
        }
    }
}