   M S B A M L   `   `   `  ����P  LAirlineReservationGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:AirlineReservationGUI.ViewsAirlineReservationGUI.Views  5   	   D    >AirlineReservationGUI.Views.ConfirmationPage_FinalConfirmation   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:AirlineReservationGUI.Views     5   	     � Title$(  "ConfirmationPage_FinalConfirmation��5   	   $	��450��64   $	��800��6A   .��5	      � ���#222840 � RowDefinitions 5
   
   �� 5      $
��1.5* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   � $��2��6    5      �� 5      $��* �6#   �� 5      $��4* �6#   .+�5      �� $��0��6   ._�5      �� -   5        $��18(�5   3   $��Back to Search��6A   $��60��6Z   
�������$��0��6}   ���#222840$��2q�6�   ���#C552EB � 	Resources 5        �� 
TargetType �� Property �� Value��%_   '��       �� 5!      " ��6    .�5"      ��  �@��CornerRadius$ 30��6=   5#      5$      5%      5&      �� 5(       �� CornerRadius$ 30��6   ���#131633$��10q�6<   $��1��6I   ._�5)      �� $
��Bold"�6   
�������$ ��Airline Reservation System��6=   $��30(�6b   $��Center=�6p   $��Center=�6�   $��2��6�   5*      5+      �� 5,   
   
�������$��Name on Ticket��6#   $��1��6<   $��20(�6I   $��Center=�6W   $��Center=�6t   �� 5-   
   
�������$��Seat Id��6#   $��2��65   $��20(�6B   $��Center=�6P   $��Center=�6m   �� 5.   
   
�������$��Departure Date��6#   $��3��6<   $��20(�6I   $��Center=�6W   $��Center=�6t   �� 5/   
   
�������$��Departure Location��6#   $��4��6@   $��20(�6M   $��Center=�6[   $��Center=�6x   �� 50   
   
�������$��Departure Time��6#   $��5��6<   $��20(�6I   $��Center=�6W   $��Center=�6t   �� 53   
   -   5         ��Name$ nameOnTicketTextBlock��53      
�������$��20(�6F   $��Center=�6T   $��Center=�6q   $��1��6�   $��1��6�   $��Name on ticket��6�   �� 54   
   -   5        $ seatIdTextBlock��54      
�������$��20(�6@   $��Center=�6N   $��Center=�6k   $��2��6�   $��1��6�   $��Seat Id��6�   �� 55   
   -   5        $ departureDateTextBlock��55      
�������$��20(�6G   $��Center=�6U   $��Center=�6r   $��3��6�   $��1��6�   $��Departure Date��6�   �� 57   
   -   5        $  departureLocationTextBlock��57      
�������$��20(�6K   $��Center=�6Y   $��Center=�6v   $��4��6�   $��1��6�   $��Departure Location��6�   �� 58   
   -   5        $ departureTimeTextBlock��58      
�������$��20(�6G   $��Center=�6U   $��Center=�6r   $��5��6�   $��1��6�   $��Departure Time��6�   �� 5:   
   -   5        ���#C552EB$��22(�5:   &   $��Confirm��6E   
�������$��6��6j   $��2��6w    5;      ��%_   '��       �� 5<      " ��6   .�5=      ��  �@��CornerRadius$ 25��65   5>      5?      5@      5A      5B      