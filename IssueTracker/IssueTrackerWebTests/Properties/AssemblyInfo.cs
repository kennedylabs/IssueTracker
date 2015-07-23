using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("IssueTrackerWeb")]
[assembly: AssemblyDescription("Issue Tracker Web Application")]
#if DEBUG
[assembly: AssemblyConfiguration("DEBUG")]
#elif RELEASE
[assembly: AssemblyConfiguration("RELEASE")]
#endif
[assembly: AssemblyCompany("Kevin Kennedy")]
[assembly: AssemblyProduct("IssueTracker")]
[assembly: AssemblyCopyright("Copyright © 2015")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
