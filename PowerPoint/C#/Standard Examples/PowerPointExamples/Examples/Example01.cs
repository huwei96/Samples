﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using ExampleBase;

using NetOffice;
using PowerPoint = NetOffice.PowerPointApi;
using NetOffice.PowerPointApi.Enums;
using NetOffice.OfficeApi.Enums;

namespace PowerPointExamplesCS4
{
    /// <summary>
    /// Example 1 - Create a presentation
    /// </summary>
    internal class Example01 : IExample
    {
        #region IExample

        public void RunExample()
        {
            // start powerpoint
            PowerPoint.Application powerApplication = new PowerPoint.Application();

            // add a new presentation with one new slide
            PowerPoint.Presentation presentation = powerApplication.Presentations.Add(MsoTriState.msoTrue);
            presentation.Slides.Add(1, PpSlideLayout.ppLayoutClipArtAndVerticalText);

            // save the document
            string fileExtension = GetDefaultExtension(powerApplication);
            string documentFile = string.Format("{0}\\Example01{1}", HostApplication.RootDirectory, fileExtension);
            presentation.SaveAs(documentFile);

            // close power point and dispose reference
            powerApplication.Quit();
            powerApplication.Dispose();

            // show dialog for the user(you!)
            HostApplication.ShowFinishDialog(null, documentFile);
        }

        public void Connect(IHost hostApplication)
        {
            HostApplication = hostApplication;
        }

        public string Caption
        {
            get { return HostApplication.LCID == 1033 ? "Example01" : "Beispiel01"; }
        }

        public string Description
        {
            get { return HostApplication.LCID == 1033 ? "Create a presentation with 1 empty slide" : "Eine neue Präsentation erstellen"; }
        }

        public UserControl Panel
        {
            get { return null; }
        }

        #endregion

        #region Properties

        internal IHost HostApplication { get; private set; }

        #endregion

        #region Helper

        /// <summary>
        /// returns the valid file extension for the instance. for example ".ppt" or ".pptx"
        /// </summary>
        /// <param name="application">the instance</param>
        /// <returns>the extension</returns>
        private static string GetDefaultExtension(PowerPoint.Application application)
        {
            double Version = Convert.ToDouble(application.Version, CultureInfo.InvariantCulture);
            if (Version >= 12.00)
                return ".pptx";
            else
                return ".ppt";
        }

        #endregion
    }
}
