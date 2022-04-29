using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eFormsBiz
{
    // Class CSessionBuffer
    public class CSessionBuffer
    {
        private List<CSessionInfo> m_List;

        // GetSessions: return list of sessions
        public List<CSessionInfo> GetSessions()
        {
            return m_List;
        }

        // SetSessions: pList - set the list of sessions
        public void SetSessions(List<CSessionInfo> pList)
        {
            m_List = pList;
        }
    }
}