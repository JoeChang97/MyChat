   M S B A M L   `   `   `  ����A  =MyChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyChat.ViewModelsMyChat.ViewModels  5   	       MyChat.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /my1clr-namespace:MyChat.ViewModels     5   	     � Title$  Client��5   	   $	��450��6   $	��800��6%   .��5      �  � ColumnDefinitions 5	   
   �� 5
      $
��429* �6   �� 5      $	��52* �6   �� 5      $
��311* �6   .+�5   
   �� $��Connect��6   $
��Left=�6?   $��26��6Z   $��309,309,0,0q�6f   $	��Top=�6{   $��76��6�    J� RenderTransformOrigin$ -0.662,0.475&�6�   ��6#   �� *	Connect+6�   �� 5   
   $
��Send��6   $
��Left=�69   $��49��6T   $��309,340,0,0q�6`   $	��Top=�6u   $��76��6�   $ -2.37,3.831&�6�   ��6    �� *Send+6�   �� 5   
   $
��Left=�6   $��80��6-   $��
10,309,0,0q�69    �� TextWrapping$
 Wrap=�6M   $	��Top=�6z   $	��294��6�   ��6a   �� *	Message+6�   �� 5   
   $
��Left=�6   $	��269��6-   $��	10,10,0,0q�6:   $
 Wrap=�6M   $	��Top=�6�   $	��375��6�   ��6a   �� *	Chatboard+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   -   5         ��Name$ QuestionLabel��5      $��Question��6'   $	��392��6:   $��Center=�6F   $
��Left=�6j   $��
390,41,0,0q�6�   $	��Top=�6�   $��3��6�   �� 5   
   -   5         ��Name$ TrueBtn��5      $��	True fact��6"   $
��Left=�6T   $��	35,90,0,0q�6o   $	��Top=�6�   $��74��6�   $��32��6�   $��1��6�   $��2��6�   ��66   �� *	WriteTrue+6�   �� 5   
   -   5        $ FalseBtn��5      $��
False bruh��6#   $
��Left=�6W   $��
146,90,0,0q�6r   $	��Top=�6�   $��74��6�   $��32��6�   $��2��6�   ��68   �� *
WriteFalse+6�   �� 5   
   $��Player 1��6   $
��Left=�6#   $��
35,182,0,0q�6>   $	��Top=�6R   $��1��6j   $��2��6z   �� 5   
   -   5        $ Player1Score��5      $
��Left=�6l   $��146,182,0,0q�6�   $	��Top=�6�   $��2��6�   ��6&   ��  �� Path$ Score1�$ PropertyChanged=�6�   �� 5   
   -   5        $ 
GameStatus��5      $��Question 1 of 10��6$   $��Center=�6?   $
��Left=�6c   $��
390,10,0,0q�6~   $	��Top=�6�   $	��392��6�   $��3��6�   �� 5   
   $��
Scoreboard��6   $
��Left=�6%   $��
35,156,0,0q�6@   $	��Top=�6T   $��1��6l   $��2��6|   �� 5   
   $��Player 2��6   $
��Left=�6#   $��
35,210,0,0q�6>   $	��Top=�6R   $��1��6j   $��2��6z   �� 5   
   -   5        $ Player2Score��5      $
��Left=�6l   $��146,210,0,0q�6�   $	��Top=�6�   $��2��6�   ��6&   �� $ Score2�$ PropertyChanged=�6�   5      5      