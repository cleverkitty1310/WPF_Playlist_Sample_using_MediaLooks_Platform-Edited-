   M S B A M L   `   `   `  ????C  ?CGEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:CGEditor.CustomPropertyEditorsCGEditor.CustomPropertyEditors  5      M IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent 5      2    ,CGEditor.CustomPropertyEditors.RibbonControl   5      FCustomPropertyEditors,clr-namespace:CGEditor.CustomPropertyEditors  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      1fluent$clr-namespace:Fluent;assembly=Fluent  5      $??Stretch=?5        ? 	Resources  5       ?? MergedDictionaries ?? Source?? 5   
    5      ?? 5      $J Dpack://application:,,,/CGEditor;component/Resources/VectorIcons.xaml??6%   %    5      .??5      ? .+?5   
     Fluent.Ribbon  -   5          Name$ ribbon??5      $??12(?6(   
???   ?
???????%   CanQuickAccessLocationChanging .     Menu 5        Fluent.Backstage  5         Content. 5         Fluent.BackstageTabControl  .@?5        Fluent.Button  -   5          Name$ 
LoadConfig??5   (      Header$ Load CG Config??6<   6T     5      -   5        $ 
SaveConfig??5   (   $ Save CG Config??6<   6T     Fluent.SeparatorTabItem  5        5      -   5        $
 Exit??5   (   66   5      5       	   QuickAccessItems	 5$         Fluent.QuickAccessMenuItem  5%      
 m? 	IsChecked
 .    Target. 5&        -   5        $ BtnUndo??5&   $   $
 Undo??65      Icon$ ..\Images\undo.png??6C   6]   5'        5(      
 . . 5)        -   5        $ BtnRedo??5)   $   $
 Redo??65   $ ..\Images\redo.png??6C   6]   5*         Tabs. 5/        Fluent.RibbonTabItem     Header$ Common properties??6#      Groups. 50        Fluent.RibbonGroupBox  $
??Auto??6(      Header$ 
Visibility??65   .@?51      5	   /CGEditor.CustomPropertyEditors.VisibilityEditor	  -   5         	 Name$ BasicVisibilityEditor??51   =   52        53      $ Position/Size??6(   $
??Auto??6?   .@?54      3
   -CGEditor.CustomPropertyEditors.PositionEditor
  -   5         
 Name$ BasicPositionEditor??54   ;   55        56      $ Alpha/BackColor??6)   $
??Auto??6C   .@?57      6   0CGEditor.CustomPropertyEditors.SimpleColorEditor  -	   5          Name$ BasicColorEditor??57   >   58        5:      $
 Misc??6)   $
??Auto??68   .@?5;      /   )CGEditor.CustomPropertyEditors.MiscEditor  -
   5          Name$ BasicMiscEditor??5;   7   5<      5=        5?      $ Effects??6#   . 5@        $ Speed??6)   $
??Auto??69   .@?5A      0   *CGEditor.CustomPropertyEditors.SpeedEditor  -   5          Name$ EffectsSpeedEditor??5A   8   5B        5C      $ Shadow??6)   $
??Auto??6:   .@?5D      1   +CGEditor.CustomPropertyEditors.ShadowEditor  -   5          Name$ EffectsShadowEditor??5D   9   5E        5F      $
 Blur??6)   $
??Auto??68   .@?5G      /   )CGEditor.CustomPropertyEditors.BlurEditor  -   5          Name$ EffectsBlurEditor??5G   7   5H        5I      $
 Glow??6)   $
??Auto??68   .@?5J      /   )CGEditor.CustomPropertyEditors.GlowEditor  -   5          Name$ EffectsGlowEditor??5J   7   5K      5L        5N      -   5          Name$ TabText??5N   #   $ Text Properties??64      
IsSelected . . 5O        $
 Text??6(   .@?5P      6   0CGEditor.CustomPropertyEditors.TextContentEditor  -   5          Name$ TextContEditor??5P   >   $	??330??6V   5Q        5R      $
 Text??6(   $
??Auto??66   .@?5S      ? .+?5T      3   -CGEditor.CustomPropertyEditors.GradientEditor  -   5          Name$ TextColorEditor??5T   ?   5U      5V        5W      $ Outline??6(   $
??Auto??69   .@?5X      ? .+?5Y      7   1CGEditor.CustomPropertyEditors.OutlineColorEditor  -   5          Name$ TextOutlineEditor??5Y   C   5Z      5[        5\      $
 Font??6)   $
??Auto??67   .@?5]      ? .+?5^      /   )CGEditor.CustomPropertyEditors.FontEditor  -   5          Name$ TextFontEditor??5^   ;   5_      5`      5a        5c      -   5        $ TabGraphics??5c   #   $ Graphics Properties??68   . 5d        $ Shape??6(   $
??Auto??67   .@?5e      0   *CGEditor.CustomPropertyEditors.ShapeEditor  -   5          Name$ GraphicsShapeEditor??5e   8   5f        5g      $ Color??6(   $
??Auto??67   .@?5h        -   5        $ GraphicsColorEditor??5h   ;   5i        5j      $ Outline??6(   $
??Auto??69   .@?5k        -   5        $ GraphicsOutlineEditor??5k   ?   5l      5m        5o      -   5        $ 	TabTicker??5o   #   $ Ticker Properties??66    . . 5p        $ New content??6(   $
??Auto??6=   .@?5q      8   2CGEditor.CustomPropertyEditors.TickerContentEditor  -   5           Name$  TickerNewContentEditor??5q   @   $	??330??6`   5r        5s      $ Ticker properties??6(   $
??Auto??6C   .@?5t      1   +CGEditor.CustomPropertyEditors.TickerEditor  -   5        !  Name$! TickerPropsEditor??5t   9   5u        5v      $
 Text??6(   $
??Auto??66   .@?5w      ? .+?5x      7   1CGEditor.CustomPropertyEditors.TextGradientEditor  -   5        "  Name$" TickerTextColorEditor??5x   ?   5y      5z        5{      $ Outline??6(   $
??Auto??69   .@?5|      ? .+?5}      ;   5CGEditor.CustomPropertyEditors.TextOutlineColorEditor  -   5        #  Name$# TickerTextOutlineEditor??5}   G   5~      5        5?      $
 Font??6)   $
??Auto??67   .@?5?      ? .+?5?        -   5        $ TickerTextFontEditor??5?   ;   5?      5?        5?      $ 
Shape type??6(   $
??Auto??6<   .@?5?        -   5        $ TickerShapeEditor??5?   9   5?        5?      $ Shape color??6(   $
??Auto??6=   .@?5?        -   5        $ TickerColorEditor??5?   ;   5?        5?      $ Shape outline??6(   $
??Auto??6?   .@?5?        -    5        $ TickerOutlineEditor??5?   ?   5?      5?        5?      -!   5        $ TabImage??5?   #   $ Image Properties??65   . 5?        $ Image source??6(   $
??Auto??6>   .@?5?      4   .CGEditor.CustomPropertyEditors.OpenImageEditor  -"   5        $  Name$$ ImageEditor??5?   <   $	??330??6Q   5?      5?        5?      -#   5        $ TabFlash??5?   #   $ Flash Properties??65   . 5?        $ Image source??6(   $
??Auto??6>   .@?5?      4   .CGEditor.CustomPropertyEditors.OpenFlashEditor  -$   5        %  Name$% FlashEditor??5?   <   $	??330??6Q   5?      5?        5?      $ Actions??6#   
???   ?. 5?        -%   5        &  Name$& GroupActions??5?   (   $ Grouping??6>   $
??Auto??6P   .@?5?        -&   5        $ BtnGroup??5?   $   $ Group??66   '   	LargeIcon' 5?      ?? 5?       	  Group#??[  6$   $??0q?6E   5?        5?      -'   5        $ 
BtnUnGroup??5?   $   $ Ungroup??68   ' 5?      ?? 5?        UnGroup#??[ 6$   $??0q?6G   5?      5?        5?      -(   5        $& GroupZOrder??5?   (   $ Z Order??6=   $
??Auto??6N   .@?5?        -)   5        $ BtnUp??5?   $   %  Fluent.RibbonAttachedProperties(   RibbonSizeDefinition$( Large??63   $ Layer Up??6p   ' 5?      ?? 5?        LayerUp#??[ 6$   $??0q?6G   5?        5?      -*   5        $ BtnDown??5?   $   $ 
Layer Down??65   $( Large??6I   ' 5?      ?? 5?        	LayerDown#??[ 6$   $??0q?6I   5?        5?      -+   5        $ 
BtnToFront??5?   $   $( Large??68   $ Bring to Front??6u   ' 5?      ?? 5?        ToFront#??[ 6$   $??0q?6G   5?        5?      -,   5        $ 	BtnToBack??5?   $   $( Large??67   $ Send to Back??6t   ' 5?      ?? 5?       
 ToBack#??[ 6$   $??0q?6F   5?      5?        5?      --   5        $& 
GroupAlign??5?   (   $ Align??6<   $
??Auto??6K   .@?5?      ?? ) ?? Orientation$) 
Horizontal=?6!   .+?5?        -.   5        $ 
AlignLefts??5?   (   $( Large??6<   $ Align Lefts??6z   ' 5?      ?? 5?   "     
AlignLefts#??[ 6(   $??0q?6N   5?        5?      -/   5        $ AlignCenters??5?   (   $( Large??6>   $ Align Centers??6{   ' 5?      ?? 5?   "     AlignCenters#??[ 6(   $??0q?6P   5?        5?      -0   5        $ AlignRights??5?   (   $( Large??6=   $ Align Rights??6z   ' 5?      ?? 5?   "     AlignRights#??[ 6(   $??0q?6O   5?      5?      ?? 5?      $) 
Horizontal=?6!   .+?5?        -1   5        $ 	AlignTops??5?   (   $( Large??6;   $ AlignL Tops??6x   ' 5?      ?? 5?   "    	 	AlignTops#??[	 6(   $??0q?6M   5?        5?      -2   5        $ AlignMiddles??5?   (   $( Large??6>   $ Align Middles??6{   ' 5?      ?? 5?   "    
 AlignMiddles#??[
 6(   $??0q?6P   5?        5?      -3   5        $ AlignBottoms??5?   (   $( Large??6>   $ Align Bottoms??6|   ' 5?      ?? 5?   "     AlignBottoms#??[ 6(   $??0q?6P   5?      5?      5?      5?      5?      5?      5?      