#pragma checksum "..\..\..\MPlaylistControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "03F0BA42D3BC27E55CB648242DDD0A044838B05957285391C60E5A61E3CF156F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace WPFMControls {
    
    
    /// <summary>
    /// MPlaylistControl
    /// </summary>
    public partial class MPlaylistControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\MPlaylistControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PlaylistTable;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\MPlaylistControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonAddFile;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\MPlaylistControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonRemove;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\MPlaylistControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonUp;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\MPlaylistControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonDown;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFMControls;component/mplaylistcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MPlaylistControl.xaml"
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
            this.PlaylistTable = ((System.Windows.Controls.DataGrid)(target));
            
            #line 18 "..\..\..\MPlaylistControl.xaml"
            this.PlaylistTable.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.PlaylistTable_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\MPlaylistControl.xaml"
            this.PlaylistTable.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PlaylistTable_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.buttonAddFile = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\MPlaylistControl.xaml"
            this.buttonAddFile.Click += new System.Windows.RoutedEventHandler(this.button1_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buttonRemove = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\MPlaylistControl.xaml"
            this.buttonRemove.Click += new System.Windows.RoutedEventHandler(this.buttonRemove_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.buttonUp = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\MPlaylistControl.xaml"
            this.buttonUp.Click += new System.Windows.RoutedEventHandler(this.buttonUp_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.buttonDown = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\MPlaylistControl.xaml"
            this.buttonDown.Click += new System.Windows.RoutedEventHandler(this.buttonDown_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

