﻿#pragma checksum "..\..\..\Window1 - Копировать.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6AA25DD51D4D44D4DEA5456E3E930E1388845C15"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace TPCR {
    
    
    /// <summary>
    /// Window1
    /// </summary>
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 39 "..\..\..\Window1 - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgMain;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Window1 - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgCustomers;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Window1 - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbCustomerName;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Window1 - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbCustomerEmail;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Window1 - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbCustomerPhone;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Window1 - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbCustomerAddress;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Window1 - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgOrders;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Window1 - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbOrderCustomerID;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Window1 - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgOrderItems;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\Window1 - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbPosType;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TPCR;V1.0.0.0;component/window1%20-%20%d0%9a%d0%be%d0%bf%d0%b8%d1%80%d0%be%d0%b2" +
                    "%d0%b0%d1%82%d1%8c.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Window1 - Копировать.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 4 "..\..\..\Window1 - Копировать.xaml"
            ((TPCR.Window1)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dgMain = ((System.Windows.Controls.DataGrid)(target));
            
            #line 39 "..\..\..\Window1 - Копировать.xaml"
            this.dgMain.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgMain_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dgCustomers = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.tbCustomerName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tbCustomerEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tbCustomerPhone = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.tbCustomerAddress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            
            #line 51 "..\..\..\Window1 - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bAddCustomer_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 52 "..\..\..\Window1 - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bDeleteCustomer_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.dgOrders = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 11:
            this.tbOrderCustomerID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            
            #line 58 "..\..\..\Window1 - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bAddOrder_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.dgOrderItems = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 14:
            
            #line 62 "..\..\..\Window1 - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bAddOrderItem_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 63 "..\..\..\Window1 - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bDeleteOrderItem_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 66 "..\..\..\Window1 - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bExit_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.cbPosType = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

