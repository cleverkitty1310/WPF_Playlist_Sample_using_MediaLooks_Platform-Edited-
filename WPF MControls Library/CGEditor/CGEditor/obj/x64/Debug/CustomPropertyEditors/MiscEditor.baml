   M S B A M L   `   `   `  ????M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent  5       CGEditor/   )CGEditor.CustomPropertyEditors.MiscEditor   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      1Fluent$clr-namespace:Fluent;assembly=Fluent  5        ? 	Resources  5       ?? MergedDictionaries ?? Source?? 5   
    5      ?? 5      $J Dpack://application:,,,/CGEditor;component/Resources/VectorIcons.xaml??6%   %    5      .??5      ? $??Stretch=?6    ? ColumnDefinitions 5   
   ?? 5      $
??Auto ?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   .+?5   
   ??  ?? Orientation$ 
Horizontal=?6   $??0??6.   $??0??6>   $??0,4,0,0q?6K   .+?5      ??    Arc#??[  6   $??22??63   $??22??6?   $??1q?6J   $??Center=?6V   $??2q?6q    ? ToolTip$ Edges smooth factor??6|      Fluent.Spinner  5      -   5          Name$ SpinnerEdgeSmooth??5      $??Center=?68   $??	-11,2,2,2q?6U      Maximum$	 100Q?6h   	   Format$	  ??6v   $??65??6?   $??65??6?   6?   ?? 5        	 Scale#??[ 6   $??22??65   $??22??6A   $??1,4,1,1q?6L   $??Center=?6^   $??6,2,2,2q?6y   $ 
Scale type??6?      Fluent.ComboBox  5!      -   5        
  Name$
 
ComboScale??5!       J? 	AllowDrop .  $??-8,2,2,2q?6F   $??Center=?6X   $??90??6s   $??90??6?    ?? 
IsEditable .  6?   ?? 5#        	Interlace#??[ 6   $??22??69   $??22??6E   $??1,4,1,1q?6P   $??Center=?6b   $??6,2,2,2q?6}   $ Interlace type??6?     5$      -   5        $
 ComboInterlace??5$       .  $??-8,2,2,2q?6J   $??Center=?6\   $??90??6w   $??90??6?    .  6?   5%      ?? 5'   
   $ 
Horizontal=?6   $??0??6.   $??1??6>   $??0,4,0,0q?6K   .+?5(      ??   	PointSize#??[ 6   $??22??69   $??22??6E   $??1,1,1,1q?6P   $??Center=?6b   $??2q?6}   $ Pixel aspect ratio??6?     5)      -   5        $ SpinnerPixelAR??5)      $??Center=?65   $??	-11,2,2,2q?6R   $ 10Q?6e   $??65??6r   $??65??6}   6?   ?? 5+       	 Align#??[ 6   $??22??65   $??22??6A   $??1,3,1,1q?6L   $??Center=?6^   $??6,2,2,2q?6y   $ Align??6?     5,      -   5        $
 
ComboAlign??5,       .  $??-8,2,2,2q?6F   $??Center=?6X   $??90??6s   $??90??6?    .  6?   ?? 5.        PlayMode#??[ 6   $??22??68   $??22??6D   $??1,5,1,1q?6O   $??Center=?6a   $??6,2,2,2q?6|   $ 	Play mode??6?     5/      -   5        $
 ComboPlayMode??5/       .  $??-8,2,2,2q?6I   $??Center=?6[   $??90??6v   $??90??6?    .  6?   50      52      54      