   M S B A M L   `   `   `  ????G  CWPFMControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:WPFMControls;assembly=WPFMControlsWPFMControls  5   	    WPF Playlist Sample"   WPFPlaylistSample.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ?my0clr-namespace:WPFMControls;assembly=WPFMControls     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5	   	   -   5          ? Icon$  Resources\Logo.ico??5   	    ? Title$ Channel Studio??5   	   $
??1366??5
   	   $	??768??5   	    ? WindowStartupLocation$ CenterScreen=?5   	   $??0q?5   	   .??5      ? ???	#FF2D2D30 ? ColumnDefinitions 5   
   ?? 5      $??2* ?6   ?? 5      $??* ?6   .+?5   
   ? $??1??6    ? RowDefinitions 5      ?? 5      $
??auto ?6    ?? 5      $??* ?6    ?? 5      $
??auto ?6    ?? 5      $
??auto ?6    .+?5      ? $??0 5 0 0q?6    5      ?? 5      $
??auto ?6'   ?? 5      $??* ?6'   ?? 5      $
??auto ?6'   .+?5!      "   WPFMControls.MPersistControl  -   5        $??MPersistControl1??5!   %   $??21??6=   $	??120??6I   $
??Left=?5"   %   $	??5 0q?6@   $??0??5#   %   %   WPFMControls.PlaylistBackground  5$      -   5        $??PlaylistBackground1??5$   (   $	??5 0q?5%   (   $??1??5&   (   ?? 5'      -   5        $??checkBoxListSeek??5(      $??Playlist Seek??5'      $??16??63    d? 	IsChecked$
 TrueY?5(   T   $	??5 0q?5)      $??2??5*      5+      #   WPFMControls.MPlaylistControl  5,      -   5        $??MPlaylistControl1??5,   "   $??1??5.   "    ? DataContext 5-   "   ?? 5.   "   ? 5/      $??2??6    50      ?? 51      $
??auto ?6'   ?? 52      $??* ?6'   .+?54         WPFMControls.MFileState  -   5        $??MFileState1??54   ;   $
??Left=?6    $	??214??6N   $	??5 0q?6Z   "   WPFMControls.MFormatsControl  55      -   5        $??MFormatsControl1??55   %   $??1??6=   $	??5 0q?6M   56      ? 57      $??3??6   .+?58          WPFMControls.MRendererList  -   5        $??MRendererList1??58   #   $	??5 5q?69   59      5;      ? 5=   
   $??0??6    5>      ?? 5?      $
??auto ?6    ?? 5@      $??* ?6    ?? 5A      $
??auto ?6    ?? 5B      $
??auto ?6    .+?5D         WPFMControls.MFileSeeking  -	   5        $??MFileSeeking1??5D      $??5q?63   ? 5E      $??1??6   .+?5F      "   WPFMControls.MPreviewControl  -
   5        $??MPreviewControl1??5F   %   5G      ? 5H      $??2??6   .+?5I      #	   WPFMControls.MFileRateControl	  -   5        $??MFileRateControl1??5I   &   5J      ? 5K      $??3??6    5L      ?? 5M      $
??auto ?6'   ?? 5N      $
??auto ?6'   ?? 5O      $
??auto ?6'   .+?5R      ?? -   5        $??buttonCG_Props??5R   >   $??2??6   $??
CG Setting??6)   $	??100??6T   $??Normal&?6}   $??5q?6?      FullBorderButtonStyle#??[  6?   5S      ?? 5T       ?? Orientation$ 
Horizontal=?6   $??3??5U      $??Right=?6&   .+?5V      ?? -   5        $
??Play??5V      $??50??6(   $??Normal&?63   $??5q?6H   $??5q?6S   #??[  6_   5W      ?? 5X      -   5        $
??Stop??5X      $??50??6(   $??Normal&?63   $??5q?6H   $??5q?6S   #??[  6_   5Y      ?? 5[      -   5        $??Load Sub Timeline??5[      $	??125??65   $??Normal&?6A   $??5q?6V   $??5q?6a     NormalButtonStyle#??[ 6m   5\      ?? 5]      -   5        $??Combine as Timeline??5]      $	??125??67   $??Normal&?6C   $??5q?6X   $??5q?6c   #??[ 6o   5^      ?? 5_      -   5        $??Load as Timeline??5_      $	??125??64   $??Normal&?6@   $??5q?6U   $??5q?6`   #??[ 6l   5`      5a      5b      5c      5d      