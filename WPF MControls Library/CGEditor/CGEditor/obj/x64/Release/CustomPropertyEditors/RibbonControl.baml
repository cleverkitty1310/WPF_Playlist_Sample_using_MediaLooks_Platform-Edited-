   M S B A M L   `   `   `  ???? C  ?CGEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:CGEditor.CustomPropertyEditorsCGEditor.CustomPropertyEditors  M IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent 2    ,CGEditor.CustomPropertyEditors.RibbonControl   FCustomPropertyEditors,clr-namespace:CGEditor.CustomPropertyEditors  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  1fluent$clr-namespace:Fluent;assembly=Fluent  $??Stretch=?  ? 	Resources   ?? MergedDictionaries ?? Source??  ?? $J Dpack://application:,,,/CGEditor;component/Resources/VectorIcons.xaml??%    .??? .+?  Fluent.Ribbon  -     Name$ ribbon??$??12(?
???   ?
???????%   CanQuickAccessLocationChanging .     Menu   Fluent.Backstage     Content.    Fluent.BackstageTabControl  .@?  Fluent.Button  -     Name$ 
LoadConfig??   Header$ Load CG Config??  -   $ 
SaveConfig??$ Save CG Config??  Fluent.SeparatorTabItem    -   $
 Exit??	   QuickAccessItems	    Fluent.QuickAccessMenuItem  
 m? 	IsChecked
 .    Target.   -   $ BtnUndo??$
 Undo??   Icon$ ..\Images\undo.png??  
 . .   -   $ BtnRedo??$
 Redo??$ ..\Images\redo.png??   Tabs.   Fluent.RibbonTabItem     Header$ Common properties??   Groups.   Fluent.RibbonGroupBox  $
??Auto??   Header$ 
Visibility??.@?5	   /CGEditor.CustomPropertyEditors.VisibilityEditor	  -    	 Name$ BasicVisibilityEditor??  $ Position/Size??$
??Auto??.@?3
   -CGEditor.CustomPropertyEditors.PositionEditor
  -    
 Name$ BasicPositionEditor??  $ Alpha/BackColor??$
??Auto??.@?6   0CGEditor.CustomPropertyEditors.SimpleColorEditor  -	     Name$ BasicColorEditor??  $
 Misc??$
??Auto??.@?/   )CGEditor.CustomPropertyEditors.MiscEditor  -
     Name$ BasicMiscEditor??  $ Effects??.   $ Speed??$
??Auto??.@?0   *CGEditor.CustomPropertyEditors.SpeedEditor  -     Name$ EffectsSpeedEditor??  $ Shadow??$
??Auto??.@?1   +CGEditor.CustomPropertyEditors.ShadowEditor  -     Name$ EffectsShadowEditor??  $
 Blur??$
??Auto??.@?/   )CGEditor.CustomPropertyEditors.BlurEditor  -     Name$ EffectsBlurEditor??  $
 Glow??$
??Auto??.@?/   )CGEditor.CustomPropertyEditors.GlowEditor  -     Name$ EffectsGlowEditor??  -     Name$ TabText??$ Text Properties??   
IsSelected . .   $
 Text??.@?6   0CGEditor.CustomPropertyEditors.TextContentEditor  -     Name$ TextContEditor??$	??330??  $
 Text??$
??Auto??.@?? .+?3   -CGEditor.CustomPropertyEditors.GradientEditor  -     Name$ TextColorEditor??  $ Outline??$
??Auto??.@?? .+?7   1CGEditor.CustomPropertyEditors.OutlineColorEditor  -     Name$ TextOutlineEditor??  $
 Font??$
??Auto??.@?? .+?/   )CGEditor.CustomPropertyEditors.FontEditor  -     Name$ TextFontEditor??  -   $ TabGraphics??$ Graphics Properties??.   $ Shape??$
??Auto??.@?0   *CGEditor.CustomPropertyEditors.ShapeEditor  -     Name$ GraphicsShapeEditor??  $ Color??$
??Auto??.@?  -   $ GraphicsColorEditor??  $ Outline??$
??Auto??.@?  -   $ GraphicsOutlineEditor??  -   $ 	TabTicker??$ Ticker Properties?? . .   $ New content??$
??Auto??.@?8   2CGEditor.CustomPropertyEditors.TickerContentEditor  -      Name$  TickerNewContentEditor??$	??330??  $ Ticker properties??$
??Auto??.@?1   +CGEditor.CustomPropertyEditors.TickerEditor  -   !  Name$! TickerPropsEditor??  $
 Text??$
??Auto??.@?? .+?7   1CGEditor.CustomPropertyEditors.TextGradientEditor  -   "  Name$" TickerTextColorEditor??  $ Outline??$
??Auto??.@?? .+?;   5CGEditor.CustomPropertyEditors.TextOutlineColorEditor  -   #  Name$# TickerTextOutlineEditor??  $
 Font??$
??Auto??.@?? .+?  -   $ TickerTextFontEditor??  $ 
Shape type??$
??Auto??.@?  -   $ TickerShapeEditor??  $ Shape color??$
??Auto??.@?  -   $ TickerColorEditor??  $ Shape outline??$
??Auto??.@?  -    $ TickerOutlineEditor??  -!   $ TabImage??$ Image Properties??.   $ Image source??$
??Auto??.@?4   .CGEditor.CustomPropertyEditors.OpenImageEditor  -"   $  Name$$ ImageEditor??$	??330??  -#   $ TabFlash??$ Flash Properties??.   $ Image source??$
??Auto??.@?4   .CGEditor.CustomPropertyEditors.OpenFlashEditor  -$   %  Name$% FlashEditor??$	??330??  $ Actions??
???   ?.   -%   &  Name$& GroupActions??$ Grouping??$
??Auto??.@?  -&   $ BtnGroup??$ Group??'   	LargeIcon' ??  	  Group#??[  $??0q?  -'   $ 
BtnUnGroup??$ Ungroup??' ??   UnGroup#??[ $??0q?  -(   $& GroupZOrder??$ Z Order??$
??Auto??.@?  -)   $ BtnUp??%  Fluent.RibbonAttachedProperties(   RibbonSizeDefinition$( Large??$ Layer Up??' ??   LayerUp#??[ $??0q?  -*   $ BtnDown??$ 
Layer Down??$( Large??' ??   	LayerDown#??[ $??0q?  -+   $ 
BtnToFront??$( Large??$ Bring to Front??' ??   ToFront#??[ $??0q?  -,   $ 	BtnToBack??$( Large??$ Send to Back??' ??  
 ToBack#??[ $??0q?  --   $& 
GroupAlign??$ Align??$
??Auto??.@??? ) ?? Orientation$) 
Horizontal=?.+?  -.   $ 
AlignLefts??$( Large??$ Align Lefts??' ??   
AlignLefts#??[ $??0q?  -/   $ AlignCenters??$( Large??$ Align Centers??' ??   AlignCenters#??[ $??0q?  -0   $ AlignRights??$( Large??$ Align Rights??' ??   AlignRights#??[ $??0q??? $) 
Horizontal=?.+?  -1   $ 	AlignTops??$( Large??$ AlignL Tops??' ??  	 	AlignTops#??[	 $??0q?  -2   $ AlignMiddles??$( Large??$ Align Middles??' ??  
 AlignMiddles#??[
 $??0q?  -3   $ AlignBottoms??$( Large??$ Align Bottoms??' ??   AlignBottoms#??[ $??0q?