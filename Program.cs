using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionZip
{
    class Program
    {
        public class Region
        {
            //Store Region name and list of associated Zip Codes
            public string strRegion;
            public List<string> lstZipCodes = new List<string>();

            public Region()
            {

            }

            public Region(string thisRegion, List<string> thisZipCodes)
            {
                this.strRegion = thisRegion;
                this.lstZipCodes = thisZipCodes;
            }
        }

        public class RegionZipFeed
        {
            //Simple object used to feed information into main functionality
            public string strRegion;
            public string strZip;

            public RegionZipFeed()
            {

            }

            public RegionZipFeed(string thisRegion, string thisZip)
            {
                this.strRegion = thisRegion;
                this.strZip = thisZip;
            }
        }
        static void Main(string[] args)
        {
            List<Region> lstRegions = new List<Region>();

            //Feed of data to be stored and related
            List<RegionZipFeed> lstFeed = new List<RegionZipFeed>();
            lstFeed.Add(new RegionZipFeed("Hartford", "06001"));
            lstFeed.Add(new RegionZipFeed("Hartford", "06002"));
            lstFeed.Add(new RegionZipFeed("Hartford", "06006"));
            lstFeed.Add(new RegionZipFeed("Hartford", "06010"));
            lstFeed.Add(new RegionZipFeed("Hartford", "06013"));

            lstFeed.Add(new RegionZipFeed("New London", "06249"));
            lstFeed.Add(new RegionZipFeed("New London", "06254"));
            lstFeed.Add(new RegionZipFeed("New London", "06360"));

            lstFeed.Add(new RegionZipFeed("Fairfield", "06901"));
            lstFeed.Add(new RegionZipFeed("Fairfield", "06902"));
            lstFeed.Add(new RegionZipFeed("Fairfield", "06903"));
            lstFeed.Add(new RegionZipFeed("Fairfield", "06904"));
            lstFeed.Add(new RegionZipFeed("Fairfield", "06905"));

            //List<string> lstTempZip = new List<string>();
            Region tempRegion = new Region();
            int i = 0;
            //for each item in list that need to be fed into Region list
            foreach (RegionZipFeed thisFeed in lstFeed)
            {
                //if Region already in list, update and readd to list of regions
                if (lstRegions.Any(z => z.strRegion == thisFeed.strRegion))
                {
                    //store List object, update then place back into list
                    tempRegion = lstRegions.Where(d => d.strRegion == thisFeed.strRegion).First();
                    i = lstRegions.IndexOf(tempRegion);
                    tempRegion.lstZipCodes.Add(thisFeed.strZip);
                    lstRegions[i] = tempRegion;
                }
                //else add Region to list if it does not exist
                else
                {
                    //store feed information in temp object and then add to list
                    tempRegion = new Region();
                    tempRegion.strRegion = thisFeed.strRegion;
                    tempRegion.lstZipCodes.Add(thisFeed.strZip);
                    lstRegions.Add(tempRegion);
                }
            }

            //cycle through Regions and output name and list of associated Zip Codes
            foreach (Region thisRegion in lstRegions)
            {
                Console.Write(thisRegion.strRegion + Environment.NewLine + "___________________" + Environment.NewLine);
                foreach (string strZip in thisRegion.lstZipCodes)
                {
                    Console.Write(strZip + Environment.NewLine);
                }
                Console.Write(Environment.NewLine);
            }

        }
    }
}
