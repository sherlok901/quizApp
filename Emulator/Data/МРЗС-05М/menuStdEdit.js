var StdEdit = new function() {
    this.MenuWorkType = 'Parent';
    this.Parent = null;
	this.CallClass = '';
	this.ParentCallMenuName = '';
	this.ValueName = '';
	this.tmpValue = '';
	
	this.CursorLine = 2;
	this.CursorPositionInLine = 0;
	
	this.Print = function() {
		disp1.text = this.ParentCallMenuName;
		disp2.text = this.tmpValue;
	}
	this.OnCursor = function() {	
		var Disp = (this.CursorLine == 1) ? disp1 : disp2;
		if (!(Disp.text.substr(this.CursorPositionInLine,1) == '_')) {
			Disp.text = Disp.text.substr(0,this.CursorPositionInLine)+'_'+Disp.text.substr(this.CursorPositionInLine+1);
		} else {
			this.Print();
		}
	}
	this.OnEvent = function(Parent, Command, Params) {
		if (this.MenuWorkType == 'Self' || Command == 'close_child_function' || Command == 'init' || Command == 'password_error' || Command == 'password_ok' || Command == 'confirm_yes' || Command == 'confirm_no') {
			switch (Command) {
				case "init": 
					this.ParentCallMenuName = Parent.GetChildMenu().Name;
					this.ValueName = Params[0];
					this.tmpValue = GetVariable(this.ValueName) + '';
					if (this.tmpValue.indexOf('.') < 4) {
						this.tmpValue = ("0".repeat(this.tmpValue.indexOf('.'))) + this.tmpValue;
					}
					if ((9 - this.tmpValue.length) > 0) {
						this.tmpValue = this.tmpValue + ("0".repeat(9 - this.tmpValue.length));
					}
					this.Parent = Parent;
					this.MenuWorkType = 'Function';
					this.CallClass = Password;
					this.CallClass.OnEvent(this, "init");
					break;
				case "cursor": 
					if (this.IsEditMode) {
						this.OnCursor();
					} else {
						this.Print();
					}
					break;
				case "enter": 
					if (this.IsEditMode) {
						this.MenuWorkType = 'Function';
						this.CallClass = Confirm;
						this.CallClass.OnEvent(this, "init");
					}
					break;
				case "esc": 
					this.Parent.OnEvent("close_child_function");
					this.Parent = null;
					this.MenuWorkType = 'Parent';
					break;
				case "close_child_function":
					this.MenuWorkType = 'Self';
					break;
				case "password_error":
					this.Parent.OnEvent("close_child_function");
					this.Parent = null;
					this.MenuWorkType = 'Parent';
					break;
				case "password_ok":
					this.CursorLine = 2;
					this.CursorPositionInLine = 0;
					this.MenuWorkType = 'Self';
					this.IsEditMode = true;
					this.Print();
					break;
				case "confirm_yes":
					SetVariable(this.ValueName, this.tmpValue);
					this.Parent.OnEvent("close_child_function");
					this.Parent = null;
					this.MenuWorkType = 'Parent';
					break;
				case "confirm_no":
					this.Parent.OnEvent("close_child_function");
					this.Parent = null;
					this.MenuWorkType = 'Parent';
					break;
				case "left": 
					if (this.IsEditMode) {
						switch (this.CursorPositionInLine) {
							case 8: this.CursorPositionInLine = 7; break;
							case 7: this.CursorPositionInLine = 6; break;
							case 6: this.CursorPositionInLine = 5; break;
							case 5: this.CursorPositionInLine = 3; break;
							case 3: this.CursorPositionInLine = 2; break;
							case 2: this.CursorPositionInLine = 1; break;
							case 1: this.CursorPositionInLine = 0; break;
						}
					}
					break;
				case "right": 
					if (this.IsEditMode) {
						switch (this.CursorPositionInLine) {
							case 0: this.CursorPositionInLine = 1; break;
							case 1: this.CursorPositionInLine = 2; break;
							case 2: this.CursorPositionInLine = 3; break;
							case 3: this.CursorPositionInLine = 5; break;
							case 5: this.CursorPositionInLine = 6; break;
							case 6: this.CursorPositionInLine = 7; break;
							case 7: this.CursorPositionInLine = 8; break;
						}
					}
					break;
				case "down": 
					break;
				case "up": 
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
						this.tmpValue = this.tmpValue.substr(0,this.CursorPositionInLine)+Command+this.tmpValue.substr(this.CursorPositionInLine+1);
						this.Print();
					break;		
			}
		} else {
			if (this.MenuWorkType == 'Function') {
				if (this.CallClass != null) {
					this.CallClass.OnEvent(this, Command);
					return;
				}
			}
		}
	}
}