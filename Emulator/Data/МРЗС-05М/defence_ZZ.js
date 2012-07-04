function CheckDefence_ZZ(IsInTimer) {
	if (!IsInTimer) IsInTimer = false;
	if ((
			GetVariable('ZIO') > GetVariable('Уставка ЗЗ') ||
			GetVariable('IzfA') > GetVariable('Уставка ЗЗ')
		) &&
		GetVariable('Защита ЗЗ') == '1' &&
		(!IsInDVArrayValue('Блок ЗЗ'))) {
		if (IsInTimer) { 
			CheckR('Сраб ЗЗ', true);
			CheckSDI('Сраб ЗЗ', true);
		} else {
			TimerDefenceZZ.SetTimer('CheckDefence_ZZ(true)', GetVariable('Выдержка ЗЗ') * 1000 + 1);
		}
	}
}

function TimerDefenceZZ::OnTimer(FuncName) {
	eval(FuncName);
}
