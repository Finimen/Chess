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
using System;
using UnityEngine;
using UnityEngine.Events;

namespace FinimenSniperC
{
    [DisallowMultipleComponent]
    internal class EventHolder : MonoBehaviour
    {
        [SerializeField] private UnityEventColorID onMate;

        [SerializeField] private UnityEventColorID onGameOver;
        [SerializeField] private UnityEventFigure onFigureDestroyed;

        [SerializeField] private UnityEvent onCellCelected;

        public static UnityEventColorID OnMate { get; private set; }
        public static UnityEventColorID OnGameOver { get; private set; }
        public static UnityEventFigure OnFigureDestroyed { get; private set; }
        public static UnityEvent OnCellCelected { get; private set; }

        public static Action<Figure> OnPawnTransformation { get; set; }
        public static Action<Figure> OnNewFigureCreated { get; set; }

        private void Awake()
        {
            OnMate = onMate;

            OnGameOver = onGameOver;

            OnFigureDestroyed = onFigureDestroyed;

            OnCellCelected = onCellCelected;
        }
    }

    [Serializable] internal class UnityEventColorID : UnityEvent<ColorID>
    {

    }
    [Serializable] internal class UnityEventFigure : UnityEvent<Figure>
    {

    }
}