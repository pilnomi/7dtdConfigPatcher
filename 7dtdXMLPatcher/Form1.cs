using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using VDF;
using System.IO;
using System.Xml;
using HtmlAgilityPack;

namespace _7dtdXMLPatcher
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        BiomesXML bxml = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Resize += frmMain_Resize;
            lblSteamLocation.Text = SteamHelper.getDefaultSteamInstall();
            DirectoryInfo d = Directory.GetParent(SteamHelper.getDefaultSteamInstall());
            string configfilepath = d.FullName + "\\libraryfolders.vdf";

            VDF.SteamConfigFile configfile = new VDF.SteamConfigFile(configfilepath);
            string gamefolder = configfile.LibraryFolders[0];
            if (string.IsNullOrEmpty(gamefolder))
                throw new Exception("No steam library found!");

            gamefolder += @"\steamapps\common\7 Days To Die\Data\Config";
            lblGameFolder.Text = gamefolder;

            bxml = new BiomesXML(gamefolder + "\\biomes.xml");
            loadBiomesList(bxml.findBiomes().ToArray());
            BindingSource bs = new BindingSource();
            bs.DataSource = loadResourceTypes();

            dataGridView1.DataSource = bs;
            dataGridView1.AllowUserToAddRows = true;
            frmMain_Resize(null, EventArgs.Empty);
        }

        void frmMain_Resize(object sender, EventArgs e)
        {
            lbBiomesAffected.Height = this.Height - lbBiomesAffected.Top - 50;
            dataGridView1.Height = this.Height - dataGridView1.Top - 50;
            dataGridView1.Width = this.Width - dataGridView1.Left - 25;

        }

        List<resourceEntry> loadResourceTypes()
        {
            List<resourceEntry> myList = new List<resourceEntry>();
            if (File.Exists("preferredResources.xml"))
            {
                XmlDocument d = new XmlDocument();
                d.Load("preferredResources.xml");
                foreach (XmlNode n in d.ChildNodes[0])
                {
                    myList.Add(new resourceEntry(n.Attributes["blockname"].Value, n.Attributes["cluster"].Value, n.Attributes["prob"].Value,
                        n.Attributes["rwgGenerationType"].Value, bool.Parse(n.Attributes["CopyToAllBiomes"].Value), false));

                }
            }
            else
            {
                myList.Add(new resourceEntry("gravel", "1", ".8130", "all", true, false));
                myList.Add(new resourceEntry("ironOre", "1", ".5", "all", true, false));
                myList.Add(new resourceEntry("potassiumNitrate", "1", ".5", "all", true, false));
                myList.Add(new resourceEntry("leadOre", "1", ".5", "all", true, false));
                myList.Add(new resourceEntry("coalOre", "1", ".5", "all", true, false));
                myList.Add(new resourceEntry("silverOre", "1", ".0002", "all", false, false));
                myList.Add(new resourceEntry("goldOre", "1", ".0001", "all", false, false));
                myList.Add(new resourceEntry("diamondOre", "1", ".00005", "all", false, false));
                myList.Add(new resourceEntry("oilDeposit", "1", ".1130", "all", false, false));
            }
            return myList;
        }

        void saveResourceTypes()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement resources = doc.CreateElement("resources");
            
            List<resourceEntry> mylist = (List<resourceEntry>)((BindingSource)dataGridView1.DataSource).DataSource;
            foreach (resourceEntry r in mylist)
            {
                XmlElement resource = doc.CreateElement("resource");
                resource.SetAttribute("blockname", r.blockname);
                resource.SetAttribute("cluster", r.cluster);
                resource.SetAttribute("prob", r.prob);
                resource.SetAttribute("rwgGenerationType", r.rwgGenerationType);
                resource.SetAttribute("CopyToAllBiomes", r.CopyToAllBiomes.ToString());
                resources.AppendChild(resource);
            }
            doc.AppendChild(resources);
            doc.Save("preferredResources.xml");
        }

        public void loadBiomesList(string[] biomes)
        {
            for (int i=0; i<biomes.Count();i++)
                lbBiomesAffected.Items.Add(biomes[i], true);
        }

        private void btnUpdateBiomesXML_Click(object sender, EventArgs e)
        {
            List<string> selectedbiomes = new List<string>();
            foreach (string s in lbBiomesAffected.CheckedItems)
                selectedbiomes.Add(s);
            bxml.updateBiomesXML((List<resourceEntry>)dataGridView1.DataSource, selectedbiomes);
        }

        private void btnSaveAsDefault_Click(object sender, EventArgs e)
        {
            saveResourceTypes();
        }
    }

    public static class SteamHelper
    {
        public static string getDefaultSteamInstall()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey(@"Software\Valve\Steam");
            string installpath = "";
            if (regKey != null)
            {
                installpath = regKey.GetValue("SourceModInstallPath").ToString();
                
            }
            return installpath;
        }
    }

    public class resourceEntry
    {
        public string blockname { get; set; }
        public string cluster { get; set; }
        public string prob { get; set; }
        public string rwgGenerationType { get; set; }
        public bool CopyToAllBiomes { get; set; }
        //public bool AddToAllSubBiomes { get; set; }
        public resourceEntry(string blockname, string cluster, string prob, string rwgGenerationType, bool copytoallbiomes, bool addtoallsubbiomes)
        {
            this.blockname = blockname;
            this.cluster = cluster;
            this.prob = prob;
            this.rwgGenerationType = rwgGenerationType;
            this.CopyToAllBiomes = copytoallbiomes;
            //this.AddToAllSubBiomes = addtoallsubbiomes;
        }

        public resourceEntry()
        {
            blockname = "<enter blockname>";
            cluster = "1";
            prob = ".4";
            rwgGenerationType = "all";
            CopyToAllBiomes = false;
        }
    }

    public class BiomesXML
    {
        public string configFilePath { get; set; }
        public BiomesXML(string configFilePath)
        {
            this.configFilePath = configFilePath;
            
        
	    }

        public List<string> findBiomes()
        {
            List<string> biomelist = new List<string>();
            using (XmlReader reader = XmlReader.Create(configFilePath))
            {
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "biome":
                            biomelist.Add(reader["name"]);
                            break;
                        }
                    }
                }
            }
            return biomelist;

        }


        public void updateBiomesXML(List<resourceEntry> resources, List<string> selectedbiomes)
        {
            int updatedcount=0, addedcount = 0;

            HtmlAgilityPack.HtmlDocument d = new HtmlAgilityPack.HtmlDocument();
            d.Load(configFilePath);

            //HtmlNode biomes = d.DocumentNode.Element("biomes");
            foreach (HtmlNode worldgeneration in d.DocumentNode.ChildNodes)
            {
                if (worldgeneration.Name != "worldgeneration") continue;
                foreach (HtmlNode biomes in worldgeneration.ChildNodes)
                {
                    if (biomes.Name != "biomes") continue;
                    foreach (HtmlNode n in biomes.Elements("biome"))
                    {
                        string biomename = n.Attributes["name"].Value;
                        bool biomeselected = false;
                        foreach (string s in selectedbiomes)
                            if (biomename == s) { biomeselected = true; break; }
                        if (!biomeselected) continue;

                        foreach (HtmlNode layers in n.Elements("layers"))
                        {
                            foreach (HtmlNode layer in layers.Elements("layer"))
                            {
                                string blockname = layer.Attributes["blockname"].Value;
                                if (blockname == "stone")
                                {
                                    foreach (resourceEntry r in resources)
                                    {
                                        bool foundresource = false;
                                        foreach (HtmlNode h in layer.Elements("resource"))
                                        {
                                            if (r.blockname == h.Attributes["blockname"].Value)
                                            {
                                                h.Attributes["cluster"].Value = r.cluster;
                                                h.Attributes["prob"].Value = r.prob;
                                                h.Attributes["rwgGenerationType"].Value = r.rwgGenerationType;
                                                foundresource = true;
                                                updatedcount++;
                                                break;
                                            }
                                        }

                                        if (!foundresource && r.CopyToAllBiomes)
                                        {
                                            HtmlNode e = d.CreateElement("resource"); //new HtmlNode(HtmlNodeType.Element, d, addedcount);
                                            //e.Name = "resource";
                                            e.Attributes.Add("blockname", r.blockname);
                                            e.Attributes.Add("cluster", r.cluster);
                                            e.Attributes.Add("prob", r.prob);
                                            e.Attributes.Add("rwgGenerationType", r.rwgGenerationType);
                                            layer.ChildNodes.Add(e);
                                            addedcount++;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                
            }

            if (MessageBox.Show(String.Format("Updated: {0}, Added {1}\r\nSave biomes.xml?", updatedcount, addedcount), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                d.Save(configFilePath);
            }

        }
    }
}
