variables = {};
SetVariable = function(name, value) {
	var res = OnBeforeSetVariable(name, value);
	variables[name] = value;
	OnAfterSetVariable(name, value);
}
GetVariable = function(name) {
	return variables[name];
}

function OnBeforeSetVariable(name, value) {
	//if ()
}

function OnAfterSetVariable(name, value) {
	if (!!Menu) {
		switch (name) {
			case 'КонфигурацияМТЗ':
				Menu.GetMenuByName('>МТЗ').IsEnable = (value == 0);
				break;
			case 'Конфигурация ЗЗ':
				Menu.GetMenuByName('>ЗЗ').IsEnable = (value == 0);
				break;
			case 'КонфигурацияАПВ':
				Menu.GetMenuByName('>АПВ').IsEnable = (value == 0);
				break;
			case 'ДВ01Вид сигнала':
				if (value == 1) btnDV01.LoadFromFile(SettingPath+'/pic/checkr_true.jpg');
				break;
			case 'ДВ02Вид сигнала':
				if (value == 1) btnDV02.LoadFromFile(SettingPath+'/pic/checkr_true.jpg');
				break;
			case 'ДВ03Вид сигнала':
				if (value == 1) btnDV03.LoadFromFile(SettingPath+'/pic/checkr_true.jpg');
				break;
			case 'ДВ04Вид сигнала':
				if (value == 1) btnDV04.LoadFromFile(SettingPath+'/pic/checkr_true.jpg');
				break;
			case 'ДВ05Вид сигнала':
				if (value == 1) btnDV05.LoadFromFile(SettingPath+'/pic/checkr_true.jpg');
				break;
			case 'ДВ06Вид сигнала':
				if (value == 1) btnDV06.LoadFromFile(SettingPath+'/pic/checkr_true.jpg');
				break;
			
		}
	}

	switch (name) {
		case 'F':
			SetVariable('3I0 Н ', value);
			break;
		case 'ZIO':
			SetVariable('3I0 КЗ', value);
			break;
			
		case 'IaA':
			SetVariable('IА  Н ', value);
			break;
		case 'IbA':
			SetVariable('IВ  Н ', value);
			break;
		case 'IcA':
			SetVariable('IС  Н ', value);
			break;
			
		case 'IaO':
			SetVariable('IА  КЗ', value);
			break;
		case 'IbO':
			SetVariable('IВ  КЗ', value);
			break;
		case 'IcO':
			SetVariable('IС  КЗ', value);
			break;

		case 'UaB':
			SetVariable('UАВ Н ', value);
			break;
		case 'UbB':
			SetVariable('UВС Н ', value);
			break;

		case 'UaO':
			SetVariable('UАВ КЗ', value);
			break;
		case 'UbO':
			SetVariable('UВС КЗ', value);
			break;
	}
	
}
/////////////////Списки ответов 

var test = [
	'test1',
	'test2',
	'test3',
	'test4',
	'test5'
];


var Configuration = ["  ЕСТЬ", "  НЕТ"];
var OFunctionType = ["  ПРЯМАЯ", "  ОБРАТНАЯ"];
var OFunctionSource = ["Сраб МТЗ1", "Сраб МТЗ2"];
var DVOKindSignal = ["  ПРЯМОЙ", "  ИНВЕРСНЫЙ"];
var DVOTypeSignal = ["  ПОСТОЯННЫЙ", "  ПЕРЕМЕННЫЙ"];
var DVList = ['Сраб ПО МТЗ 1',
'Сраб МТЗ 1',
'Сраб ПО МТЗ 2',
'Сраб МТЗ 2',
'Блок ускор. МТЗ',	
'Блок. МТЗ 1',
'Блок МТЗ 2',
'Аварийное Откл.',
'Сраб АПВ',
'АЧР/ЧАПВ',
'Сраб ПО ЗЗ',
'Сраб ЗЗ',
'Опред.',
'Блок вкл. ВВ',
'Работа БО',
'Работа БВ',
'Сброс индикации',
'Неисправность',
'Положение ВВ.'];
var RList = ['Сраб ПО МТЗ 1',
'Сраб МТЗ 1',
'Сраб ПО МТЗ 2',
'Сраб МТЗ 2',
'Блок ускор. МТЗ',	
'Блок. МТЗ 1',
'Блок МТЗ 2',
'Аварийное Откл.',
'Сраб АПВ',
'АЧР/ЧАПВ',
'Сраб ПО ЗЗ',
'Сраб ЗЗ',
'Опред.',
'Блок вкл. ВВ',
'Работа БО',
'Работа БВ',
'Сброс индикации',
'Неисправность',
'Положение ВВ.'];
var IndicationList = ['Сраб ПО МТЗ 1',
'Сраб МТЗ 1',
'Сраб ПО МТЗ 2',
'Сраб МТЗ 2',
'Блок ускор. МТЗ',	
'Блок. МТЗ 1',
'Блок МТЗ 2',
'Аварийное Откл.',
'Сраб АПВ',
'АЧР/ЧАПВ',
'Сраб ПО ЗЗ',
'Сраб ЗЗ',
'Опред.',
'Блок вкл. ВВ',
'Работа БО',
'Работа БВ',
'Сброс индикации',
'Неисправность',
'Положение ВВ.'];
			
var OnOff = ["  ОТКЛ", "  ВКЛ"];
var MTZVariant = ["  ЗАВИСИМАЯ", "  НЕЗАВИСИМАЯ"];


