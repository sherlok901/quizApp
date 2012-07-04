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
	Button.Top = Top;
	Button.Left = Left;
	Button.Height = Height;
	Button.Width = Width; 
	Button.LoadFromFile(SettingPath+'/pic/'+ButtonName+'.'+Ext);
	Button.transparent = true;
	Button.proportional = true;
}


SetButton(ImageDeviceBackground, 'device_background', 5, 410, 460, 410, 'jpg');
SetButton(DeviceIndicator, 'lamp_on_false', 63, 705, 24, 24);
SetButton(SDIIndicator1, 'lamp_on_false', 63, 455, 24, 24);
SetButton(SDIIndicator2, 'lamp_on_false', 103, 455, 24, 24);
SetButton(SDIIndicator3, 'lamp_on_false', 143, 455, 24, 24);
SetButton(SDIIndicator4, 'lamp_on_false', 183, 455, 24, 24);
SetButton(SDIIndicator5, 'lamp_on_false', 223, 455, 24, 24);
SetButton(SDIIndicator6, 'lamp_on_false', 263, 455, 24, 24);

SetButton(btn1, 		'1', -300);
SetButton(btn2, 		'2', -300);
SetButton(btn3, 		'3', -300);
SetButton(btn4, 		'4', -300);
SetButton(btn5, 		'5', -300);
SetButton(btn6, 		'6', -300);
SetButton(btn7, 		'7', -300);
SetButton(btn8, 		'8', -300);
SetButton(btn9, 		'9', -300);
SetButton(btn0, 		'0', -300);
SetButton(btnDot, 		'Dot', -300);
SetButton(btnF, 	    'F', -300);
SetButton(btnEnter, 	'Enter', -300);
SetButton(btnEsc, 		'Esc',  -300);
SetButton(btnLEFT, 		'left', 	258, 595, 53, 60);
SetButton(btnRIGHT, 	'right', 	258, 635, 53, 60);
SetButton(btnUP, 		'up', 		258, 695, 60, 87);
SetButton(btnBOTTOM, 	'bottom', 	258, 735, 60, 87);

SetButton(btnDV01, 	'checkr_false', 5 + 30, 380, 21, 21, 'jpg');
SetButton(btnDV02, 	'checkr_false', 5 + 80, 380, 21, 21, 'jpg');
SetButton(btnDV03, 	'checkr_false', 5 + 130, 380, 21, 21, 'jpg');
SetButton(btnDV04, 	'checkr_false', 5 + 180, 380, 21, 21, 'jpg');
SetButton(btnDV05, 	'checkr_false', 5 + 230, 380, 21, 21, 'jpg');
SetButton(btnDV06, 	'checkr_false', 5 + 280, 380, 21, 21, 'jpg');

SetButton(btnR01, 	'check_false', 	30, 830, 21, 52, 'jpg');
SetButton(btnR02, 	'check_false', 	80, 830, 21, 52, 'jpg');
SetButton(btnR03, 	'check_false', 	130, 830, 21, 52, 'jpg');
SetButton(btnR04, 	'check_false', 	180, 830, 21, 52, 'jpg');
SetButton(btnR05, 	'check_false', 	230, 830, 21, 52, 'jpg');

SetEdit(SDIEdit1, 63,  480, 22, 50, "1");
SetEdit(SDIEdit2, 103, 480, 22, 50, "2");
SetEdit(SDIEdit3, 143, 480, 22, 50, "3");
SetEdit(SDIEdit4, 183, 480, 22, 50, "4");
SetEdit(SDIEdit5, 223, 480, 22, 50, "5");
SetEdit(SDIEdit6, 263, 480, 22, 50, "6");

SetEdit(disp1, 160, 580, 40, 202, Menu.MainMenu.Childs[0].Name);
SetEdit(disp2, 200, 580, 40, 202, Menu.MainMenu.Childs[1].Name);
