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
        ///   Looks up a localized string similar to Tanımlamak istediğiniz email yada username başka bir kayıt için kullanılmış. Lütfen değişiklik yaparak kayıt işlemine devam ediniz..
        /// </summary>
        public static string Error_ExistingUserNameOrEmail {
            get {
                return ResourceManager.GetString("Error_ExistingUserNameOrEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tanımsız resource key kullanımı.
        /// </summary>
        public static string Error_InvalidResourceKey {
            get {
                return ResourceManager.GetString("Error_InvalidResourceKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Geçersiz Email yada pass..
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
        ///   Looks up a localized string similar to İşleminiz kaydedilemedi..
        /// </summary>
        public static string Error_SqlException {
            get {
                return ResourceManager.GetString("Error_SqlException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Beklenmeyen bir hata oluştu..
        /// </summary>
        public static string Error_Unexpected {
            get {
                return ResourceManager.GetString("Error_Unexpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        public static string Info_DeleteUser {
            get {
                return ResourceManager.GetString("Info_DeleteUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User save..
        /// </summary>
        public static string Info_SetUser {
            get {
                return ResourceManager.GetString("Info_SetUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Profile update edilmiştir..
        /// </summary>
        public static string Info_UpdateProfile {
            get {
                return ResourceManager.GetString("Info_UpdateProfile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to dosyasını silmek istediğinize emin misiniz?.
        /// </summary>
        public static string Warning_DeleteFile {
            get {
                return ResourceManager.GetString("Warning_DeleteFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to kullanıcısını silmek istediğinize emin misiniz?.
        /// </summary>
        public static string Warning_DeleteUser {
            get {
                return ResourceManager.GetString("Warning_DeleteUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Admin profili düzenlenemez..
        /// </summary>
        public static string Warning_NotUpdateAdminProfile {
            get {
                return ResourceManager.GetString("Warning_NotUpdateAdminProfile", resourceCulture);
            }
        }
    }
}
