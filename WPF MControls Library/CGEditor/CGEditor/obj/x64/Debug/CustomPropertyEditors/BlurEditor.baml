   M S B A M L   `   `   `  ????M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  5       CGEditor/   )CGEditor.CustomPropertyEditors.BlurEditor   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      1Fluent$clr-namespace:Fluent;assembly=Fluent  5        ? 	Resources  5       ?? MergedDictionaries ?? Source?? 5   
    5      ?? 5      $J Dpack://application:,,,/CGEditor;component/Resources/VectorIcons.xaml??6%   %    5      .??5      ? $??Stretch=?6    ? ColumnDefinitions 5   
   ?? 5      $
??Auto ?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   .+?5   
   ??  ?? Orientation$ 
Horizontal=?6   $??0??6.   $??0??6>   $??0,4,0,0q?6K   .+?5      d? -   5         d?Name$ 
ToggleBlur??5         Blur#??[  6/   $??78??6P   $??2,2,2,2q?6\   $??4,6,4,4q?6m   |?.   ? ToolTip$ Enadle blur effect??6?   $??22??6?   $??22??6?   6?   ?? 5       	 Width#??[ 6   $??22??65   $??22??6A   $??1,8,1,1q?6L   $??Center=?6^   $??5,2,2,2q?6y   $ Blur X size??6?      Fluent.Spinner  5      -   5          Name$ SpinnerBlurSizeX??5      $??Center=?67   $??	-11,2,2,2q?6T   	   Maximum$		 100Q?6g   $??65??6u   $??65??6?   
   Format$
  ??6?   6?   5       ?? 5"   
   $ 
Horizontal=?6   $??0??6.   $??1??6>   $??0,4,0,0q?6K   .+?5#      ??  	 Align#??[ 6   $??22??67   $??22??6C   $??2q?6N   $??1q?6Y   $??Center=?6e   $ 
Blur align??6?   $??0q?6?      Fluent.ComboBox  5$      -   5          Name$ ComboBlurAlign??5$       J? 	AllowDrop .  $??-8,2,2,2q?6J   $??Center=?6\   $??62??6w   $??62??6?    ?? 
IsEditable .  6?   ?? 5&       
 Height#??[ 6   $??22??66   $??22??6B   $??1,8,1,1q?6M   $??Center=?6_   $??5,2,2,2q?6z   $ Blur Y size??6?     5'      -   5        $ SpinnerBlurSizeY??5'      $??Center=?67   $??	-11,2,2,2q?6T   $		 100Q?6g   $??65??6u   $??65??6?   $
  ??6?   6?   5(      5*      5,      