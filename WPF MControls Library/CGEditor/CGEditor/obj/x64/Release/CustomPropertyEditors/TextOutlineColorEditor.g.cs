﻿#pragma checksum "..\..\..\..\CustomPropertyEditors\TextOutlineColorEditor.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0AAF762F50E6FE0F3823E43D8391D6B84648F21BB5CA34B08E169B90F47787B9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Fluent;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace CGEditor.CustomPropertyEditors {
    
    
    /// <summary>
    /// TextOutlineColorEditor
    /// </summary>
    public partial class TextOutlineColorEditor : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\..\CustomPropertyEditors\TextOutlineColorEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackColors;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\CustomPropertyEditors\TextOutlineColorEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.ColorPicker CPColor0;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\CustomPropertyEditors\TextOutlineColorEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonAddGradient;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\CustomPropertyEditors\TextOutlineColorEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.ComboBox ComboGradientType;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\CustomPropertyEditors\TextOutlineColorEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.Spinner SpinnerOutlineWidth;
        
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
            System.Uri resourceLocater = new System.Uri("/CGEditor;component/custompropertyeditors/textoutlinecoloreditor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\CustomPropertyEditors\TextOutlineColorEditor.xaml"
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
            this.StackColors = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.CPColor0 = ((Xceed.Wpf.Toolkit.ColorPicker)(target));
            
            #line 32 "..\..\..\..\CustomPropertyEditors\TextOutlineColorEditor.xaml"
            this.CPColor0.SelectedColorChanged += new System.Windows.RoutedPropertyChangedEventHandler<System.Windows.Media.Color>(this.SelectedColorChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buttonAddGradient = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\..\CustomPropertyEditors\TextOutlineColorEditor.xaml"
            this.buttonAddGradient.Click += new System.Windows.RoutedEventHandler(this.buttonAddGradient_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ComboGradientType = ((Fluent.ComboBox)(target));
            
            #line 39 "..\..\..\..\CustomPropertyEditors\TextOutlineColorEditor.xaml"
            this.ComboGradientType.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboGradientType_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SpinnerOutlineWidth = ((Fluent.Spinner)(target));
            
            #line 41 "..\..\..\..\CustomPropertyEditors\TextOutlineColorEditor.xaml"
            this.SpinnerOutlineWidth.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SpinnerGradientAngle_ValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
