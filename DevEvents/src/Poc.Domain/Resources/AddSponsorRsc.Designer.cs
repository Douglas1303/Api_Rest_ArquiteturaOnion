﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Poc.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AddSponsorRsc {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AddSponsorRsc() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Poc.Domain.Resources.AddSponsorRsc", typeof(AddSponsorRsc).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cep incorreto. .
        /// </summary>
        internal static string CepInvalidError {
            get {
                return ResourceManager.GetString("CepInvalidError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Documento não pode ser vazio. .
        /// </summary>
        internal static string DocumentEmptyError {
            get {
                return ResourceManager.GetString("DocumentEmptyError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Documento invalido. .
        /// </summary>
        internal static string DocumentInvalidError {
            get {
                return ResourceManager.GetString("DocumentInvalidError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tamanho do documento invalido..
        /// </summary>
        internal static string DocumentLengthError {
            get {
                return ResourceManager.GetString("DocumentLengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tipo de documento não existe. .
        /// </summary>
        internal static string DocumentNotExistsError {
            get {
                return ResourceManager.GetString("DocumentNotExistsError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nome do patrocinador já existe. .
        /// </summary>
        internal static string NamesSponsorExistsError {
            get {
                return ResourceManager.GetString("NamesSponsorExistsError", resourceCulture);
            }
        }
    }
}