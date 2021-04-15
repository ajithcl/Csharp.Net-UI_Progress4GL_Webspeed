using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using GuestLibrary;
using System.Configuration;
using System.Collections.Specialized;

/*
 * Read values from configuration file :
 * https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/store-custom-information-config-file
 * URI builder:
 * https://docs.microsoft.com/en-us/dotnet/api/system.uribuilder.query?view=net-5.0
 */

namespace WindowsFormsAppWebspeed
{
    public partial class Form1 : Form
    {
        private string guestNumber;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            //Clear existing data from UI
            ClearUi();

            guest objGuest=getGuestDetails(guestNumber);

            txtName.Text    = objGuest.guestName;
            txtAddress.Text = objGuest.guestAddress;
            txtCountry.Text = objGuest.guestCountry;
        }
        private guest getGuestDetails(string guestNr)
        {
            //Get base url from App.config file
            string baseUrl = ConfigurationManager.AppSettings.Get("baseurl");

            //Append query parameters to url
            UriBuilder urlString = new UriBuilder(baseUrl);

            if (urlString != null && urlString.Query.Length > 1)
                urlString.Query = urlString.Query.Substring(1) + "&" + "guestnumber=" + guestNr;
            else
                urlString.Query = "guestnumber=" + guestNr;

// string urlString = "http://nlbavwsldev01/cgi-bin/cgiip.exe/WService=webspeedone/websample.p?guestnumber=123" + guestNr;

            XmlTextReader reader = new XmlTextReader(urlString.ToString());
            string CurrentElement = null;
            guest objGuest = new guest();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        {
                            CurrentElement = reader.Name;
                            break;
                        }
                    case XmlNodeType.Text:
                        {
                            switch (CurrentElement)
                            {
                                case "NAME":
                                    objGuest.guestName = reader.Value;
                                    break;
                                case "address1":
                                    objGuest.guestAddress = reader.Value;
                                    break;
                                case "country":
                                    objGuest.guestCountry = reader.Value;
                                    break;
                            }
                            break;
                        }
                }
            }

            return objGuest;
        }
        private void txtGuestNr_TextChanged(object sender, EventArgs e)
        {
            guestNumber = txtGuestNr.Text;
        }
        private void ClearUi()
        {
            txtAddress.Text = "";
            txtCountry.Text = "";
            txtName.Text = "";
        }
    }
}
