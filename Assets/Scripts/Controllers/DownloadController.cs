using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BoogieDownGames
{

    public class DownloadController : UnitySingleton<DownloadController>
    {
        [SerializeField]
        private int m_currentDownIndex;

        [SerializeField]
        private int m_initialDownload;

       //TODO: Alter this list if order changes
       private List<string> m_defaultDownloadOrder = new List<string>
        {
            "YouHaveIt",
            "LayWithMe",
            "MoreThanLove",
        };

        #region PROPERTIES

        public List<string> DefaultDownloadOrder
        {
            get { return m_defaultDownloadOrder; }
        }

        #endregion

       void Awake()
        {
            // Make sure we are properly initialized and the list has something in it before we start
            if (m_defaultDownloadOrder.Count > 0)
            {
                m_initialDownload = Mathf.Clamp(m_initialDownload, 0, m_defaultDownloadOrder.Count - 1);

                m_currentDownIndex = m_initialDownload;
            }
        }

        public void DownloadAtIndex(int p_Downindex)
         {
             // Make sure index is not past the index range
             p_Downindex = Mathf.Clamp(p_Downindex, 0, m_defaultDownloadOrder.Count - 1);
             if (m_defaultDownloadOrder.Count > 0)
             {
                 m_currentDownIndex = p_Downindex;
                 GameMaster.Instance.CurrentDownload = m_currentDownIndex;
             }
         }

        public void NextDownload()
        {
            m_currentDownIndex++;
            if (m_currentDownIndex > m_defaultDownloadOrder.Count - 1)
            {
                m_currentDownIndex = 0;
            }
            GameMaster.Instance.CurrentDownload = m_currentDownIndex;
        }

        public void PrevDownload()
        {
            m_currentDownIndex--;
            if (m_currentDownIndex < 0)
            {
                m_currentDownIndex = m_defaultDownloadOrder.Count - 1;
            }
            GameMaster.Instance.CurrentDownload = m_currentDownIndex;
        }
    }
}