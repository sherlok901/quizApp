/*
SetVariable('Уставка МТЗ1', '0002.0000');
SetVariable('Уставка МТЗ2', '0010.0000');
SetVariable('Выдержка МТЗ1', '0001.0000');
SetVariable('Выдержка МТЗ2', '0002.0000');
SetVariable('Т Ускор МТЗ', '0000.0000');
SetVariable('Т Ввода ускор', '0000.0000');
SetVariable('Вариант МТЗ', '1');
SetVariable('1 Ступень МТЗ', '1');
SetVariable('2 Ступень МТЗ', '1');
SetVariable('Ускор МТЗ2', '1');
*/

function CheckDefence_MTZ(IsInTimer) {
	if (!IsInTimer) IsInTimer = false;
	CheckDefence_MTZ1(IsInTimer);
	CheckDefence_MTZ2(IsInTimer);
}

function CheckDefence_MTZ1(IsInTimer) {
	if (!IsInTimer) IsInTimer = false;
	if ((
			GetVariable('IaA') > GetVariable('Уставка МТЗ1') ||
			GetVariable('IbA') > GetVariable('Уставка МТЗ1') ||
			GetVariable('IcA') > GetVariable('Уставка МТЗ1')
		) &&
		(GetVariable('1 Ступень МТЗ') == '1') &&
		(!IsInDVArrayValue('Блок МТЗ1'))) {
		if (IsInTimer) { 
			CheckR('Сраб МТЗ 1', true);
			CheckSDI('Сраб МТЗ 1', true);
			CheckDefence_APV(false, 1);
		} else {
			TimerDefenceMTZ1.SetTimer('CheckDefence_MTZ1(true)', GetVariable('Выдержка МТЗ1') * 1000 + 1);
		}
	}
}

function CheckDefence_MTZ2(IsInTimer, IsUskor) {
	if (!IsInTimer) IsInTimer = false;
	if ((
			GetVariable('IaA') > GetVariable('Уставка МТЗ2') ||
			GetVariable('IbA') > GetVariable('Уставка МТЗ2') ||
			GetVariable('IcA') > GetVariable('Уставка МТЗ2')
		) &&
		(GetVariable('2 Ступень МТЗ') == '1') &&
		(!IsInDVArrayValue('Блок МТЗ2'))) {
		if (IsInTimer) { 
			if (IsUskor) {
				CheckR('Блок ускор. МТЗ', true);
				CheckSDI('Блок ускор. МТЗ', true);
			} else {
				CheckR('Сраб МТЗ 1', true);
				CheckSDI('Сраб МТЗ 1', true);
			}
			CheckDefence_APV(false, 2);
		} else {
			if (GetVariable('Ускор МТЗ2') == '1') {
				TimerDefenceMTZ2.SetTimer('CheckDefence_MTZ2(true, true)', GetVariable('Т Ускор МТЗ') * 1000 + 1);
			} else {
				TimerDefenceMTZ2.SetTimer('CheckDefence_MTZ2(true, false)', GetVariable('Выдержка МТЗ2') * 1000 + 1);
			}
		}
	}
}

function TimerDefenceMTZ1::OnTimer(FuncName) {
	eval(FuncName);
}
function TimerDefenceMTZ2::OnTimer(FuncName) {
	eval(FuncName);
}
