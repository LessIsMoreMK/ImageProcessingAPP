   M S B A M L   `   `   `  ����M  IImageProcessingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ImageProcessingAppImageProcessingApp  5   	   #    ImageProcessingApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ImageProcessingApp     5   	     � Title$  
MainWindow��5   	   $	��400��6   $
��1000��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      $��25 �6   �� 5      $��25 �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      �� 5      .+�5   
   � $��0��6   $��0��6   $��3��6,    5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5!      �� $��0��6   $��0��6%   $
��Open��62   $��5q�6A   ��6L   �� *LoadImageCommand+�� 5"      $��0��6   $��1��6%   $��Open Default��62   $��5q�6I   ��6T   �� *LoadDefaultImageCommand+�� 5#      $��0��6   $��2��6%   $
��Save��62   $��5q�6A   ��6L   �� *SaveImageCommand+�� 5$      $��0��6   $��3��6%   $��Convert RGB Values��62   $��5q�6O   ��6Z   �� *ConvertRGBCommand+�� 5%      $��0��6   $��4��6%   $��Convert RGB Values Async��62   $��5q�6U   ��6`   �� *ConvertRGBAsyncCommand+�� 5&      $��0��6   $��5��6%   $��	Grayscale��62   $��5q�6F   ��6Q   �� *GrayscaleCommand+�� 5'      $��0��6   $��6��6%   $��Grayscale Async��62   $��5q�6L   ��6W   �� *GrayscaleAsyncCommand+�� 5(      $��0��6   $��7��6%   $��Grayscale Cpp��62   $��5q�6J   ��6U   �� *GrayscaleCppCommand+5)      �� 5+   
   $��0��6   $��1��6   $��2q�5,      
�������$
��Auto��5-      $
��Auto��6   $��5q�6,   ._�5.      �� -   5         ��Name$ imageBefore��5.      $��Stretch=�50      $��Stretch=�51      $
��Fill=�52      ��5/      �� *	ImagePath+52      53      �� 54   
   $��0��6   $��2��6   $��2q�55      
�������$
��Auto��57      $
��Auto��58      $��5q�6   ._�59      �� -   5        $ 
imageAfter��59      $��Stretch=�5;      $��Stretch=�5<      $
��Fill=�5=      ��5:      �� *ImageResult+5=      5>      �� 5@   
   -   5         ��Name$ 	timeLabel��5C      $��1��5@      $��1��6   $��2��6-   $��12(�5A      ��5B      �� *Time+5C      �� 5D   
   -   5        $ 
timeLabel2��5G      $��2��5D      $��1��6   $��2��6-   $��12(�5E      $�� ��5F      5G      5H      5I      