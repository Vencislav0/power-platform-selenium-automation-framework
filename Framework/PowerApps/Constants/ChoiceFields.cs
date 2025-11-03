using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.PowerApps.Constants
{
    public enum IsArmedChoices
    {
        Yes = 0,
        No = 1
    }

    public enum MissionTypeChoices
    {
        Cargo,
        Passenger,
        Other
    }

    public enum MaintenanceTasksStatusChoices
    {
        Completed,
        NotStarted,
        InProgress

    }

    public enum FinalOutcomeChoices
    {
        
        ReturnToService,
        Decomission,
        OtherReason
    }
}
