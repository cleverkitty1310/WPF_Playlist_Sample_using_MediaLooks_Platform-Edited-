   M S B A M L   `   `   `  ????  WPFMControls"    WPFMControls.MPreviewControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .??5      ?   ? RowDefinitions  5	   
   ?? 5
      $??* ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   .+?5   
   ? ??5      ?? 5       ?? ImageSource$ Resources/Tile.png??6    j? Stretch$
 Fill=?6>    j? Viewport$ 0, 0, 20, 20?6M    j? TileMode$
 Tile=?6e    j? ViewportUnits$ Absolute=?6u   .+?5      r? -   5        $??preview??5       r? Stretch$ Uniform=?6*    r? UnloadedBehavior$ Manual=?6<   $y?Hidden=?6V   5      ?? 5   
    ?? Orientation$ 
Horizontal=?6   $??1??6.   .+?5      ?? -   5        $??checkBoxVideo??5   @   $??Video Preview??6   	 d? 	IsChecked$
	 TrueY?6/   $??18??6?   $??5q?6?   ?? 5      -   5        $??checkBoxAudio??5   ;   $??Audio Preview??6   $??18??6/   $??5q?6?   5      ?? 5   
   $ 
Horizontal=?6   $??2??6.   .+?5      ?? -   5        $??label1??5   8   $??Audio Volume:??6   $??18??6,   
???????$??0q?6Y   $??5q?6e   ?? 5      -   5        $??trackBar??5   /   $??17??6   
 ? Maximum$	
 100Q?6!    ? Value$ 50Q?6?   $	??195??6J   $??5q?6z   5      5      5      