﻿#pragma checksum "..\..\..\Views\AnnualConsumption.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3B9879240FB1CDFC759186847D9D3ED2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Client.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Client.Views {
    
    
    /// <summary>
    /// AnnualConsumption
    /// </summary>
    public partial class AnnualConsumption : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Views\AnnualConsumption.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TitleLabel;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Views\AnnualConsumption.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CityNameLabel;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Views\AnnualConsumption.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CityNameTxtBox;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Views\AnnualConsumption.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label YearLabel;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Views\AnnualConsumption.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YearTxtBox;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Views\AnnualConsumption.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GetConsumptionsBtn;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Views\AnnualConsumption.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ValidationMsg;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Views\AnnualConsumption.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ResultLabel;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Views\AnnualConsumption.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Res;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Client;component/views/annualconsumption.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\AnnualConsumption.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TitleLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.CityNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.CityNameTxtBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.YearLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.YearTxtBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.GetConsumptionsBtn = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Views\AnnualConsumption.xaml"
            this.GetConsumptionsBtn.Click += new System.Windows.RoutedEventHandler(this.GetConsumptionsBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ValidationMsg = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.ResultLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.Res = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
