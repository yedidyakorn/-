using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class Jobs
    {
        public BackgroundWorker bankBranchesJob = new BackgroundWorker();

        public  Jobs()
        {

            bankBranchesJob.DoWork += Bg_DoWork;
            bankBranchesJob.RunWorkerCompleted += Bg_RunWorkerCompleted;
            bankBranchesJob.ProgressChanged += Bg_ProgressChanged;
            bankBranchesJob.WorkerReportsProgress = true;
        }

        private void Bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
         //   Data.BANK_BRANCHES = e.Result;
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {

            Data.BANK_BRANCHES = Data.GetATMList();

            e.Result = Data.BANK_BRANCHES;

        }
    }
}
