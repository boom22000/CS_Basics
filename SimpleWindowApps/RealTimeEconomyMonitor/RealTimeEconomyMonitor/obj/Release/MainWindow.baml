   M S B A M L   `   `   `  ���� Q  MRealTimeEconomyMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:RealTimeEconomyMonitorRealTimeEconomyMonitor  '    !RealTimeEconomyMonitor.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6local$clr-namespace:RealTimeEconomyMonitor       � Title$:  4실시간 세계 경제 흐름 - 개발자 조성범��$	��800��$
��1200��.��� .+��� -    ��Name$	 Dim��$y�Hidden=� K� ZIndex$ 1��$
��auto��$
��auto��
 �� Top$	 100�� �� Left$	 100��
������� J� Opacity$	 0.5Q��� -    ��Name$ Spinner��$y�Hidden=�$ 2��$	��120��$	��120��$��Center=�$��Center=�$��Images/loading.png���  � RowDefinitions �� �� $��5* ��� �� $��5* �.+�� $��0�� � ColumnDefinitions �� �� .+��� $��0��$��Currency��	 �� TextAlignment$	 Center=�$��Center=�$��16(�$
��Bold"����#88EE88�� $��2��$��Index��$	 Center=�$��Center=�$��16(�$
��Bold"�
����·�� $��1�� �� �� .+�� $��0��.+��� -   
 ��Name$
 lstBx_Currency��$��Visible=� �� SelectionMode$ Extended=����� *lstBx_Currency_Items+ �� SelectedItem �� *lstBx_Currency_Selected+����  �� 
TargetType" ��.���  �� Property � �� �� Value$ 32����  � ��
 �������  � ��$ 1q�� $��1��.+��� -   $
 lstBx_Index��$��Visible=�$ Extended=����� *lstBx_Index_Items+ �� *lstBx_Index_Selected+���� " ��.���  � ��$ 32����  � ��
 �������  � ��$ 1q�� $��2�� �� �� .+��� $��0��$	��HOT��$	 Center=�$��Center=�$��16(�$
��Bold"����#EE8888�� $��2��$
��News��$	 Center=�$��Center=�$��16(�$
��Bold"����#EEEE88� $��3�� �� �� .+�� $��0��.+��� -   $
 
lstBx_Hots��$��Visible=�$ Extended=����� *lstBx_Hots_Items+ �� *lstBx_Hots_Selected+���� " ��.���  � ��$ 32����  � ��
 �������  � ��$ 1q�� $��1��.+��� -   $
 
lstBx_News��$��Visible=�$ Extended=����� *lstBx_News_Items+ �� *lstBx_News_Selected+���� " ��.���  � ��$ 32����  � ��
 �������  � ��$ 1q�-   