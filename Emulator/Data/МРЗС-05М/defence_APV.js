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
		) && !IsInDVArrayValue('АЧР/ЧАПВ')) {
		if (IsInTimer) { 
			if (!(
					GetVariable('IaA') > GetVariable('Уставка МТЗ1') ||
					GetVariable('IbA') > GetVariable('Уставка МТЗ1') ||
					GetVariable('IcA') > GetVariable('Уставка МТЗ1')
				) &&
				(GetVariable('1 Ступень МТЗ') == '1')) {
				CheckR('Сраб МТЗ 1', false);
			} 
			if (!(
					GetVariable('IaA') > GetVariable('Уставка МТЗ2') ||
					GetVariable('IbA') > GetVariable('Уставка МТЗ2') ||
					GetVariable('IcA') > GetVariable('Уставка МТЗ2')
				) &&
				(GetVariable('2 Ступень МТЗ') == '1')) {
				CheckR('Сраб МТЗ 2', false);
			} 
			CheckSDI('Сраб АПВ', true);
		} else {
			TimerDefenceAPV.SetTimer('CheckDefence_APV(true, ' + RunFromMTZ + ')', GetVariable('1 Цикл АПВ') * 1000 + 1);
		}
	}
}

function TimerDefenceAPV::OnTimer(FuncName) {
	eval(FuncName);
}
