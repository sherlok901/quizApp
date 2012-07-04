/*
SetVariable('1 Цикл АПВ', '0005.0000');
SetVariable('Блок АПВ1', '0010.0000');
SetVariable('Блок АПВ2', '0010.0000');
SetVariable('АПВ', '1');
SetVariable('Пуск от МТЗ1', '1');
SetVariable('Пуск от МТЗ2', '1');
*/

function CheckDefence_APV(IsInTimer, RunFromMTZ) {
	if (!IsInTimer) IsInTimer = false;
	if ((
			((GetVariable('Пуск от МТЗ1') == '1') && (RunFromMTZ == 1)) ||
			((GetVariable('Пуск от МТЗ2') == '1') && (RunFromMTZ == 2))
		) && IsInDVArrayValue('АЧР/АПВ')) {
		if (IsInTimer) { 
			CheckR('Блок МТЗ' + (RunFromMTZ), false);
			CheckSDI('АЧР/АПВ', true);
		} else {
			TimerDefenceAPV.SetTimer('CheckDefence_APV(true, ' + RunFromMTZ + ')', GetVariable('1 Цикл АПВ') * 1000 + 1);
		}
	}
}

function TimerDefenceAPV::OnTimer(FuncName) {
	eval(FuncName);
}
