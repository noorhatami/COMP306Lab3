﻿#pragma checksum "..\..\..\ParkingWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7FBC5544B8D62CAC64B1076E122590A26E0F7719"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ParkingClient;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace ParkingClient {
    
    
    /// <summary>
    /// ParkingWindow
    /// </summary>
    public partial class ParkingWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\ParkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_get;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\ParkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_post;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\ParkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_put;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\ParkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_delete;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\ParkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_patch;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\ParkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridDisplayParking;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\ParkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textLotID;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\ParkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_getByID;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ParkingClient;component/parkingwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ParkingWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Btn_get = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\ParkingWindow.xaml"
            this.Btn_get.Click += new System.Windows.RoutedEventHandler(this.Btn_get_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Btn_post = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\ParkingWindow.xaml"
            this.Btn_post.Click += new System.Windows.RoutedEventHandler(this.Btn_post_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Btn_put = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\ParkingWindow.xaml"
            this.Btn_put.Click += new System.Windows.RoutedEventHandler(this.Btn_put_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Btn_delete = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\ParkingWindow.xaml"
            this.Btn_delete.Click += new System.Windows.RoutedEventHandler(this.Btn_delete_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Btn_patch = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\ParkingWindow.xaml"
            this.Btn_patch.Click += new System.Windows.RoutedEventHandler(this.Btnpatch_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.gridDisplayParking = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.textLotID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.Btn_getByID = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\ParkingWindow.xaml"
            this.Btn_getByID.Click += new System.Windows.RoutedEventHandler(this.Btn_getByID_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
