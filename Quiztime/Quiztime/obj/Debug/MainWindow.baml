   M S B A M L   `   `   `  ����C  ?Quiztime, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:QuiztimeQuiztime  5   	       Quiztime.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Quiztime     5   	     � WindowState$  	Maximized=�5   	    � WindowStyle$
 None=�5	   	    � Title$ 
MainWindow��5
   	   $	��450��6   $	��800��6)      
MainWindow#��[  65   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5    
   �� $��9��6     MainGrid#���  6+   6P   &   System.Windows.Controls.DataGrid  5!   
   -   5          Name$ QuizGrid��5!      $��1��6%   $��7��62   $��1��6C   $��3��6S      AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .    QuizGrid#���  6�   	   Columns	 5"      4  .System.Windows.Controls.DataGridTemplateColumn  5#      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Picture��6)      
IsReadOnly .    CellTemplate 5$      �� 5%      .R�5&      �� $	��100��6?   ��6$   �� *	Picture+6?   5'      5)      0  *System.Windows.Controls.DataGridTextColumn  5*      $

 Name��6%    . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *
QuizName+6d     5+      $

 Date��6%    .  63   �� *	Updated+6c     5,       .    Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6;    5-      �� 5.      .R�5/      �� -   5        $��
Start Quiz��5/   %    
 Button#���  6c   
 � Tag 6:   �� *	LocalId+6c   50      52        53       . $	 100 6;    54      �� 55      .R�56      �� -   5        $��
Check Quiz��56   %   #���  6d    6:   �� *	LocalId+6d   57      59        5:       . $	 100 6;    5;      �� 5<      .R�5=      �� -   5        $��	Edit Quiz��5=   %   #���  6b    69   �� *	LocalId+6b   5>      5@      5B      �� 5C   
   -   5        $��2��5C      $��9��6!   $��Add new Quiz��6.   #���  6E   6|   5D      5E      