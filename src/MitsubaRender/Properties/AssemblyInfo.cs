using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Rhino.PlugIns;


// Plug-in Description Attributes - all of these are optional
// These will show in Rhino's option dialog, in the tab Plug-ins
[assembly: PlugInDescription(DescriptionType.Address, "Teixidores 1, Office 17. Premià de Dalt, Barcelona")]
[assembly: PlugInDescription(DescriptionType.Country, "Spain")]
[assembly: PlugInDescription(DescriptionType.Email, "info@tdmsolutions.com")]
[assembly: PlugInDescription(DescriptionType.Phone, "+34 937547774")]
[assembly: PlugInDescription(DescriptionType.Fax, "+34 937525215")]
[assembly: PlugInDescription(DescriptionType.Organization, "TDM Solutions SL")]
[assembly: PlugInDescription(DescriptionType.UpdateUrl, "http://v5.rhino3d.com/group/mitsuba-renderer")]
[assembly: PlugInDescription(DescriptionType.WebSite, "http://v5.rhino3d.com/group/mitsuba-renderer")]


// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Mitsuba Render")] // Plug-In title is extracted from this
[assembly: AssemblyDescription("Open Source Render Plugin for Rhino and RhinoGold")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("www.tdmsolutions.com")]
[assembly: AssemblyProduct("Mitsuba Render Integrator by TDM Solutions SL")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("24b6e560-183d-4898-bf4c-e1d721f46258")] // This will also be the Guid of the Rhino plug-in

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.2")]
[assembly: AssemblyFileVersion("1.0.0.2")]
