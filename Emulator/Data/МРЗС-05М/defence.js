function CheckDefence(IsInTimer) {
	if (GetVariable('DeviceEnable') != '1') return;
	CheckDefence_MTZ(IsInTimer);
	CheckDefence_ZZ(IsInTimer);
	//CheckDefence_APV(IsInTimer); //запуск от MTZ...
}

function IsInDVArrayValue(name) {
	for (var i = 0; i < 6; i++) {
		if (GetVariable('btnDV0' + (i+1) + 'check') != '1') continue;
		var SomeDV = GetVariable('ВходыДВ0' + (i+1)).split(',');
		for (var j = 0; j < SomeDV.length; j++) {
			if (DVList[SomeDV[j]] == name) return true;
		}
	}
	return false;
}

function CheckSDI(name, enable) {
	if (!enable) enable = false;
	for (var i = 0; i < 6; i++) {
		if ((enable && GetVariable('0SDIIndicator' + (i+1) + 'check') == '1') ||
			(!enable && GetVariable('0SDIIndicator' + (i+1) + 'check') != '1')) {
			continue;
		}
		
		var SomeSDI = GetVariable('ИндикацияСДИ' + (i+1)).split(',');
		for (var j = 0; j < SomeSDI.length; j++) {
			if (IndicationList[SomeSDI[j]] == name) {
				var lamp = eval('SDIIndicator' + (i+1));
				lamp.LoadFromFile(SettingPath+'/pic/lamp_on_' + (enable ? 'true' : 'false') + '.bmp');
				SetVariable('0SDIIndicator' + (i+1) + 'check', (enable ? '1' : '0')); 
				break;
			}
		}
	}
}

function CheckR(name, enable) {
	if (!enable) enable = false;
	for (var i = 0; i < 5; i++) {
		if ((enable && GetVariable('btnR0' + (i+1) + 'check') == '1') ||
			(!enable && GetVariable('btnR0' + (i+1) + 'check') != '1')) {
			//continue;
		}
		
		var SomeR = GetVariable('Выходы комР0' + (i+1)).split(',');
		for (var j = 0; j < SomeR.length; j++) {
			if (RList[SomeR[j]] == name) {
				var lamp = eval('btnR0' + (i+1));
				lamp.LoadFromFile(SettingPath+'/pic/check_' + (enable ? 'true' : 'false') + '.jpg');
				SetVariable('btnR0' + (i+1) + 'check', (enable ? '1' : '0')); 
				break;
			}
		}
	}
}