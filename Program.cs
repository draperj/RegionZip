using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionZip
{
    public class Region
    {
        //Store Region name and list of associated Zip Codes
        public string strRegion;
        public List<ZipCodeGroup> lstZipCodes = new List<ZipCodeGroup>();

        public Region()
        {

        }

        public Region(string thisRegion, List<ZipCodeGroup> thisZipCodes)
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

    public class ZipCodeGroup
    {
        public string strStartZip;
        public string strEndZip;

        public ZipCodeGroup()
        {

        }

        public ZipCodeGroup(string thisStartZip, string thisEndZip)
        {
            this.strStartZip = thisStartZip;
            this.strEndZip = thisEndZip;
        }
    }
    public static class RegionList
    {
        public static void GetRegions(this List<Region> lstRegions, List<RegionZipFeed> lstFeed)
        {
            //Initialize required variables
            Region tempRegion = new Region();
            ZipCodeGroup tempZipGroup = new ZipCodeGroup();
            List<ZipCodeGroup> tempZipList = new List<ZipCodeGroup>();
            int intTempZip = 0;
            int intEndZip = 0;
            int i = 0;
            //for each item in list that need to be fed into Region list
            foreach (RegionZipFeed thisFeed in lstFeed)
            {
                //if Region already in list, update and readd to list of regions
                if (lstRegions.Any(z => z.strRegion == thisFeed.strRegion))
                {
                    //store List object, update then place back into list if ZIPs are valid numeric values
                    tempRegion = lstRegions.Where(d => d.strRegion == thisFeed.strRegion).First();
                    i = lstRegions.IndexOf(tempRegion);

                    //get last Zipcode on stored groups
                    ZipCodeGroup thisZipGroup = tempRegion.lstZipCodes.Last();

                    //esnure Zipcodes are of values that can be compared
                    if(Int32.TryParse(thisFeed.strZip, out intTempZip) && Int32.TryParse(thisZipGroup.strEndZip, out intEndZip))
                    {
                        //if the Zipcode is within the current group (increasing numerically by 1), add new end
                        if (intEndZip + 1 == intTempZip)
                        {
                            thisZipGroup.strEndZip = thisFeed.strZip;
                        }
                        //else create a new Zip group
                        else if (intEndZip + 1 < intTempZip)
                        {
                            tempZipGroup = new ZipCodeGroup();
                            tempZipGroup.strStartZip = thisFeed.strZip;
                            tempZipGroup.strEndZip = thisFeed.strZip;
                            tempRegion.lstZipCodes.Add(tempZipGroup);
                        }

                        //update Region in list
                        lstRegions[i] = tempRegion;
                    }
                   
                   
                }
                //else add Region to list if it does not exist
                else
                {
                    //store feed information in temp object and then add to list
                    tempRegion = new Region();
                    tempZipGroup = new ZipCodeGroup();
                    tempZipGroup.strStartZip = thisFeed.strZip;
                    tempZipGroup.strEndZip = thisFeed.strZip;
                    tempRegion.strRegion = thisFeed.strRegion;
                    tempRegion.lstZipCodes.Add(tempZipGroup);
                    lstRegions.Add(tempRegion);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Feed of data to be stored and related
            List<RegionZipFeed> lstFeed = new List<RegionZipFeed>();
            //Simulate unordered data feed list, not likely but still should be accounted for
            lstFeed.Add(new RegionZipFeed("Hartford", "06013"));
            lstFeed.Add(new RegionZipFeed("Hartford", "06001"));
            lstFeed.Add(new RegionZipFeed("Hartford", "06002"));
            lstFeed.Add(new RegionZipFeed("Hartford", "06003"));
            lstFeed.Add(new RegionZipFeed("Hartford", "06006"));
            lstFeed.Add(new RegionZipFeed("Hartford", "06007"));
            lstFeed.Add(new RegionZipFeed("Hartford", "06008"));
            lstFeed.Add(new RegionZipFeed("Hartford", "06010"));
            //Simulate bad data
           lstFeed.Add(new RegionZipFeed("Hartford", "060sadfas13"));

            lstFeed.Add(new RegionZipFeed("New London", "06249"));
            lstFeed.Add(new RegionZipFeed("New London", "06254"));
            lstFeed.Add(new RegionZipFeed("New London", "06360"));

            lstFeed.Add(new RegionZipFeed("Fairfield", "06901"));
            lstFeed.Add(new RegionZipFeed("Fairfield", "06902"));
            lstFeed.Add(new RegionZipFeed("Fairfield", "06903"));
            lstFeed.Add(new RegionZipFeed("Fairfield", "06904"));
            lstFeed.Add(new RegionZipFeed("Fairfield", "06905"));

            //Sort list to ensure proper comparisons can be used for grouping
            lstFeed.Sort((x, y) => string.Compare(x.strZip, y.strZip));

            List<Region> lstRegions = new List<Region>();
            //Feed in list to populate Regions list with Regiosn objects
            lstRegions.GetRegions(lstFeed);

            //cycle through Regions and output name and list of associated Zip Codes
            foreach (Region thisRegion in lstRegions)
            {
                Console.Write(thisRegion.strRegion + Environment.NewLine + "___________________" + Environment.NewLine);

                foreach (ZipCodeGroup thisZipGroup in thisRegion.lstZipCodes)
                {
                    Console.Write("Start ZIP " + thisZipGroup.strStartZip + " End ZIP " + thisZipGroup.strEndZip + Environment.NewLine);
                }
                Console.Write(Environment.NewLine);
            }

        }
    }
}
