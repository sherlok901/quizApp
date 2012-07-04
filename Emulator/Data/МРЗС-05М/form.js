var SettingPath = 'Data/ÌÐÇÑ-05Ì';
var isNoEditData = false;

function ImageON::OnClick() {
	ImageON.LoadFromFile(SettingPath+'/pic/button_on_true.jpg');
	ImageOFF.LoadFromFile(SettingPath+'/pic/button_off_false.jpg');
	SetVariable('DeviceEnable', '1');
	CheckDefence();
}

function ImageOFF::OnClick() {
	ImageON.LoadFromFile(SettingPath+'/pic/button_on_false.jpg');
	ImageOFF.LoadFromFile(SettingPath+'/pic/button_off_true.jpg');
	SetVariable('DeviceEnable', '0');
	CheckDefence();
}

function SomeCheckClick(btn, var_name, img_subname, ext) { 
	ext = (!ext) ? 'jpg' : ext;
	if (GetVariable(var_name) == '0') {
		SetVariable(var_name, '1');
		btn.LoadFromFile(SettingPath+'/pic/'+img_subname+'_true.'+ext);
	} else {
		SetVariable(var_name, '0');
		btn.LoadFromFile(SettingPath+'/pic/'+img_subname+'_false.'+ext);
	}	
}

Menu.Init();

function OnClickF() {
	for (var i = 0; i < 6; i++) {
		var lamp = eval('SDIIndicator' + (i+1));
		lamp.LoadFromFile(SettingPath+'/pic/lamp_on_false.bmp');
	}
}

arr_static_edits = [
	{	Edit_name: 'EditF',		Variable_name: 'F',		UpDown_name: 'UpDownF',		min: 0,	max: 100},
	{	Edit_name: 'EditZIO',	Variable_name: 'ZIO',	UpDown_name: 'UpDownZIO',	min: 0,	max: 100},
                                                                     
	{	Edit_name: 'EditIaA',	Variable_name: 'IaA',	UpDown_name: 'UpDownIaA',	min: 0,	max: 100},
	{	Edit_name: 'EditIbA',	Variable_name: 'IbA',	UpDown_name: 'UpDownIbA',	min: 0,	max: 100},
	{	Edit_name: 'EditIcA',	Variable_name: 'IcA',	UpDown_name: 'UpDownIcA',	min: 0,	max: 100},
	{	Edit_name: 'EditIzfA',	Variable_name: 'IzfA',	UpDown_name: 'UpDownIzfA',	min: 0,	max: 100},
                                                                     
	{	Edit_name: 'EditIaO',	Variable_name: 'IaO',	UpDown_name: 'UpDownIaO',	min: 0,	max: 360},
	{	Edit_name: 'EditIbO',	Variable_name: 'IbO',	UpDown_name: 'UpDownIbO',	min: 0,	max: 360},
	{	Edit_name: 'EditIcO',	Variable_name: 'IcO',	UpDown_name: 'UpDownIcO',	min: 0,	max: 360},
	{	Edit_name: 'EditIzfO',	Variable_name: 'IzfO',	UpDown_name: 'UpDownIzfO',	min: 0,	max: 360},
                                                                     
	{	Edit_name: 'EditUaB',	Variable_name: 'UaB',	UpDown_name: 'UpDownUaB',	min: 0,	max: 100},
	{	Edit_name: 'EditUbB',	Variable_name: 'UbB',	UpDown_name: 'UpDownUbB',	min: 0,	max: 100},
	{	Edit_name: 'EditUcB',	Variable_name: 'UcB',	UpDown_name: 'UpDownUcB',	min: 0,	max: 100},
	{	Edit_name: 'EditUzfB',	Variable_name: 'UzfB',	UpDown_name: 'UpDownUzfB',	min: 0,	max: 100},
                                                                     
	{	Edit_name: 'EditUaO',	Variable_name: 'UaO',	UpDown_name: 'UpDownUaO',	min: 0,	max: 360},
	{	Edit_name: 'EditUbO',	Variable_name: 'UbO',	UpDown_name: 'UpDownUbO',	min: 0,	max: 360},
	{	Edit_name: 'EditUcO',	Variable_name: 'UcO',	UpDown_name: 'UpDownUcO',	min: 0,	max: 360},
	{	Edit_name: 'EditUzfO',	Variable_name: 'UzfO',	UpDown_name: 'UpDownUzfO',	min: 0,	max: 360}
];
	
var ActiveObj = null;
for (var i = 0; i < arr_static_edits.length; i++) {
	eval(arr_static_edits[i].Edit_name).text = GetVariable(arr_static_edits[i].Variable_name);
}
function SomeUpDownClick(UpDownName, ClickType) {
	var obj = null;
	for (var i = 0; i < arr_static_edits.length; i++) {
		if (arr_static_edits[i].UpDown_name == UpDownName) {
			obj = arr_static_edits[i];
			break; 
		}
	}
	if (obj == null) return;
	SetActiveObj(obj.Edit_name);
	ClickType = (ClickType == 1) ? "UP" : "DOWN";
	var value = GetVariable(obj.Variable_name)*1;
	var step = EditStep.text*1;
	if (ClickType == "UP") {
		value = value * 1.0 + step * 1.0;
	} else {
		value = value * 1.0 - step * 1.0;
	}
	if (value < 0.00001) value = 0;
	if (value < obj.min) value = obj.min;
	if (value > obj.max) value = obj.max;
	SetVariable(obj.Variable_name, value);
	
	isNoEditData = true;
	eval(ActiveObj.Edit_name).text = (value + '');
	isNoEditData = false;
	SetActiveObj(ActiveObj.Edit_name);
	CheckDefence();
}

//////////////////////////////////
function SetActiveObj(Edit_name) {
	if (ActiveObj != null) {
		eval(ActiveObj.Edit_name).color = 'clNavy';
	}
	var obj = null;
	for (var i = 0; i < arr_static_edits.length; i++) {
		if (arr_static_edits[i].Edit_name == Edit_name) {
			obj = arr_static_edits[i];
			break; 
		}
	}
	ActiveObj = obj;
	eval(ActiveObj.Edit_name).color = 'clPurple';
	var value = GetVariable(ActiveObj.Variable_name);
	TrackBar.min = ActiveObj.min*100;
	TrackBar.max = ActiveObj.max*100;
	TrackBar.position = value*100;

	EditMin.Text = ActiveObj.min;
	EditMax.Text = ActiveObj.max;
}

function SomeEditClick(EditName) {
	SetActiveObj(EditName);
}

function TrackBar::OnChange() {
	if (ActiveObj == null) return;
	var value = (TrackBar.position*1.0)/100;
	isNoEditData = true;
	eval(ActiveObj.Edit_name).text = (value + '');
	isNoEditData = false;
	SetVariable(ActiveObj.Variable_name, value);
	CheckDefence();
}

function EditStep::OnChange() {
	var value = EditStep.text;
	value = value.replace(',', '.');
	if (!isInt(value)) return;
	if (value != EditStep.text) EditStep.text = value;
	/*if (text > 0) {
		TrackBar.step = text*100;
	}*/
}

function isInt ( s ) {
	return !isNaN( s * 1.0 );
}


function SomeEditChange(EditName) {
	if (isNoEditData) return;
	var value = eval(EditName).text;
	value = value.replace(',', '.');
	if (!isInt(value)) return;
	SetActiveObj(EditName);

	Mess(value);
	value = value * 1.0;

	SetVariable(ActiveObj.Variable_name, value);
	SetActiveObj(ActiveObj.Edit_name);

	CheckDefence();
}




function blinc(button, name, is_timer) {
	if(!is_timer) {
		button.LoadFromFile(SettingPath+'/pic/'+name+'_active.bmp');
		TimerButtonClick.SetTimer('blinc("'+button+'", "'+name+'", true)', 130);
	} else {
		switch (name) {
			case '1': 	button = btn1;	break;
			case '2':	button = btn2;	break;
			case '3':	button = btn3;	break;
			case '4':	button = btn4;	break;
			case '5': 	button = btn5;	break;
			case '6':	button = btn6;	break;
			case '7':	button = btn7;	break;
			case '8':	button = btn8;	break;
			case '9': 	button = btn9;	break;
			case '0':	button = btn0;	break;

			case 'dot':	button = btnDot;	break;
			case 'f':	button = btnF;	break;
			case 'enter': 	button = btnEnter;	break;
			case 'esc':	button = btnEsc;	break;

			case 'left': 	button = btnLEFT;	break;
			case 'right':	button = btnRIGHT;	break;
			case 'up':		button = btnUP;		break;
			case 'bottom':	button = btnBOTTOM;	break;
		}
		SetButton(button, name);
	}
}

function TimerButtonClick::OnTimer(FuncName) {
	eval(FuncName);
}











