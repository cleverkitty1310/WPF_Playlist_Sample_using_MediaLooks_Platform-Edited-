   M S B A M L   `   `   `  ????M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  5       CGEditor3   -CGEditor.CustomPropertyEditors.PositionEditor   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      1Fluent$clr-namespace:Fluent;assembly=Fluent  5        ? 	Resources  5       ?? MergedDictionaries ?? Source?? 5   
    5      ?? 5      $J Dpack://application:,,,/CGEditor;component/Resources/VectorIcons.xaml??6%   %    5      .??5      ? $??Stretch=?6    ? ColumnDefinitions 5   
   ?? 5      $
??Auto ?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   .+?5   
   ??  ?? Orientation$ 
Horizontal=?6   $??0??6.   $??0??6>   $??0,4,0,0q?6K   .+?5      ??    X#??[  6   $??22??61   $??22??6=   $??2q?6H   $??2q?6S   $??Center=?6_    ? ToolTip$ 
X Position??6{      Fluent.Spinner  5      -   5          Name$ SpinnerXPos??5      $??Center=?62   $??	-11,2,2,2q?6O      Minimum$ -20000Q?6b   	   Maximum$	 20000Q?6t   
   Format$
  ??6?   $??93??6?   $??93??6?   6?   ?? 5        	 Width#??[ 6   $??22??65   $??25??6A   $??1,8,1,1q?6L   $??Center=?6^   $??6,2,2,2q?6y   $ Width??6?     5!      -   5        $ SpinnerWidth??5!      $??Center=?63   $??	-11,2,2,2q?6P   $ -20000Q?6c   $	 20000Q?6u   $
  ??6?   $??93??6?   $??93??6?   6?   5"      ?? 5$   
   $ 
Horizontal=?6   $??0??6.   $??1??6>   $??0,4,0,0q?6K   .+?5%      ??   Y#??[ 6   $??22??61   $??22??6=   $??2q?6H   $??2q?6S   $??Center=?6_   $ 
Y Position??6{     5&      -   5        $ SpinnerYPos??5&      $??Center=?62   $??	-11,2,2,2q?6O   $ -20000Q?6b   $	 20000Q?6t   $
  ??6?   $??93??6?   $??93??6?   6?   ?? 5(       
 Height#??[ 6   $??22??66   $??1,8,1,1q?6B   $??Center=?6T   $??6,2,2,2q?6o   $ Height??6?   $??25??6?     5)      -   5        $ SpinnerHeight??5)      $??Center=?64   $??	-11,2,2,2q?6Q   $ -20000Q?6d   $	 20000Q?6v   $
  ??6?   $??93??6?   $??93??6?   6?   5*      5,      5.      