   M S B A M L   `   `   `  ????M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  5       CGEditor7   1CGEditor.CustomPropertyEditors.OutlineColorEditor   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      1Fluent$clr-namespace:Fluent;assembly=Fluent  5        ? 	Resources  5       ?? MergedDictionaries ?? Source?? 5   
    5      ?? 5      $J Dpack://application:,,,/CGEditor;component/Resources/VectorIcons.xaml??6%   %    5      .??5      ? $??Stretch=?6    ? ColumnDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??* ?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   .+?5   
   ??    Palette#??[  6   $??22??63   $??1q?6?   $??2,4,2,2q?6K    ? ToolTip$ Color??6\   $??Center=?6l   ?? 5   
    ?? Orientation$ 
Horizontal=?6   $??1??6.   $??0??6>   $??0,4,0,0q?6K   .+?5      ?? -   5        $??StackColors??5   2   $ 
Horizontal=?6   .+?5       #  Xceed.Wpf.Toolkit.ColorPicker  -   5          Name$ CPColor0??5    #   $??36??66   $??2q?6A      ShowDisableButton .  6f   5!      ?? 5"      -   5        	 ??Name$	 buttonAddGradient??5"   !   $??+??6   $??Center=?6<   $??20??6W   $??2q?6b   |?.  6?   5#      ?? 5%   
   $ 
Horizontal=?6   $??0??6.   $??1??6>   $??2??6K   .+?5&      ??   Gradient#??[ 6   $??22??68   $??1q?6D   $??Center=?6P   $??2q?6k   $ Gradient type??6v      Fluent.ComboBox  5'      -   5        
  Name$
 ComboGradientType??5'       J? 	AllowDrop .  $??-8,2,2,2q?6M   $??Center=?6c   $	??105??6~   $	??105??6?    ?? 
IsEditable .  6?   ?? 5(        	LineWidth#??[ 6   $??22??69   $??22??6E   $??10,2,2,2q?6P   $??1,1,1,0q?6b   $ Outline width??6t      Fluent.Spinner  5)      -   5          Name$ SpinnerOutlineWidth??5)      $??Center=?6:   $??	-10,5,2,2q?6W      Maximum$	 100Q?6j      Format$  ??6x   $??65??6?   $??65??6?   6?   5*      5,      5.      