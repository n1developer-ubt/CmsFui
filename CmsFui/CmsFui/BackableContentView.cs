using System;
using System.Collections.Generic;
using System.Text;

namespace CmsFui
{
    interface BackableContentView
    {
        event GoBack.ActionPerformed BackToDashboard;
    }
}
