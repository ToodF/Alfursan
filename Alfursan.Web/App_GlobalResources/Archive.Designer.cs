﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alfursan.Resx {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Archive {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Archive() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Alfursan.Web.App_GlobalResources.Archive", typeof(Archive).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Created By.
        /// </summary>
        public static string CreatedBy {
            get {
                return ResourceManager.GetString("CreatedBy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Created Date.
        /// </summary>
        public static string CreatedDate {
            get {
                return ResourceManager.GetString("CreatedDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Customer.
        /// </summary>
        public static string CustomerName {
            get {
                return ResourceManager.GetString("CustomerName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File Name.
        /// </summary>
        public static string FileName {
            get {
                return ResourceManager.GetString("FileName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File Type.
        /// </summary>
        public static string FileType {
            get {
                return ResourceManager.GetString("FileType", resourceCulture);
            }
        }
    }
}
