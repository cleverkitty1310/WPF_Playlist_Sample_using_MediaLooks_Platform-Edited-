   M S B A M L   `   `   `  ????M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  5       CGEditor0   *CGEditor.CustomPropertyEditors.ShapeEditor   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      1Fluent$clr-namespace:Fluent;assembly=Fluent  5        ? 	Resources  5       ?? MergedDictionaries ?? Source?? 5   
    5      ?? 5      $J Dpack://application:,,,/CGEditor;component/Resources/VectorIcons.xaml??6%   %    5      .??5      ? $??Stretch=?6    ? ColumnDefinitions 5   
   ?? 5      $
??Auto ?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   .+?5   
   ??  ?? Orientation$ 
Horizontal=?6   $??0??6.   $??0??6>   $??0,4,0,0q?6K   .+?5      ??    Hex#??[  6   $??22??63   $??1q?6?   $??Center=?6K   $??2q?6f    ? ToolTip$ 
Shape type??6q      Fluent.ComboBox  5      -   5          Name$ ComboShapeType??5       J? 	AllowDrop .  $??-8,2,2,2q?6J   $??Center=?6\   $??90??6w   $??90??6?   	 ?? 
IsEditable	 .  6?   ?? 5         CornerRadius#??[ 6   $??22??6<   $??25??6H   $??1,4,1,1q?6S   $??Center=?6e   $??6,2,2,2q?6?   $ Gradient type??6?      Fluent.Spinner  5!      -   5        
  Name$
 SpinnerRoundCorners??5!      $??Center=?6:   $??	-11,2,2,2q?6W      Maximum$	 100Q?6j      Format$  ??6x   $??93??6?   $??93??6?   6?   5"      ?? 5$   
   $ 
Horizontal=?6   $??0??6.   $??1??6>   $??0,4,0,0q?6K   .+?5%      ??  
 NSides#??[ 6   $??22??66   $??1,3,1,1q?6B   $??Center=?6T   $??2q?6o   $ Number of sides??6z   $??25??6?     5&      -   5        $
 SpinnerNSides??5&      $??Center=?64   $??	-11,2,2,2q?6Q   $ 20Q?6d   $  ??6q   $??93??6{   $??93??6?   {?.  6?   ?? 5(       	 Angle#??[ 6   $??22??65   $??1,3,1,1q?6A   $??Center=?6S   $??6,2,2,2q?6n   $ Number of sides??6   $??25??6?     5)      -   5        $
 SpinnerRotate??5)      $??Center=?64   $??	-11,2,2,2q?6Q   $	 360Q?6d   $  ??6r   $??93??6|   $??93??6?   6?   5*      5,      5.      