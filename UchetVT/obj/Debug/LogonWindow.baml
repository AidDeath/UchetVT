   M S B A M L   `   `   `  ����B  >UchetVT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UchetVTUchetVT  5   	       UchetVT.LogonWindow   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   8x,http://schemas.microsoft.com/winfx/2006/xaml    5   	   >d2http://schemas.microsoft.com/expression/blend/2008    5   	   Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5   	   %localclr-namespace:UchetVT    5   	     � Title$   Вход в систему��5   	   $	��200��6    $��384.562��6-    � 
ResizeMode$ NoResize=�6=    � Icon$ Resources/rfid_chip.png��6S    � WindowStartupLocation$ CenterScreen=�6r   .��5
      � $��0,0,4.062,-0.001q�6    � ColumnDefinitions 5   
   �� 5      $	��81* �6   �� 5      $
��112* �6    � RowDefinitions 5   
   �� 5      $	��30* �6   �� 5      $	��50* �6   �� 5      $	��50* �6   �� 5      $	��30* �6   .+�5   
   �� $
��Wrap=�6   $:��4Учёт вычислительной техники��6*   $��0��6N   $��2��6[   $��Italic$�6p   $
��Bold"�6�   $��14(�6�   $��Center=�6�   $��Center=�6�   �� 5   
   $��1��6   $��1��6!   $%��Имя пользователя��61   �� 5   
   -   5        $��UsernameTextBox��5      $��1��6)   $��1��6:    �� TextWrapping$
 Wrap=�6G   $��Center=�6\   $��Center=�6~   $��Center=�6�   $	��140��6�   .��6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      .��5      5      �� 5   
   $��1��6   $��2��6%   $��Пароль��62   G� 5   
   -   5        $��UserPasswordBox��5      $��1��6-   $��2��6>   $��Center=�6L   $��Center=�6n   $��Center=�6�   $	��140��6�    5        5      5!      �� 5"   
   -   5        $��Вход��5"      $��1��6    $��3��62   $	��140��6[   $��25��6g    �� 	IsDefault . 6s   �� 5$   
   $��1��6   $��2��6   $��Resources/rfid_chip.png��6.   6Q   5'      5(      