   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.customerWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  customerWindow��5   	   $	��650��6    $	��950��6-   .��5
      � ���	#FFE5E5E5$��0,0,0,-6q�6"   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ productsDatagrid��5      $��10,76,499,147q�6-   �� 5   
   -   5         ��Name$ productsCombo��5       �� DisplayMemberPath$
 name��6*   $
��Left=�6D   $��116,508,0,0q�6_   $	��Top=�6t   $	��162��6�   $��32��6�   �� 5   
   $��Customer��6   $
��Left=�6#   $��
26,548,0,0q�6>   $	��Top=�6R   $��74��6j   �� 5   
   $��Product��6   $
��Left=�6"   $��
26,512,0,0q�6=   $	��Top=�6Q   $��74��6i   �� 5   
   -   5        $��18(�5   &   $��	Make sale��64   $
��Left=�6H   $��299,508,0,0q�6c   $	��Top=�6x   $	��100��6�   $	��152��6�   �� 5   
   -   5         ��Name$ quantityText��5      $
��Left=�6(   $��116,582,0,0q�6C    �� TextWrapping$
 Wrap=�6X   $�� ��6l   $	��Top=�6t   $	��162��6�   $��24��6�   �� 5   
   $��Quantity��6   $
��Left=�6#   $��
26,582,0,0q�6>   $	��Top=�6R   $��74��6j   �� 5   
   -   5        $ 
usersCombo��5      $ username��6'   $
��Left=�6D   $��116,546,0,0q�6_   $	��Top=�6t   $	��162��6�   $��28��6�     5   
   -   5        $ salesDataGrid��5      $��475,76,10,142q�6*   �� 5   
   -   5        $ salesSearch��5      $
��Left=�6M   $��
613,42,0,0q�6h   $
 Wrap=�6|   $�� ��6�   $	��Top=�6�   $	��277��6�   $��28��6�   �� 5   
   -   5        $ productsSearch��5      $
��Left=�6S   $��
148,42,0,0q�6n   $
 Wrap=�6�   $�� ��6�   $	��Top=�6�   $	��303��6�   $��28��6�   �� 5   
   -	   5        $ productsFilterCombo��5      $
��Left=�60   $��	10,42,0,0q�6K   $	��Top=�6^   $	��133��6v   $��28��6�   .@�5      �� .��6   Name6!   �� 5      .��6   Price6"   �� 5      .��6   
Quantity6%   �� 5      .��6   
Category6%   5      �� 5   
   -
   5        $ SalesFilterCombo��5      $
��Left=�6-   $��
475,40,0,0q�6H   $	��Top=�6\   $	��133��6t   $��30��6�   .@�5      �� $��Customer��6   �� 5      $��Product��6   5       �� 5!   
   -   5         ��Name$ 	logoutBtn��5!   "   $��Logout��6   $
��Left=�6M   $��
862,10,0,0q�6h   $	��Top=�6|   $��66��6�   5#      5$      