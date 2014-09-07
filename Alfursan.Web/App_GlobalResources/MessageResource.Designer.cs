﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
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
    public class MessageResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MessageResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Alfursan.Web.App_GlobalResources.MessageResource", typeof(MessageResource).Assembly);
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
        ///   Looks up a localized string similar to  This Username or E-mail Used. Please Change Username or E-mail and Try Again.
        /// </summary>
        public static string Error_ExistingUserNameOrEmail {
            get {
                return ResourceManager.GetString("Error_ExistingUserNameOrEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Undefined Key Resource Use.
        /// </summary>
        public static string Error_InvalidResourceKey {
            get {
                return ResourceManager.GetString("Error_InvalidResourceKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid E-mail or Password.
        /// </summary>
        public static string Error_InvalidUserNameOrPass {
            get {
                return ResourceManager.GetString("Error_InvalidUserNameOrPass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Model is not valid.
        /// </summary>
        public static string Error_ModelNotValid {
            get {
                return ResourceManager.GetString("Error_ModelNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Process Could Not Be Saved..
        /// </summary>
        public static string Error_SqlException {
            get {
                return ResourceManager.GetString("Error_SqlException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An Unexpected Error Has Occurred..
        /// </summary>
        public static string Error_Unexpected {
            get {
                return ResourceManager.GetString("Error_Unexpected", resourceCulture);
            }
        }
 /// <summary>
///   Looks up a localized string similar to Şifre değiştirmek için doğrulama kodunuz mail adresinize gönderilmiştir..
        /// </summary>
        public static string Info_ChangePass {
            get {
                return ResourceManager.GetString("Info_ChangePass", resourceCulture);
            }
        }
               
        
        /// <summary>
        ///   ///   Looks up a localized string similar to Delete User.

        /// </summary>
        public static string Info_DeleteUser {
            get {
                return ResourceManager.GetString("Info_DeleteUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Create.
        /// </summary>
        public static string Info_SetUser {
            get {
                return ResourceManager.GetString("Info_SetUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your Profile Updated.
        /// </summary>
        public static string Info_UpdateProfile {
            get {
                return ResourceManager.GetString("Info_UpdateProfile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are You Sure To Delete This File?.
        /// </summary>
        public static string Warning_DeleteFile {
            get {
                return ResourceManager.GetString("Warning_DeleteFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are You Sure To Delete This User?.
        /// </summary>
        public static string Warning_DeleteUser {
            get {
                return ResourceManager.GetString("Warning_DeleteUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You Can&apos;t Edit The Admin Profile..
        /// </summary>
        public static string Warning_NotUpdateAdminProfile {
            get {
                return ResourceManager.GetString("Warning_NotUpdateAdminProfile", resourceCulture);
            }
        }
    }
}
