// 
// LogReportingStartup.cs
//  
// Author:
//       Alan McGovern <alan@xamarin.com>
// 
// Copyright (c) 2011, Xamarin Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Core.LogReporting;


namespace MonoDevelop.Ide
{
	public class LogReportingStartup : CommandHandler
	{
		protected override void Run ()
		{
//			var pid = Process.GetCurrentProcess ().Id;
//			var directory = new DirectoryInfo (LogReportingService.CrashLogDirectory);
//			
//			if (Platform.IsMac) {
//				var crashmonitor = Path.Combine (PropertyService.EntryAssemblyPath, "MonoDevelopLogAgent.app");
//				Process.Start (new ProcessStartInfo ("open", string.Format ("-a {0} -n --args -pid {1} -log {2} -session {3}", crashmonitor, pid, directory.FullName, SystemInformation.SessionUuid)) {
//					UseShellExecute = false,
//				});
//			}
			
			LogReportingService.ShouldEnableReporting = () => {
				var title = GettextCatalog.GetString ("A crash has just occurred");
				var part1 = GettextCatalog.GetString ("Details of this crash, along with anonymous installation " +
							"information, can be uploaded to Xamarin to help diagnose the issue. " +
						    "Do you wish to automatically upload this information for this and future crashes?");
				var part2 = GettextCatalog.GetString ("This setting can be changed in the 'Log Agent' section of the MonoDevelop preferences.");
				
				var result = MessageService.AskQuestion (
					title,
				    string.Format ("{0}{1}{1}{2}", part1, Environment.NewLine, part2),
					AlertButton.No, AlertButton.Yes);
				
				return result == AlertButton.Yes;
			};
			
			// Process cached crash reports if there are any and uploading is enabled
			LogReportingService.ProcessCache ();
		}
	}
}

