   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	   U QSystem.Runtime, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a7,clr-namespace:System;assembly=System.RuntimeSystem 5   	       WpfApp1.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	   ?System,clr-namespace:System;assembly=System.Runtime     5   	     � Title$  
MainWindow��5	   	   $	��600��6   $	��600��6)   .��5
      � $��
0,-2,-7,-5q�6   .+�5   
   �� -   5         ��Name$ list_listable��5      $��10,26,17,330q�6�    �� SelectedItem 6U   �� *ItemSelecionado+��6~   �� *ListaListable+ �� View 5      �� 5      .P�5      �� $��Codigo��6%    �� DisplayMemberBinding 65   ��  �� Path$ Codigo��� 5      $
��Nome��6%    63   �� $ NomeCompleto�5      5      �� 5   
   $��
Biblioteca��6   $
��Left=�6%   $��	219,4,0,0q�6@   $	��Top=�6S   $��26��6k   $��62��6w   �� 5   
   -   5         ��Name$ cmb_type��5   <   $
��Left=�6N   $��
10,274,0,0q�6i   $	��Top=�6}   $	��120��6�   $��22��6�    6   �� *TipoSelecionado+.@�5      ��
Pessoa��6$   ��5      	Livro��6#   5      �� 5   
   $��Incluir��6)   $
��Left=�6;   $��
10,307,0,0q�6V   $	��Top=�6j   $��20��6�   $��36��6�   ��6   �� *Add+6�   �� 5   
   $��Remover��6,   $
��Left=�6>   $��
10,338,0,0q�6Y   $	��Top=�6m   $��20��6�   $��51��6�   ��6   �� *Remove+6�   �� 5   
   $��Alterar��6,   $
��Left=�6>   $��
10,368,0,0q�6Y   $	��Top=�6m   $��20��6�   $��39��6�   ��6   �� *Update+6�   �� 5   
   $��	Emprestar��6/   $
��Left=�6C   $��
10,398,0,0q�6^   $	��Top=�6r   $��20��6�   $��57��6�   ��6   �� *	Emprestar+6�   �� 5   
   $��Devolver��6.   $
��Left=�6A   $��
10,433,0,0q�6\   $	��Top=�6p   $��20��6�   $��51��6�   ��6   �� *
Devolver+6�   �� 5   
   $��Informacoes��6*   $
��Left=�6@   $��
10,466,0,0q�6[   $	��Top=�6o   ��6   �� *Info+6o   5      5      