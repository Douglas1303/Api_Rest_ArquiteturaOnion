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
    public class AddEventRsc {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AddEventRsc() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Poc.Domain.Resources.AddEventRsc", typeof(AddEventRsc).Assembly);
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
        ///   Looks up a localized string similar to Id da categoria não existe. .
        /// </summary>
        internal static string CategoryExists {
            get {
                return ResourceManager.GetString("CategoryExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CategoriaId precisa ser informado. .
        /// </summary>
        internal static string CategoryIdNullErrror {
            get {
                return ResourceManager.GetString("CategoryIdNullErrror", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Descrição não pode ser vazia. .
        /// </summary>
        internal static string DescriptionIsEmptyErrror {
            get {
                return ResourceManager.GetString("DescriptionIsEmptyErrror", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data final precisa ser informada. .
        /// </summary>
        internal static string EndDateIsEmptyErrror {
            get {
                return ResourceManager.GetString("EndDateIsEmptyErrror", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data inicial precisa ser informada. .
        /// </summary>
        internal static string InitialDateIsEmptyErrror {
            get {
                return ResourceManager.GetString("InitialDateIsEmptyErrror", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Id da categoria não existe. .
        /// </summary>
        internal static string String1 {
            get {
                return ResourceManager.GetString("String1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Titulo do evento já existe. .
        /// </summary>
        internal static string TitleExists {
            get {
                return ResourceManager.GetString("TitleExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Titulo não pode ser vazio. .
        /// </summary>
        internal static string TitleIsEmptyErrror {
            get {
                return ResourceManager.GetString("TitleIsEmptyErrror", resourceCulture);
            }
        }
    }
}
