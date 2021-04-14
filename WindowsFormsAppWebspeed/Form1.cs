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

namespace WindowsFormsAppWebspeed
{
    public partial class Form1 : Form
    {
        private string guestNumber,guestName,guestAddress,guestCountry;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            string urlString = "http://nlbavwsldev01/cgi-bin/cgiip.exe/WService=webspeedone/websample.p?guestnumber=" + guestNumber;
            XmlTextReader reader = new XmlTextReader(urlString);
            string CurrentElement =null;
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
                                    guestName = reader.Value;
                                    break;
                                case "address1":
                                    guestAddress = reader.Value;
                                    break;
                                case "country":
                                    guestCountry = reader.Value;
                                    break;                            
                                }
                            break;
                        }
            }
            }
            txtName.Text = guestName;
            txtAddress.Text = guestAddress;
            txtCountry.Text = guestCountry;
        }

        private void txtGuestNr_TextChanged(object sender, EventArgs e)
        {
            guestNumber = txtGuestNr.Text;
        }
    }
}
