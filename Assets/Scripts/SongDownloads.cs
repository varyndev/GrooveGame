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
        Text m_pointsText2;

        [SerializeField]
        Image m_downloadLockedIcon;

        [SerializeField]
        int m_pointCost;

        [SerializeField]
        string m_downloadLink;

        void YouHaveIt()
        {
            m_downloadTitleText.text = "You Have It";
            m_downloadArtistText.text = "by Sky Girlz";
            m_pointCost = 5000;
            m_downloadLink = "http://www.groovegame.com/music/YouHaveIt.mp3";
        }

        void LayWithMe()
        {
            m_downloadTitleText.text = "Lay With Me";
            m_downloadArtistText.text = " by Sky Girlz";
            m_pointCost = 10000;
            m_downloadLink = "http://www.groovegame.com/music/LayWithMe.mp3";
        }

        void MoreThanLove()
        {
            m_downloadTitleText.text = "More Than Love";
            m_downloadArtistText.text = " by Sky Girlz";
            m_pointCost = 15000;
            m_downloadLink = "www.groovegame.com/freesongz";
        }

        public void OpenDownload()
        {
            if (Player.Instance.coinsTotal >= m_pointCost)
            {
                Application.OpenURL(m_downloadLink);
            }
        }

        void Update()
        {
            //Update Download button info
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

            //Update download button points text
            if (Player.Instance.coinsTotal >= m_pointCost)
            {
                m_pointsText.text = "Click to Download!";
                m_downloadLockedIcon.enabled = false;
                m_pointsText2.text = "";
            }
            else
            {
                m_pointsText.text = "Earn " +  m_pointCost +  " points";
                m_pointsText2.text = "to unlock download!";
                m_downloadLockedIcon.enabled = true;
            }
        }
    }
}
