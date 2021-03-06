﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace ClinicalInterview.Api.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class PatientIntakeFormTemplate : PatientIntakeFormTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"utf-8\" />\r\n    <title></title" +
                    ">\r\n    <style>\r\n        body {\r\n            font-family: Arial, Helvetica, sans-" +
                    "serif;\r\n            font-size: 12pt;\r\n        }\r\n        .page-header,\r\n        " +
                    ".page-header-space {\r\n            height: 100px;\r\n        }\r\n\r\n\r\n        .page-h" +
                    "eader {\r\n            position: fixed;\r\n            top: 0mm;\r\n            width:" +
                    " 100%;\r\n        }\r\n\r\n        table {\r\n            width: 100%;\r\n        }\r\n\r\n   " +
                    "     .table td {\r\n            padding-top: 10pt;\r\n            padding-right: 5pt" +
                    ";\r\n            padding-bottom: 3pt;\r\n            padding-left: 0;\r\n            v" +
                    "ertical-align: bottom;\r\n            border-bottom: solid black 1pt;\r\n           " +
                    " word-wrap: break-word;\r\n            overflow-wrap: break-word;\r\n        }\r\n\r\n  " +
                    "      .table td.header {\r\n            background-color: #D9D9D9 !important;\r\n   " +
                    "         padding: 10pt 5pt;\r\n            font-weight: bold;\r\n            border:" +
                    " none;\r\n            vertical-align: middle;\r\n        }\r\n\r\n        .table td.labe" +
                    "l {\r\n            font-weight: bold;\r\n            border: none;\r\n        }\r\n\r\n   " +
                    "     .square {\r\n           height: 18px;\r\n           width: 18px;\r\n           bo" +
                    "rder: solid #777777 2px;\r\n           background-color: transparent;\r\n           " +
                    "float: left;\r\n           margin-right: 10pt;\r\n        }\r\n\r\n        .square .squa" +
                    "re-checked {\r\n           height: 12px;\r\n           width: 12px;\r\n           back" +
                    "ground-color: #555555;\r\n           margin: 3px;\r\n        }\r\n\r\n        .square-la" +
                    "bel {\r\n            padding-top: 1pt;\r\n        }\r\n\r\n        @page {\r\n            " +
                    "margin: 20mm\r\n        }\r\n\r\n        @media print {\r\n            thead {\r\n        " +
                    "        display: table-header-group;\r\n            }\r\n\r\n            tfoot {\r\n    " +
                    "            display: table-footer-group;\r\n            }\r\n\r\n            button {\r" +
                    "\n                display: none;\r\n            }\r\n\r\n            body {\r\n          " +
                    "      margin: 0;\r\n                -webkit-print-color-adjust: exact;\r\n          " +
                    "  }\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"page-header\">\r\n  " +
                    "      <p style=\"text-align: center; font-size: 30pt; font-weight: bold;\">\r\n     " +
                    "       Patient Intake Form\r\n        </p>\r\n    </div>\r\n  \r\n    <table>\r\n        <" +
                    "thead>\r\n            <tr>\r\n                <td>\r\n                    <!--place ho" +
                    "lder for the fixed-position header-->\r\n                    <div class=\"page-head" +
                    "er-space\"></div>\r\n                </td>\r\n            </tr>\r\n        </thead>\r\n  " +
                    "      <tbody>\r\n            <tr>\r\n                <td>\r\n                    <div " +
                    "class=\"page\">\r\n                        <table class=\"table\">\r\n                  " +
                    "          <tbody>\r\n                                <tr>\r\n                       " +
                    "             <td class=\"header\" colspan=\"4\">\r\n                                  " +
                    "      Demographic\r\n                                    </td>\r\n                  " +
                    "              </tr>\r\n                                <tr>\r\n                     " +
                    "               <td style=\"width: 80px;\" class=\"label\">\r\n                        " +
                    "                Full name:\r\n                                    </td>\r\n         " +
                    "                           <td colspan=\"3\">\r\n                                   " +
                    "     ");
            
            #line 132 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValue("bio-fullname")));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""width: 80px;"" class=""label"">
                                        Address:
                                    </td>
                                    <td colspan=""3"">
                                        ");
            
            #line 140 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValue("bio-address")));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""width: 80px;"" class=""label"">
                                        City:
                                    </td>
                                    <td>
                                        ");
            
            #line 148 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValue("bio-city")));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                    <td style=""width: 50px; padding-left: 10pt;"" class=""label"">
                                        State:
                                    </td>
                                    <td>
                                        ");
            
            #line 154 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValue("bio-state")));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""width: 80px;"" class=""label"">
                                        Phone:
                                    </td>
                                    <td>
                                        ");
            
            #line 162 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValue("bio-phone")));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                    <td style=""width: 50px; padding-left: 10pt;"" class=""label"">
                                        DoB:
                                    </td>
                                    <td>
                                        ");
            
            #line 168 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValue("bio-dob")));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <br />
                        <br />
                        <table class=""table"">
                            <tbody>
                                <tr>
                                    <td class=""header"" colspan=""4"">
                                        Emergency Contact
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""width: 80px;"" class=""label"">
                                        Name:
                                    </td>
                                    <td colspan=""3"">
                                        ");
            
            #line 188 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValue("emergency-name")));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""width: 80px;"" class=""label"">
                                        Phone:
                                    </td>
                                    <td>
                                        ");
            
            #line 196 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValue("emergency-phone")));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                    <td style=""width: 100px; padding-left: 10pt;"" class=""label"">
                                        Relationship:
                                    </td>
                                    <td>
                                        ");
            
            #line 202 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValue("emergency-relationship")));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <br />
                        <br />
                        <table class=""table"">
                            <tbody>
                                <tr>
                                    <td class=""header"" colspan=""2"">
                                        Medical History
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""width: 80px;"" class=""label"">
                                        Medications:
                                    </td>
                                    <td>
                                        ");
            
            #line 222 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValue("medical-medications")));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <table class=""table"">
                            <tbody>
                                <tr>
                                    <td style=""width: 80px; padding-bottom: 5pt;"" class=""label"" colspan=""3"">
                                        Health Conditions:
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""border: none; width: 30%;"">
                                        <div class=""square"">
                                                ");
            
            #line 238 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((GetValue("health-headaches") == "true") ? "<div class='square-checked'></div>" : ""));
            
            #line default
            #line hidden
            this.Write(@"
                                            </div>
                                        <div class=""square-label"">Headaches</div>
                                    </td>
                                    <td style=""border: none; width: 30%;"">
                                        <div class=""square"">
                                            ");
            
            #line 244 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((GetValue("health-cancer") == "true") ? "<div class='square-checked'></div>" : ""));
            
            #line default
            #line hidden
            this.Write(@"
                                        </div>
                                        <div class=""square-label"">Cancer</div>
                                    </td>
                                    <td style=""border: none; width: 40%;"">
                                        <div class=""square"">
                                                ");
            
            #line 250 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((GetValue("health-heart") == "true") ? "<div class='square-checked'></div>" : ""));
            
            #line default
            #line hidden
            this.Write(@"
                                            </div>
                                        <div class=""square-label"">Heart / Circulation problem</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""border: none; width: 30%;"">
                                        <div class=""square"">
                                                ");
            
            #line 258 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((GetValue("health-numbness") == "true") ? "<div class='square-checked'></div>" : ""));
            
            #line default
            #line hidden
            this.Write(@"
                                            </div>
                                        <div class=""square-label"">Numbness</div>
                                    </td>
                                    <td style=""border: none; width: 30%;"">
                                        <div class=""square"">
                                                ");
            
            #line 264 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((GetValue("health-diabetes") == "true") ? "<div class='square-checked'></div>" : ""));
            
            #line default
            #line hidden
            this.Write(@"
                                            </div>
                                        <div class=""square-label"">Diabetes</div>
                                    </td>
                                    <td style=""border: none; width: 40%;"">
                                        <div class=""square"">
                                                ");
            
            #line 270 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((GetValue("health-pressure") == "true") ? "<div class='square-checked'></div>" : ""));
            
            #line default
            #line hidden
            this.Write(@"
                                            </div>
                                        <div class=""square-label"">High / Low blood pressure</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""border: none; width: 30%;"">
                                        <div class=""square"">
                                                ");
            
            #line 278 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((GetValue("health-allergy") == "true") ? "<div class='square-checked'></div>" : ""));
            
            #line default
            #line hidden
            this.Write(@"
                                            </div>
                                        <div class=""square-label"">Allergies</div>
                                    </td>
                                    <td style=""border: none; width: 70%;"" colspan=""2"">
                                        <div class=""square"">
                                                ");
            
            #line 284 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((GetValue("health-neckback") == "true") ? "<div class='square-checked'></div>" : ""));
            
            #line default
            #line hidden
            this.Write(@"
                                            </div>
                                        <div class=""square-label"">Neck / Back injuries</div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <table class=""table"">
                            <tbody>
                                <tr>
                                    <td style=""width: 140px;"" class=""label"">
                                        Conditions Details:
                                    </td>
                                    <td>
                                        ");
            
            #line 299 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValue("medical-details")));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <br />
                        <br />
                        <table class=""table"">
                            <tbody>
                                <tr>
                                    <td class=""header"">
                                        Reason of Appointment
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        ");
            
            #line 316 "C:\Src\NC\speech-clinical-interview\src\ClinicalInterview.Api\Templates\PatientIntakeFormTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetValue("reason-appointment")));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class PatientIntakeFormTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
