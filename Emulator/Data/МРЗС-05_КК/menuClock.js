var Clock = new function() {
    this.MenuWorkType = 'Parent';
	this.BlockNum = 1;
	this.IsEditMode = false;
    this.Parent = null;
	this.CallFunctionName = '';
	
	this.CursorLine = 1;
	this.CursorPositionInLine = 0;
	
	this.CurrentTime = new Date(
		GetVariable('clock_year'),
		GetVariable('clock_month'),
		GetVariable('clock_day'),
		GetVariable('clock_hour'),
		GetVariable('clock_minutes'),
		GetVariable('clock_seconds')
	);
	this.seconds = this.CurrentTime.getSeconds();
	this.minutes = this.CurrentTime.getMinutes();
	this.hours = this.CurrentTime.getHours();
	this.day = this.CurrentTime.getDate();
	this.month = this.CurrentTime.getMonth() + 1;
	this.year = this.CurrentTime.getFullYear();

	this.AddOneSecond = function() {
		if (!this.IsEditMode) {
			this.CurrentTime.setSeconds(this.CurrentTime.getSeconds()+1);
			this.seconds = this.CurrentTime.getSeconds();
			this.minutes = this.CurrentTime.getMinutes();
			this.hours = this.CurrentTime.getHours();
			this.day = this.CurrentTime.getDate();
			this.month = this.CurrentTime.getMonth() + 1;
			this.year = this.CurrentTime.getFullYear();
			
			var day = (this.day < 10) ? '0' + this.day : this.day; 
			var month = (this.month < 10) ? '0' + this.month : this.month; 
			var hours = (this.hours < 10) ? '0' + this.hours : this.hours; 
			var minutes = (this.minutes < 10) ? '0' + this.minutes : this.minutes; 
			var seconds = (this.seconds < 10) ? '0' + this.seconds : this.seconds; 
			
			SetVariable('clock_year', this.year);
			SetVariable('clock_month', month);
			SetVariable('clock_day', day);
			SetVariable('clock_hour', hours);
			SetVariable('clock_minutes', minutes);
			SetVariable('clock_seconds', seconds);
		}
	}
	
	this.Print = function() {
		if (this.BlockNum == 1) {
			var day = (this.day < 10) ? '0' + this.day : this.day; 
			var month = (this.month < 10) ? '0' + this.month : this.month; 
			var hours = (this.hours < 10) ? '0' + this.hours : this.hours; 
			var minutes = (this.minutes < 10) ? '0' + this.minutes : this.minutes; 
			var seconds = (this.seconds < 10) ? '0' + this.seconds : this.seconds; 
			if (this.IsEditMode) {
				disp1.text = day + '-' + month + '-' + this.year;
				disp2.text = hours + ':' + minutes + ':' + seconds;
			} else {
				disp1.text = '   ' + day + '-' + month + '-' + this.year;
				disp2.text = '    ' + hours + ':' + minutes + ':' + seconds;
			}
		} else {
			if (this.IsEditMode) {
				disp1.text = ' Коррекция хода';
				disp2.text = '' + GetVariable('clock_correction');
			} else {
				disp1.text = ' Коррекция хода';
				disp2.text = '    ' + GetVariable('clock_correction');
			}
		}
	}
	this.OnCursor = function() {	
		//this.CursorLine = 1;
		//this.CursorPositionInLine = 0;	
		var Disp = (this.CursorLine == 1) ? disp1 : disp2;
		if (!(Disp.text.substr(this.CursorPositionInLine,1) == '_')) {
			Disp.text = Disp.text.substr(0,this.CursorPositionInLine)+'_'+Disp.text.substr(this.CursorPositionInLine+1);
		} else {
			this.Print();
		}
	}
	this.OnEvent = function(Parent, Command, Params) {
		if (this.MenuWorkType == 'Self' || Command == 'close_child_function' || Command == 'init' || Command == 'password_error' || Command == 'password_ok') {
			switch (Command) {
				case "init": 
					this.Parent = Parent;
					this.MenuWorkType = 'Self';
					this.Print();
					break;
				case "cursor": 
					if (this.IsEditMode) {
						this.OnCursor();
					} else {
						this.Print();
					}
					break;
				case "enter": 
					this.MenuWorkType = 'Function';
					this.CallFunctionName = 'Password';
					eval(this.CallFunctionName+'.OnEvent(this, "init")');
					break;
				case "esc": 
					this.Parent.OnEvent("close_child_function");
					this.Parent = null;
					this.MenuWorkType = 'Parent';
					break;
				case "left": 
					if (this.IsEditMode) {
						if (this.CursorLine == 1) {
							switch (this.CursorPositionInLine) {
								case 9: this.CursorPositionInLine = 8; break;
								case 8: this.CursorPositionInLine = 7; break;
								case 7: this.CursorPositionInLine = 6; break;
								case 6: this.CursorPositionInLine = 4; break;
								case 4: this.CursorPositionInLine = 3; break;
								case 3: this.CursorPositionInLine = 1; break;
								case 1: this.CursorPositionInLine = 0; break;
							}
						} else {
							switch (this.CursorPositionInLine) {
								case 7: this.CursorPositionInLine = 6; break;
								case 6: this.CursorPositionInLine = 4; break;
								case 4: this.CursorPositionInLine = 3; break;
								case 3: this.CursorPositionInLine = 1; break;
								case 1: this.CursorPositionInLine = 0; break;
							}
						}
					}
					break;
				case "right": 
					if (this.IsEditMode) {
						if (this.CursorLine == 1) {
							switch (this.CursorPositionInLine) {
								case 0: this.CursorPositionInLine = 1; break;
								case 1: this.CursorPositionInLine = 3; break;
								case 3: this.CursorPositionInLine = 4; break;
								case 4: this.CursorPositionInLine = 6; break;
								case 6: this.CursorPositionInLine = 7; break;
								case 7: this.CursorPositionInLine = 8; break;
								case 8: this.CursorPositionInLine = 9; break;
							}
						} else {
							switch (this.CursorPositionInLine) {
								case 0: this.CursorPositionInLine = 1; break;
								case 1: this.CursorPositionInLine = 3; break;
								case 3: this.CursorPositionInLine = 4; break;
								case 4: this.CursorPositionInLine = 6; break;
								case 6: this.CursorPositionInLine = 7; break;
							}
						}
					}
					break;
				case "down": 
					if (!this.IsEditMode) {
						if (this.BlockNum == 1) {
							this.CursorPositionInLine = 0;
							this.BlockNum = 2;
							this.Print();
						}
					} else {
						this.Print();
						this.CursorLine = 2;
					}
					break;
				case "up": 
					if (!this.IsEditMode) {
						if (this.BlockNum == 2) {
							this.CursorPositionInLine = 0;
							this.BlockNum = 1;
							this.Print();
						}
					} else {
						this.Print();
						this.CursorLine = 1;
					}
					break;
				case "close_child_function":
					this.MenuWorkType = 'Self';
					break;
				case "password_error":
					this.MenuWorkType = 'Self';
					this.IsEditMode = false;
					this.Print();
					break;
				case "password_ok":
					this.CursorLine = 1;
					this.CursorPositionInLine = 0;
					this.MenuWorkType = 'Self';
					this.IsEditMode = true;
					this.Print();
					break;
				case "1": 
				case "2": 
				case "3": 
				case "4": 
				case "5": 
				case "6": 
				case "7": 
				case "8": 
				case "9": 
				case "0": 
					if (this.IsEditMode) {
						var Disp = (this.CursorLine == 1) ? disp1 : disp2;
						Disp.text = Disp.text.substr(0,this.CursorPositionInLine)+Command+Disp.text.substr(this.CursorPositionInLine+1);
						this.OnEvent(null, 'recalc');
					}
					break;		
				case "recalc":
					var date = disp1.text.split('.');
					var clock = disp2.text.split(':');
					this.day = date[0];
					this.month = date[1];
					this.hours = date[2];
					this.minutes = clock[0];
					this.seconds = clock[1];
			}
		} else {
			if (this.MenuWorkType == 'Function') {
				if (this.CallFunctionName.length > 0) {
					eval(this.CallFunctionName+'.OnEvent(this, "'+Command+'")');
					return;
				}
			}
		}
	}
}

TimerClock.SetTimer('Clock.AddOneSecond()', 1000);
function TimerClock::OnTimer(FuncName) {
	eval(FuncName);
    TimerClock.SetTimer('Clock.AddOneSecond()', 1000);
}
