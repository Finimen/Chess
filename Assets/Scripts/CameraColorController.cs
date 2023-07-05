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
    internal class CameraColorController: MonoBehaviour
    {
        [SerializeField] private ColorID currentColorID;

        [Space(15)]
        [SerializeField] private Color enemyDestroed;
        [SerializeField] private Color frendlyDestroed;

        private UnityEngine.Camera cameraMain;

        private Color startColor;

        private void Awake()
        {
            cameraMain = FindObjectOfType<UnityEngine.Camera>();
        }

        private void Start()
        {
            startColor = cameraMain.backgroundColor;

            EventHolder.OnFigureDestroyed.AddListener(FigureDistroed);
        }

        private void FigureDistroed(Figure figureDestroed)
        {
            if(figureDestroed.ColorID == currentColorID)
            {
                StartCoroutine(SetCameraColorAndReturnStart(frendlyDestroed));
            }
            else
            {
                StartCoroutine(SetCameraColorAndReturnStart(enemyDestroed));
            }
        }

        private void GameOver(ColorID teamWinnedID)
        {
            if (teamWinnedID == currentColorID)
            {
                StartCoroutine(SetCameraColorAndReturnStart(frendlyDestroed));
            }
            else
            {
                StartCoroutine(SetCameraColorAndReturnStart(enemyDestroed));
            }
        }

        private System.Collections.IEnumerator SetCameraColorAndReturnStart(Color targetColor)
        {
            for(int i = 0; i < 120; i++)
            {
                yield return null;

                cameraMain.backgroundColor = Color.Lerp(cameraMain.backgroundColor, targetColor, .02f);
            }

            for (int i = 0; i < 120; i++)
            {
                yield return null;

                cameraMain.backgroundColor = Color.Lerp(cameraMain.backgroundColor, startColor, .02f);
            }
        }

        private System.Collections.IEnumerator SetCameraColor(Color targetColor)
        {
            for (int i = 0; i < 120; i++)
            {
                yield return null;

                cameraMain.backgroundColor = Color.Lerp(cameraMain.backgroundColor, targetColor, .02f);
            }
        }
    }
}