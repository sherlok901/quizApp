var Confirm = new function() {
    this.MenuWorkType = 'Parent';
    this.Parent = null;
	this.CallFunction = null;
	
	this.Print = function() {
		disp1.text = '   Вы уверены?';
		disp2.text = 'Enter-ДА,  Esc-НЕТ';
	}
	this.OnEvent = function(Parent, Command) {
		if (this.MenuWorkType == 'Self' || Command == 'close_child_function' || Command == 'init') {
			switch (Command) {
				case "init": 
					this.Parent = Parent;
					this.MenuWorkType = 'Self';
					this.Password = '';
					this.Print();
					break;
				case "cursor": 
					this.Print();
					break;
				case "enter": 
					this.Parent.OnEvent(this, "confirm_yes");
					this.Parent = null;
					this.MenuWorkType = 'Parent';
					break;
				case "esc": 
					this.Parent.OnEvent(this, "confirm_no");
					this.Parent = null;
					this.MenuWorkType = 'Parent';
					break;
			}
		} else {
		
		}
	}
}