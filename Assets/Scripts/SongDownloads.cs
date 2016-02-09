using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace BoogieDownGames{

    public class SongDownloads : MonoBehaviour {

        [SerializeField]
        Text m_downloadTitleText;

        [SerializeField]
        Text m_downloadArtistText;

        [SerializeField]
        Text m_pointsText;

        [SerializeField]
        Image m_downloadLockedIcon;

        [SerializeField]
        int m_pointCost;

        [SerializeField]
        string m_downloadLink;

        void YouHaveIt()
        {
            m_downloadTitleText.text = "You Have It";
            m_downloadArtistText.text = "Tori Martin";
            m_pointsText.text = "5000 points";
            m_pointCost = 5000;
            m_downloadLink = "www.japanjapan.net/freesong";
        }

        void LayWithMe()
        {
            m_downloadTitleText.text = "Lay With Me";
            m_downloadArtistText.text = "Sky Girl Jo";
            m_pointsText.text = "10000 points";
            m_pointCost = 10000;
            m_downloadLink = "www.japanjapan.net/freesongs";
        }

        void MoreThanLove()
        {
            m_downloadTitleText.text = "More Than Love";
            m_downloadArtistText.text = "";
            m_pointsText.text = "15000 points";
            m_pointCost = 15000;
            m_downloadLink = "www.japanjapan.net/freesongz";
        }

        public bool IsDownloadLocked(int downloadIndex)
         {
            bool isdownloadLocked = true;
             if (isdownloadLocked)
             {
                isdownloadLocked = !Player.Instance.IsDownloadUnlocked(downloadIndex);
             }
             return isdownloadLocked;
         }

        public void OpenDownload()
        {
            if (Player.Instance.coinsTotal >= m_pointCost)
            {
                Application.OpenURL(m_downloadLink);
                Debug.Log("The download is opened!");
            }
            else
            {
                Debug.Log("The player does not have enough points");
            }
        }

        void Update()
        {
            if (GameMaster.Instance.CurrentDownload == 0)
            {
                YouHaveIt();
            }
            if (GameMaster.Instance.CurrentDownload == 1)
            {
                LayWithMe();
            }
            if (GameMaster.Instance.CurrentDownload == 2)
            {
                MoreThanLove();
            }

            if (Player.Instance.coinsTotal >= m_pointCost)
            {
                m_downloadLockedIcon.enabled = true;
            }
            else
            {
                m_downloadLockedIcon.enabled = false;
            }
        }
    }
}
