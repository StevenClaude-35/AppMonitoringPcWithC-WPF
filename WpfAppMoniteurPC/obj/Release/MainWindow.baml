   M S B A M L   `   `   `  ���� K  GWpfAppMoniteurPC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WpfAppMoniteurPCWpfAppMoniteurPC  !    WpfAppMoniteurPC.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:WpfAppMoniteurPC       � Title$*  $Moniteur d'activité - Claude Cavour��$	��580��$	��815�� � Icon$ Compteur.png��.��� ���	#7FCFD8DD.+��� ���	#FFE74C3C$
��Left=�$��32�����	#FFCFD8DD$	��Top=�$	��815���� -   $��infoMsg��$5��/Infos : Mise à jour de sécurité disponible !��$
��Left=�$��	0,-5,-2,0q�$	��Top=�$	��809��$
��Bold"�$��20(�
�������$��Center=�$��Center=��� $��Moniteur d'activité WPF�����	#FF37474F$
��Left=�$��	10,35,0,0q�$	��Top=�$	��787��$��51��$��Center=�$
��Bold"�$��30(��� ���	#00E74C3C$
��Left=�$��90��$��6,91,0,0q����	#FFCFD8DD$	��Top=�$	��787���� ���	#00E74C3C$
��Left=�$	��294��$��5,198,545,0q����	#FFCFD8DD$	��Top=�$	��244���� ���	#00E74C3C$��Center=�$	��294��$��275,198,275,0q����	#FFCFD8DD$	��Top=�$	��245���� ���	#00E74C3C$��Right=�$	��294��$��545,198,5,0q����	#FFCFD8DD$	��Top=�$	��246���� $��Informations système :�����	#FF37474F$
��Left=�$��5,95,0,0q�$	��Top=�$	��392��$��38��$
��Bold"�$��22(��� -   $��osName��$��OS��$
��Left=�$��	5,133,0,0q�$	��Top=�$��24��$	��280���� -   $��	archiName��$��OS��$
��Left=�$��	5,157,0,0q�$	��Top=�$��24��$	��280���� -   $��gpuName��$	��GPU��$
��Left=�$��299,157,0,0q�$	��Top=�$��24��$	��479���� -   $��procName��$
��PROC��$
��Left=�$��299,133,0,0q�$	��Top=�$��24��$	��479���� $
��Left=�$	��246��$��	6,206,0,0q�$	��Top=�$	��243��$��Compteur.png���� -   $��imgAiguille��$
��Left=�$��70��$��
74,270,0,0q�$	��Top=�$��70��$��Aiguille.png�� J� RenderTransformOrigin$ 0.729,0.729&�z�V� .~��� �� ��  �� Angle$ 179.464Q�T� �� $	��CPU�����	#FFCFD8DD$
��Left=�$��
68,381,0,0q�$	��Top=�$	��119��$��51��$��Center=�$
��Bold"�$��30(��� -   $	��cpu��$��0,00%��
���   �$
��Left=�$��
68,437,0,0q�$	��Top=�$	��119��$��49��$��Center=�$
��Bold"�$��30(�$��Center=��� $	��RAM�����	#FF37474F$
��Left=�$��288,200,0,0q�$	��Top=�$��81��$��51��$��Center=�$
��Bold"�$��30(��� $ ��Utilisation de la mémoire��$
��Left=�$��288,253,0,0q�$	��Top=�$	��222��$��18(�� -   $��barName��$
��Left=�$��25��$��288,289,0,0q�$	��Top=�$	��222�����	#FFCFD8DD���	#FF78909C���	#FF37474F � Value$ 50Q��� -	   $��ramTotal��$��Total : 15,90 GB��$
��Left=�$��288,317,0,0q�$	��Top=�$	��222��$��18(��� -
   $��ramUsed��$��Utilisée : 10,00 GB��$
��Left=�$��288,353,0,0q�$	��Top=�$	��222��$��18(��� -   $��ramFree��$��Libre : 5,90 GB��$
��Left=�$��288,389,0,0q�$	��Top=�$	��222��$��18(��� $��TEMPERATURE�����	#FF37474F$
��Left=�$��318,425,0,0q�$	��Top=�$	��153��$��38��$��Center=�$
��Bold"�$��20(��� -   $
��temp��$��85°C�����	#FF37474F$
��Left=�$��281,463,0,0q�$	��Top=�$	��245��$��29��$��Center=�$
��Bold"�$��18(�$��Center=��� $��DISQUES�����	#FF37474F$
��Left=�$��556,198,0,0q�$	��Top=�$	��142��$��51��$��Center=�$
��Bold"�$��30(��� -   $��listeDisques��$
��Left=�$��81��$��561,249,0,0q�$	��Top=�$	��222���� $��RESEAU�����	#FF37474F$
��Left=�$��556,336,0,0q�$	��Top=�$	��142��$��51��$��Center=�$
��Bold"�$��30(��� $��Données transférées :��$
��Left=�$��556,381,0,0q�$	��Top=�$	��222��$��18(��� -   $��netMont��$��0��$
��Left=�$��556,415,0,0q�$	��Top=�$	��191��$��18(�$��Right=�$��Center=��� -   $��netDes��$��0��$
��Left=�$��556,453,0,0q�$	��Top=�$	��191��$��18(�$��Right=�$��Center=��� $
��Left=�$��29��$��760,420,0,0q�$	��Top=�$��33��$��	arrow.png���� $
��Left=�$��29��$��761,456,0,0q�$	��Top=�$��33��$��	arrow.png��$ 0.5,0.5&�z�V� .~��� �� �� $ 179.01Q�T� �� ���	#FF37474F$
��Left=�$��37��$��
0,512,-2,0q����	#FFCFD8DD$	��Top=�$	��815���� $��Copyright @ Claude Cavour��$
��Left=�$��309,512,0,0q�$	��Top=�$��Center=�$��Center=�$��14(�
�������