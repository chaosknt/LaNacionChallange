using System;
using System.Collections.Generic;
using System.Text;

namespace LaNacion.Common
{
    public class BuildInfo
    {
        public BuildInfo(string BranchName, string BuildNumber, string BuildId, string Info = "")
        {
            this.BranchName = BranchName;
            this.BuildNumber = BuildNumber;
            this.BuildId = BuildId;
            this.Info = Info;
        }

        public string BranchName { get; set; }

        public string BuildNumber { get; set; }

        public string BuildId { get; set; }

        public string Info { get; set; }
    }
}
