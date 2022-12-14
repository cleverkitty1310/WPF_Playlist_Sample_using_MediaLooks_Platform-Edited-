#pragma checksum "..\..\..\..\CustomPropertyEditors\ShapeEditor.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8C87DDF8987C6C56029632004E544423CDCB593D8C4BC9D825247734EC7E633F"
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
    /// ShapeEditor
    /// </summary>
    public partial class ShapeEditor : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\..\CustomPropertyEditors\ShapeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.ComboBox ComboShapeType;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\CustomPropertyEditors\ShapeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.Spinner SpinnerRoundCorners;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\CustomPropertyEditors\ShapeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.Spinner SpinnerNSides;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\CustomPropertyEditors\ShapeEditor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.Spinner SpinnerRotate;
        
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
            System.Uri resourceLocater = new System.Uri("/CGEditor;component/custompropertyeditors/shapeeditor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\CustomPropertyEditors\ShapeEditor.xaml"
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
            this.ComboShapeType = ((Fluent.ComboBox)(target));
            
            #line 30 "..\..\..\..\CustomPropertyEditors\ShapeEditor.xaml"
            this.ComboShapeType.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboShapeType_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SpinnerRoundCorners = ((Fluent.Spinner)(target));
            
            #line 33 "..\..\..\..\CustomPropertyEditors\ShapeEditor.xaml"
            this.SpinnerRoundCorners.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SpinnerRoundCorners_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SpinnerNSides = ((Fluent.Spinner)(target));
            
            #line 38 "..\..\..\..\CustomPropertyEditors\ShapeEditor.xaml"
            this.SpinnerNSides.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SpinnerNSides_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SpinnerRotate = ((Fluent.Spinner)(target));
            
            #line 41 "..\..\..\..\CustomPropertyEditors\ShapeEditor.xaml"
            this.SpinnerRotate.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SpinnerRotate_ValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

