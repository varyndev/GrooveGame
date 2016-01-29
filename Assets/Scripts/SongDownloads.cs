using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace BoogieDownGames{

    public class DownloadItem
    {
        public string downloadId;        // unique id that refers to the AudioClip of the sound file
        public string downloadTitle;     // Full title of the song
        public string downloadArtist;    // Song artist attribution
        public int pointCost;           //Points it costs to open up download
        public bool unlocked;        // true indicates this is a locked song can be unlocked by paying for it. false for free songs.
        public string downloadLink;     //Link to open up the download

        public DownloadItem(string _downloadId, string _downloadTitle, string _downloadArtist, int _pointCost, bool _unlocked, string _downloadLink)
        {
            downloadId = _downloadId;
            downloadTitle = _downloadTitle;
            downloadArtist = _downloadArtist;
            pointCost = _pointCost;
            unlocked = _unlocked;
            downloadLink = _downloadLink;
        }
    }

    public class SongDownloads : MonoBehaviour {

        [SerializeField]
        Text m_downloadTitleText;

        [SerializeField]
        Text m_downloadArtistText;

        [SerializeField]
        Image m_downloadLockedIcon;

        [SerializeField]
        int m_pointCost = 0;

        [SerializeField]
        string m_downloadLink = "";

        private List<DownloadItem> m_downloadList;

        //TODO: May need to adjust the demoOffset and demoDuration if the song is altered for any reason
        // 		Demo times are listed in seconds
        void Awake()
         {
             m_downloadList = new List<DownloadItem>();
             m_downloadList.Add(new DownloadItem("YouHaveIt", "You Have It", "Tori Martin", 5000,  true, "www.japanjapan.net/freesong"));
             m_downloadList.Add(new DownloadItem("LayWithMe", "Lay With Me", "Sky Girl Jo", 10000, true, "www.japanjapan.net/freesongs"));
             m_downloadList.Add(new DownloadItem("MoreThanLove", "More Than Love", "", 15000, true, "www.japanjapan.net/freesongz"));
         }

         public bool IsDownloadLocked(int downloadIndex)
         {
             bool isdownloadLocked = m_downloadList[downloadIndex].unlocked;
             if (isdownloadLocked)
             {
                // TODO: determine if the player has unlocked this item
                isdownloadLocked = !Player.Instance.IsDownloadUnlocked(downloadIndex);
             }
             return isdownloadLocked;
         }

         private void SetDownloadInfo(string downloadId)
         {
             if (downloadId != null && m_downloadTitleText != null)
             {
                 int downloadIndex = 0;
                 foreach (DownloadItem download in m_downloadList)
                 {
                     if (download.downloadId == downloadId)
                     {
                         m_downloadTitleText.text = download.downloadTitle;
                         m_downloadArtistText.text = download.downloadArtist;
                         m_downloadLockedIcon.enabled = IsDownloadLocked(downloadIndex);
                         break;
                     }
                     downloadIndex++;
                 }
             }
         }

        void OpenDownload()
        {
            DownloadItem downLoad = null;
            m_pointCost = downLoad.pointCost;
            m_downloadLink = downLoad.downloadLink;

            if (Player.Instance.coinsTotal >= m_pointCost)
            {
                Application.OpenURL(m_downloadLink);
            }
            else
            {
                Debug.Log("The player does not have enough points");
            }
        }
    }
}
