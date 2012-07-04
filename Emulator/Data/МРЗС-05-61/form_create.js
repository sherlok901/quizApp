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
SetButton(DeviceIndicator, 'lamp_on_false', 62, 715, 24, 24);
SetButton(SDIIndicator1, 'lamp_on_false', 64,  435, 24, 24);
SetButton(SDIIndicator2, 'lamp_on_false', 102, 435, 24, 24);
SetButton(SDIIndicator3, 'lamp_on_false', 140, 435, 24, 24);
SetButton(SDIIndicator4, 'lamp_on_false', 178, 435, 24, 24);
SetButton(SDIIndicator5, 'lamp_on_false', 216, 435, 24, 24);
SetButton(SDIIndicator6, 'lamp_on_false', 254, 435, 24, 24);

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
SetButton(btnLEFT, 		'left', 	190, 595, 40, 60);
SetButton(btnRIGHT, 	'right', 	190, 700, 40, 60);
SetButton(btnUP, 		'up', 		175, 627, 30, 87);
SetButton(btnBOTTOM, 	'bottom', 	217, 627, 30, 87);

SetButton(btnDV01, 	'checkr_false', 5 + 30, 385, 21, 21, 'jpg');
SetButton(btnDV02, 	'checkr_false', 5 + 70, 385, 21, 21, 'jpg');
SetButton(btnDV03, 	'checkr_false', 5 + 110, 385, 21, 21, 'jpg');
SetButton(btnDV04, 	'checkr_false', 5 + 150, 385, 21, 21, 'jpg');
SetButton(btnDV05, 	'checkr_false', 5 + 190, 385, 21, 21, 'jpg');
SetButton(btnDV06, 	'checkr_false', 5 + 230, 385, 21, 21, 'jpg');

SetButton(btnDV07, 	'checkr_false', 5 + 270, 385, 21, 21, 'jpg');
SetButton(btnDV08, 	'checkr_false', 5 + 310, 385, 21, 21, 'jpg');
SetButton(btnDV09, 	'checkr_false', 5 + 350, 385, 21, 21, 'jpg');
SetButton(btnDV010, 	'checkr_false', 5 + 390, 385, 21, 21, 'jpg');
SetButton(btnDV011, 	'checkr_false', 5 + 430, 385, 21, 21, 'jpg');
SetButton(btnDV012, 	'checkr_false', 5 + 470, 385, 21, 21, 'jpg');

SetButton(btnR01, 	'check_false', 	30, 825, 21, 52, 'jpg');
SetButton(btnR02, 	'check_false', 	70, 825, 21, 52, 'jpg');
SetButton(btnR03, 	'check_false', 	110, 825, 21, 52, 'jpg');
SetButton(btnR04, 	'check_false', 	150, 825, 21, 52, 'jpg');
SetButton(btnR05, 	'check_false', 	190, 825, 21, 52, 'jpg');

SetButton(btnR06, 	'check_false', 	230, 825, 21, 52, 'jpg');
SetButton(btnR07, 	'check_false', 	270, 825, 21, 52, 'jpg');
SetButton(btnR08, 	'check_false', 	310, 825, 21, 52, 'jpg');
SetButton(btnR09, 	'check_false', 	350, 825, 21, 52, 'jpg');
SetButton(btnR010, 	'check_false', 	390, 825, 21, 52, 'jpg');

SetEdit(SDIEdit1, 64,  465, 22, 50, "1");
SetEdit(SDIEdit2, 102, 465, 22, 50, "2");
SetEdit(SDIEdit3, 140, 465, 22, 50, "3");
SetEdit(SDIEdit4, 178, 465, 22, 50, "4");
SetEdit(SDIEdit5, 216, 465, 22, 50, "5");
SetEdit(SDIEdit6, 254, 465, 22, 50, "6");

SetEdit(disp1, 52, 540, 22, 160, Menu.MainMenu.Childs[0].Name);
SetEdit(disp2, 74, 540, 22, 160, Menu.MainMenu.Childs[1].Name);
