using System;
using Rhino;
using Rhino.Commands;

namespace MitsubaRender.Commands.About
{
    [System.Runtime.InteropServices.Guid("7bb46650-13ad-4430-b3a6-cf6c99cf12fd")]
    public class MitsubaAbout : Command
    {
        static MitsubaAbout _instance;
        public MitsubaAbout()
        {
            _instance = this;
        }

        ///<summary>The only instance of the MitsubaAbout command.</summary>
        public static MitsubaAbout Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "MitsubaAbout"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            AboutDialog dlg = new AboutDialog();
            dlg.Show();
            return Result.Success;
        }
    }
}
