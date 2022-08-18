﻿#pragma checksum "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DCFE7A5DA93817AEED0FFE6DE042771A578340964310F80102CC33596C5B104F"
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
    /// ShadowEditor
    /// </summary>
    public partial class ShadowEditor : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton ToggleShadow;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider SliderShadowAlpha;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.Spinner SpinnerOffsetX;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.Spinner SpinnerShadowBlur;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.ColorPicker CPShadowColor;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.Spinner SpinnerOffsetY;
        
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
            System.Uri resourceLocater = new System.Uri("/CGEditor;component/custompropertyeditors/shadoweditor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
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
            this.ToggleShadow = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 29 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
            this.ToggleShadow.Checked += new System.Windows.RoutedEventHandler(this.ToggleShadow_Checked);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
            this.ToggleShadow.Unchecked += new System.Windows.RoutedEventHandler(this.ToggleShadow_Unchecked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SliderShadowAlpha = ((System.Windows.Controls.Slider)(target));
            
            #line 47 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
            this.SliderShadowAlpha.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SliderShadowAlpha_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SpinnerOffsetX = ((Fluent.Spinner)(target));
            
            #line 51 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
            this.SpinnerOffsetX.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SpinnerOffsetX_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SpinnerShadowBlur = ((Fluent.Spinner)(target));
            
            #line 64 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
            this.SpinnerShadowBlur.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SpinnerShadowBlur_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CPShadowColor = ((Xceed.Wpf.Toolkit.ColorPicker)(target));
            
            #line 67 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
            this.CPShadowColor.SelectedColorChanged += new System.Windows.RoutedPropertyChangedEventHandler<System.Windows.Media.Color>(this.CPShadowColor_SelectedColorChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SpinnerOffsetY = ((Fluent.Spinner)(target));
            
            #line 70 "..\..\..\..\CustomPropertyEditors\ShadowEditor.xaml"
            this.SpinnerOffsetY.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SpinnerOffsetY_ValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
