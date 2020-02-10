using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Util
{
    public static class Data
    {
        public static  List<BankBranch> BANK_BRANCHES = new List<BankBranch>();

        internal static List<BankBranch> GetATMList()
        {

            XElement bankXml;

            const string xmlLocalPath = @"atm.xml";

           bankXml = XElement.Load(xmlLocalPath);
           BANK_BRANCHES = bankXml.Elements("ATM").Select(atm =>atm.ToString().ToObject<BankBranch>()).ToList();


            WebClient wc = new WebClient();


            try
            {
                string xmlServerPath = @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            catch
            {
                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            finally
            {
                wc.Dispose();
            }

            bankXml = XElement.Load(xmlLocalPath);

            return bankXml.Elements("ATM").Select(atm =>
            atm.ToString().ToObject<BankBranch>()).ToList();

        }
    }
}
