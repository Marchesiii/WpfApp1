   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.TelaPessoa   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  
TelaPessoa��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   �� $��Codigo��6   $
��Left=�6!   $��	10,10,0,0q�6<   $	��Top=�6O   �� 5   
   ${�False��6   $
��Left=�6$   $��	58,14,0,0q�6?    �� TextWrapping$
 Wrap=�6R   $	��Top=�6~   $	��120��6�   ��6f   �� *Codigo+6�   �� 5   
   $
��Nome��6   $
��Left=�6   $��	10,41,0,0q�6:   $	��Top=�6M   �� 5   
   ${�False��6   $
��Left=�6$   $��	58,41,0,0q�6?   $
 Wrap=�6R   $	��Top=�6�   $	��120��6�   ��6f   �� *NomeCompleto+6�   �� 5   
   $��Livros Emprestados��6   $
��Left=�6-   $��	10,72,0,0q�6H   $	��Top=�6[   �� 5   
   $��
0,103,0,77q�6h   ��6>   �� *LivrosEmprestados+ �� View 5      �� 5      .P�5      �� $��Codigo��6%    �� DisplayMemberBinding 65   ��  �� Path$ Codigo��� 5      $
��Nome��6%    63   �� $ NomeCompleto�5      5      �� 5   
   -   5        $��Fechar��5      $��Center=�6"   $��	0,381,0,0q�6?   $	��Top=�6R   $��84��6   5      5      