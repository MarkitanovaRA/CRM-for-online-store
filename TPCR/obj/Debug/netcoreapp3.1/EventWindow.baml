   M S B A M L   `   `   `  ����  TPCR    TPCR.EventWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  События��5   	   $	��450��6   $	��800��6&   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   &   System.Windows.Controls.Calendar  -   5          Name$ eventCalendar��5      $��0��6*   $��10,10,390,10q�67   
���������5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FFE4EAF0��6#   � 5      $ 	#FFECF0F4��6#    � Offset$
 0.16Q�65   � 5      $ 	#FFFCFCFD��6#   $
 0.16Q�65   � 5      $ 	#FFA2A2A2��6#   $ 1Q�65   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgEvents��5      $��1��6%   $��10q�62      AutoGenerateColumns$
 True��6>   �� 5   
   	 �� Orientation$	 
Horizontal=�6   $��2��6.   $��Right=�6;   $��10q�6W   �� 5   
   -   5        $#��Создать событие��5      
 J� RenderTransformOrigin$
 1.114,-4.904&�6F   $��345,156,0,0q�6k   $��2��6�   
���   �
�������$
��Left=�6�   $	��119��6�   $��32��6�   $	��Top=�6�   �� 5   
   -   5        $/��)Редактировать событие��5      $��485,155,0,0q�6J   $��2��6_   
�������
���   �$
��Left=�6�   $	��134��6�   $��33��6�   $	��Top=�6�   �� 5   
   -   5        $#��Удалить событие��5      $��637,156,0,0q�6+   $��2��6[   
�������
���   �$
��Left=�6�   $	��107��6�   $��32��6�   $	��Top=�6�   5      5       