using System;
using System.Collections;
using System.Linq;
using System.Threading;

namespace SenkaSticker.Common.TypeDef.Enum
{
    public enum IssueState
    {
        Backlog,
        Processing,
        WaitingForVerification,
        Verifying,
        Closed
    }
}
