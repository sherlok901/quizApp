var StdEditList = new function() {
    this.MenuWorkType = 'Parent';
    this.Parent = null;
	this.CallClass = null;
	this.ParentCallMenuName = '';
	this.ValueName = '';
	this.Value = '';
	this.ListArray;
	this.CurrentListArray = new Array();
	this.DisplayBlock = 0;
	this.IsEditOnly = false;
	this.IsEditMode = false;
	this.tmpValue = '';
	this.Disp1Val = '';
	
	this.CursorLine = 2;
	this.CursorPositionInLine = 0;
	
	this.Print = function() {
		if (this.IsEditOnly) { 
			disp1.text = this.Disp1Val;
			disp2.text = this.ListArray[this.Value];
		} else {
			if (this.IsEditMode) {
				disp1.text = this.ListArray[this.DisplayBlock];
				if (this.CurrentListArray.inArray(this.ListArray[this.DisplayBlock]) >= 0) {
					disp2.text = " дю";
				} else {
					disp2.text = " мер";
				}
			} else {
				disp1.text = this.CurrentListArray[this.DisplayBlock*2];
				if (this.CurrentListArray.length > this.DisplayBlock*2+1) {
					disp2.text = this.CurrentListArray[this.DisplayBlock*2+1];
				} else {
					disp2.text = '';
				}
			}
		}
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
					this.Parent = Parent;
					this.ParentCallMenuName = Parent.GetChildMenu().Name;
					this.ValueName = Params[0];
					this.ListArray = Params[1];
					this.IsEditOnly = Params[2];
					if (this.IsEditOnly) {
						this.Disp1Val = Params[3];
						this.Value = GetVariable(this.ValueName);
						this.IsEditMode = true;
						this.MenuWorkType = 'Function';
						this.CallClass = Password;
						this.CallClass.OnEvent(this, "init");
					} else {
						this.Value = GetVariable(this.ValueName).split(',');
						this.CurrentListArray = new Array();
						for(var i = 0; i < this.Value.length; i++) {
							this.CurrentListArray[this.CurrentListArray.length] = this.ListArray[this.Value[i]];
						}
						this.IsEditMode = false;
						this.MenuWorkType = "Self";
						this.DisplayBlock = 0;
						this.Print();
					}
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
						if (this.IsEditOnly) { 
							Mess('this.Value= '+this.Value);
							Mess('this.ListArray.length= '+this.ListArray.length);
							if (this.Value < this.ListArray.length-1) {
								this.Value++;
							} else {
								this.Value = 0;
							}
							Mess(this.Value);
						} else {
							var Num = this.CurrentListArray.inArray(this.ListArray[this.DisplayBlock]);
							if (Num >= 0) {
								this.CurrentListArray.remove(Num);
							} else {
								this.CurrentListArray.insert(this.DisplayBlock, this.ListArray[this.DisplayBlock]);
							}					
						}
					} else {
						this.MenuWorkType = 'Function';
						this.CallClass = Password;
						this.CallClass.OnEvent(this, "init");
					}
					this.Print();
					break;
				case "esc": 
					if (this.IsEditMode) {
						this.MenuWorkType = 'Function';
						this.CallClass = Confirm;
						this.CallClass.OnEvent(this, "init");
					} else {
						this.Parent.OnEvent("close_child_function");
						this.Parent = null;
						this.MenuWorkType = 'Parent';
					}
					break;
				case "close_child_function":
					this.MenuWorkType = 'Self';
					break;
				case "password_error":
					if (this.IsEditOnly) {
						this.Parent.OnEvent("close_child_function");
						this.Parent = null;
						this.MenuWorkType = 'Parent';
					} else {
						this.MenuWorkType = 'Self';
					}
					break;
				case "password_ok":
					this.MenuWorkType = 'Self';
					this.CursorLine = 2;
					this.CursorPositionInLine = 0;
					this.IsEditMode = true;
					this.DisplayBlock = 0;
					this.Print();
					break;
				case "confirm_yes":
					if (this.IsEditOnly) {
						this.DisplayBlock = 0;
						SetVariable(this.ValueName, this.Value);
						this.Parent.OnEvent("close_child_function");
						this.Parent = null;
						this.MenuWorkType = 'Parent';
					} else {
						this.MenuWorkType = 'Self';
						this.IsEditMode = false;
						this.DisplayBlock = 0;
						var res = new Array();
						for (var i = 0; i < this.ListArray.length; i++) {
							if (this.CurrentListArray.inArray(this.ListArray[i]) >= 0) {
								res[res.length] = i;
							}
						}
						SetVariable(this.ValueName, res.join(','));
					}
					break;
				case "confirm_no":
					if (this.IsEditOnly) {
						this.DisplayBlock = 0;
						this.Parent.OnEvent("close_child_function");
						this.Parent = null;
						this.MenuWorkType = 'Parent';
					} else {
						this.MenuWorkType = 'Self';
						this.IsEditMode = false;
						this.DisplayBlock = 0;
						this.CurrentListArray = new Array();
						this.Value = GetVariable(this.ValueName).split(',');
						for(var i = 0; i < this.Value.length; i++) {
							this.CurrentListArray[this.CurrentListArray.length] = this.ListArray[this.Value[i]];
						}
					}
					break;
				case "down":
					if (this.IsEditMode) {
						if (this.ListArray.length-1 > this.DisplayBlock) {
							this.DisplayBlock++;
						}
					} else {
						if (this.CurrentListArray.length/2-1 > this.DisplayBlock) {
							this.DisplayBlock++;
						}
					}
					this.Print();
					break;
				case "up": 
					if (this.IsEditMode) {
						if (this.DisplayBlock > 0) {
							this.DisplayBlock--;
						}
					} else {
						if (this.DisplayBlock > 0) {
							this.DisplayBlock--;
						}
					}
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