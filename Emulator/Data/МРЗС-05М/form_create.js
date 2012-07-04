ImageON.LoadFromFile(SettingPath+'/pic/button_on_false.jpg');
ImageOFF.LoadFromFile(SettingPath+'/pic/button_off_false.jpg');

with (disp1) { 	Font = 'MRZ';	color = 'clLime';	border_style = 'None';  font_size = 15;}
with (disp2) { 	Font = 'MRZ';	color = 'clLime';	border_style = 'None';  font_size = 15;}

function SetEdit(Edit, Top, Left, Height, Width, Text) {
	if (!Text) Text = '';
	Edit.Top = Top;
	Edit.Left = Left;
	Edit.Height = Height;
	Edit.Width = Width; 
	Edit.Text = Text;
}

function SetButton(Button, ButtonName, Top, Left, Height, Width, Ext) {
	if (!Ext) Ext = 'bmp'
	if (Top > 0)			Button.Top = Top;
	if (Left > 0)			Button.Left = Left;
	if (Height > 0)			Button.Height = Height;
	if (Width > 0)			Button.Width = Width; 
	Button.LoadFromFile(SettingPath+'/pic/'+ButtonName+'.'+Ext);
	Button.transparent = true;
	Button.proportional = true;
}


SetButton(ImageDeviceBackground, 'device_background', 5, 437, 460, 410, 'jpg');
SetButton(DeviceIndicator, 'lamp_on_false', 10, 474, 24, 24);
SetButton(SDIIndicator1, 'lamp_on_false', 126, 448, 24, 24);
SetButton(SDIIndicator2, 'lamp_on_false', 163, 448, 24, 24);
SetButton(SDIIndicator3, 'lamp_on_false', 200, 448, 24, 24);
SetButton(SDIIndicator4, 'lamp_on_false', 236, 448, 24, 24);
SetButton(SDIIndicator5, 'lamp_on_false', 273, 448, 24, 24);
SetButton(SDIIndicator6, 'lamp_on_false', 310, 448, 24, 24);

SetButton(btn1, 		'1', 		230, 549, 40, 40);
SetButton(btn2, 		'2', 		230, 594, 40, 40);
SetButton(btn3, 		'3', 		230, 639, 40, 40);
SetButton(btn4, 		'4', 		230, 684, 40, 40);
SetButton(btn5, 		'5', 		275, 549, 40, 40);
SetButton(btn6, 		'6', 		275, 594, 40, 40);
SetButton(btn7, 		'7', 		275, 639, 40, 40);
SetButton(btn8, 		'8', 		275, 684, 40, 40);
SetButton(btn9, 		'9', 		320, 549, 40, 40);
SetButton(btn0, 		'0', 		320, 594, 40, 40);
SetButton(btnDot, 		'Dot', 		320, 639, 40, 40);
SetButton(btnF, 	    'F', 		320, 684, 40, 40);
SetButton(btnEnter, 	'Enter',	365, 549, 40, 40);
SetButton(btnEsc, 		'Esc', 		410, 549, 40, 40);
SetButton(btnLEFT, 		'left', 	380, 593, 53, 60);
SetButton(btnRIGHT, 	'right', 	380, 676, 53, 60);
SetButton(btnUP, 		'up', 		360, 615, 37, 87);
SetButton(btnBOTTOM, 	'bottom', 	415, 615, 37, 87);

SetButton(btnDV01, 	'checkr_false', 5 + 30, 380, 21, 21, 'jpg');
SetButton(btnDV02, 	'checkr_false', 5 + 80, 380, 21, 21, 'jpg');
SetButton(btnDV03, 	'checkr_false', 5 + 130, 380, 21, 21, 'jpg');
SetButton(btnDV04, 	'checkr_false', 5 + 180, 380, 21, 21, 'jpg');
SetButton(btnDV05, 	'checkr_false', 5 + 230, 380, 21, 21, 'jpg');
SetButton(btnDV06, 	'checkr_false', 5 + 280, 380, 21, 21, 'jpg');

SetButton(btnR01, 	'check_false', 	30, 810, 21, 52, 'jpg');
SetButton(btnR02, 	'check_false', 	80, 810, 21, 52, 'jpg');
SetButton(btnR03, 	'check_false', 	130, 810, 21, 52, 'jpg');
SetButton(btnR04, 	'check_false', 	180, 810, 21, 52, 'jpg');
SetButton(btnR05, 	'check_false', 	230, 810, 21, 52, 'jpg');

SetEdit(SDIEdit1, 126, 472, 22, 50, "1");
SetEdit(SDIEdit2, 163, 472, 22, 50, "2");
SetEdit(SDIEdit3, 200, 472, 22, 50, "3");
SetEdit(SDIEdit4, 236, 472, 22, 50, "4");
SetEdit(SDIEdit5, 273, 472, 22, 50, "5");
SetEdit(SDIEdit6, 310, 472, 22, 50, "6");

SetEdit(disp1, 56, 485, 28, 202, Menu.MainMenu.Childs[0].Name);
SetEdit(disp2, 84, 485, 28, 202, Menu.MainMenu.Childs[1].Name);
