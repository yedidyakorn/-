using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PLWPF.Helpers
{
   public static class Methods
    {

        public static void LoadHostingUnitsDairy()
        {
            try
            {
                BL_Singletone.Instance.GetHostingUnitsList().ForEach(h =>

                BL_Singletone.Instance.GetOrderList().Where(o => o.Status == BE.OrderStatuses.Closed_ApprovedByCustomer
                && o.HostingUnitKey == h.HostingUnitKey).ToList()
                .ForEach(o =>
                {
                    var gr = BL_Singletone.Instance.GetGuestRequestsByKey(o.GuestRequestKey);

                    if (gr.EntryDate.Date > DateTime.Now.AddMonths(-1).Date &&
                    gr.EntryDate.Date < DateTime.Now.AddMonths(11).Date)
                    {
                        DateTime cureentDate = gr.EntryDate;

                        while (cureentDate.Date != gr.ReleaseDate.Date.AddDays(1))
                        {
                            h.Diary[cureentDate.Month - 1, cureentDate.Day - 1] = true;

                            cureentDate = cureentDate.AddDays(1);
                        }

                    }

                    BL_Singletone.Instance.UpdateHostingUnit(h);
                }
                )
                );
            }
            catch (LogicException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("general error while loading Hosting Units dairy");
            }
        }

    }
}

