   M S B A M L   `   `   `  ????M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  5       CGEditor5   /CGEditor.CustomPropertyEditors.VisibilityEditor   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      1Fluent$clr-namespace:Fluent;assembly=Fluent  5        ? 	Resources  5       ?? MergedDictionaries ?? Source?? 5   
    5      ?? 5      $J Dpack://application:,,,/CGEditor;component/Resources/VectorIcons.xaml??6%   %    5      .??5      ? $??Stretch=?6    ? ColumnDefinitions 5   
   ?? 5      $
??Auto ?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   .+?5   
   ??  ?? Orientation$ 
Horizontal=?6   $??0??6.   $??0??6>   $??0,4,0,0q?6K   .+?5      d? -   5         d?Name$ ToggleVisible??5      $??0q?62   $??50??6G   $??50??6R   $??4q?6^   $??5q?6i   |?.  
??????  ? ToolTip$ 
Visibility??6?    d? 	IsChecked$
 TrueY?6?   ??5      ?? 5      	 ?? 
TargetType"	 d?6&   .R?5       ?? 
 ?? CornerRadius$
 3??6!   #??z??62   ._?5!      ?? $??1q?6/   $??Center=?6:   $??Center=?6W   5"       ?? Triggers 5#      P? 5$       P? Property ?@d?	IsChecked P? Value$
 TrueY?6;   . ?5%   "   ??    	EyeOpened ?? Value# [  6<    ?? Property ? ??5&   #   5'      P? 5(       ?@d?	IsChecked$ FalseY?6;   . ?5)   "   ??   	EyeClosed# [ 6<    ? ??5*   #   5+      5-      5/      5E      5F      5H      