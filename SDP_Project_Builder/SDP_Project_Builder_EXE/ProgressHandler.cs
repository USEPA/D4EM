// -----------------------------------------------------------------------
// <copyright file="ProgressHandler.cs" company="DotSpatial Team">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DemoMap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DotSpatial.Data;
    using System.ComponentModel.Composition;

    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IProgressHandler))]
    public class ProgressHandler : IProgressHandler
    {
        /// <summary>
        /// Progress is the method that should receive a progress message.
        /// </summary>
        /// <param name="key">The message string without any information about the status of completion.</param>
        /// <param name="percent">An integer from 0 to 100 that indicates the condition for a status bar etc.</param>
        /// <param name="message">A string containing both information on what the process is, as well as its completion status.</param>
        public void Progress(string key, int percent, string message)
        {

        }
    }
}
