﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace McNeight {
    using System;
    using System.Reflection;
    
    
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
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
#if NET20 || NET35 || NET40
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("McNeight.Strings", typeof(Strings).GetType().Assembly);
#else
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("McNeight.Strings", typeof(Strings).GetTypeInfo().Assembly);
#endif
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
        ///   Looks up a localized string similar to Decimal byte array constructor requires an array of length four containing valid decimal bytes..
        /// </summary>
        internal static string Arg_DecBitCtor {
            get {
                return ResourceManager.GetString("Arg_DecBitCtor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted to divide by zero..
        /// </summary>
        internal static string Arg_DivideByZero {
            get {
                return ResourceManager.GetString("Arg_DivideByZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Object must be of type Decimal..
        /// </summary>
        internal static string Arg_MustBeDecimal {
            get {
                return ResourceManager.GetString("Arg_MustBeDecimal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value &apos;{0}&apos; is not valid for this usage of the type {1}..
        /// </summary>
        internal static string Argument_InvalidEnumValue {
            get {
                return ResourceManager.GetString("Argument_InvalidEnumValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Argument must be between {0} and {1}..
        /// </summary>
        internal static string ArgumentOutOfRange_Bounds_Lower_Upper {
            get {
                return ResourceManager.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Decimal can only round to between 0 and 28 digits of precision..
        /// </summary>
        internal static string ArgumentOutOfRange_DecimalRound {
            get {
                return ResourceManager.GetString("ArgumentOutOfRange_DecimalRound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Decimal&apos;s scale value must be between 0 and 28, inclusive..
        /// </summary>
        internal static string ArgumentOutOfRange_DecimalScale {
            get {
                return ResourceManager.GetString("ArgumentOutOfRange_DecimalScale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Valid values are between {0} and {1}, inclusive..
        /// </summary>
        internal static string ArgumentOutOfRange_Range {
            get {
                return ResourceManager.GetString("ArgumentOutOfRange_Range", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rounding digits must be between 0 and {0}, inclusive..
        /// </summary>
        internal static string ArgumentOutOfRange_RoundingDigits {
            get {
                return ResourceManager.GetString("ArgumentOutOfRange_RoundingDigits", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Function does not accept decimal null values..
        /// </summary>
        internal static string Arithmetic_Null {
            get {
                return ResourceManager.GetString("Arithmetic_Null", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value was either too large or too small for a Decimal..
        /// </summary>
        internal static string Overflow_Decimal {
            get {
                return ResourceManager.GetString("Overflow_Decimal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Negating the minimum value of a twos complement number is invalid..
        /// </summary>
        internal static string Overflow_NegateTwosCompNum {
            get {
                return ResourceManager.GetString("Overflow_NegateTwosCompNum", resourceCulture);
            }
        }
    }
}
