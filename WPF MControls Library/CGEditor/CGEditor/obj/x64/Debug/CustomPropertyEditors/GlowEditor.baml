   M S B A M L   `   `   `  ????M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  5       CGEditor/   )CGEditor.CustomPropertyEditors.GlowEditor   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      1Fluent$clr-namespace:Fluent;assembly=Fluent  5        ? 	Resources  5       ?? MergedDictionaries ?? Source?? 5   
    5      ?? 5      $J Dpack://application:,,,/CGEditor;component/Resources/VectorIcons.xaml??6%   %    5      .??5      ? $??Stretch=?6    ? ColumnDefinitions 5   
   ?? 5      $
??Auto ?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   .+?5   
   ??  ?? Orientation$ 
Horizontal=?6   $??0??6.   $??0??6>   $??0,4,0,0q?6K   .+?5      d? -   5         d?Name$ 
ToggleGlow??5         Glow#??[  6/   $??78??6P   $??2,2,2,2q?6\   $??4,6,4,4q?6m   |?.   ? ToolTip$ Enadle glow effect??6?   $??22??6?   $??22??6?   6?   ?? 5       	 Width#??[ 6   $??22??65   $??22??6A   $??1,8,1,1q?6L   $??Center=?6^   $??5,2,2,2q?6y   $ Glow X size??6?      Fluent.Spinner  5      -   5          Name$ SpinnerGlowSizeX??5      $??Center=?67   $??	-11,2,2,2q?6T   	   Maximum$		 100Q?6g   $??65??6u   $??65??6?   
   Format$
  ??6?   6?   5       ?? 5"   
   $ 
Horizontal=?6   $??0??6.   $??1??6>   $??0,4,0,0q?6K   .+?5#      ??   
FourArrows#??[ 6   $??22??6<   $??22??6H   $??2q?6S   $??1q?6^   $??Center=?6j   $ 
Glow value??6?   $??0q?6?     5$      -   5        $ SpinnerGlowVlue??5$      $??Center=?66   $??	-11,2,2,2q?6S   $		 100Q?6f   $??65??6t   $??65??6   $
  ??6?   6?   ?? 5&       
 Height#??[ 6   $??22??66   $??22??6B   $??1,8,1,1q?6M   $??Center=?6_   $??5,2,2,2q?6z   $ Glow Y size??6?     5'      -   5        $ SpinnerGlowSizeY??5'      $??Center=?67   $??	-11,2,2,2q?6T   $		 100Q?6g   $??65??6u   $??65??6?   $
  ??6?   6?   5(      5*      5,      