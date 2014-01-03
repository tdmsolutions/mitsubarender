using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MitsubaRender.Commands.About
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void AboutDialog_Load(object sender, EventArgs e)
        {
            linkLabelMcNeel.Links.Add(new LinkLabel.Link {LinkData = "http://www.mcneel.com/"});
            linkLabelTDM.Links.Add(new LinkLabel.Link {LinkData = "http://www.tdmsolutions.com/"});
            linkLabelWenzel.Links.Add(new LinkLabel.Link {LinkData = "mailto:wenzel@cs.cornell.edu"});
            linkLabelGNU.Links.Add(new LinkLabel.Link {LinkData = "http://www.gnu.org/licenses/gpl-3.0.html"});
            linkLabelWeb.Links.Add(new LinkLabel.Link { LinkData = "http://www.mitsuba.cat" });
        }

        private void linkLabelMcNeel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e != null && e.Link != null) Process.Start(e.Link.LinkData.ToString());
        }

        private void linkLabelTDM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e != null && e.Link != null) Process.Start(e.Link.LinkData.ToString());
        }

        private void linkLabelWenzel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e != null && e.Link != null) Process.Start(e.Link.LinkData.ToString());
        }

        private void linkLabelGNU_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e != null && e.Link != null) Process.Start(e.Link.LinkData.ToString());
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}