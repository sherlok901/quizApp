var Menu = new function() {
	this.MainMenu = {
		Name: 'Главное меню',
		PositionInParentMenu: 1,
		IsEnable: true,
		IsEnableCursor: true,
		IsBlockMenuCrossing: false,
		CallClass: null,
		CallFunctionParams: null,

		Parent: null,
		Childs: new Array()
	};

    this.MenuWorkType = 'Self';
    this.MenuChildClass = null;
    this.MenuLinePos = 0;
    this.MenuDispShow = [0,1];
	
	this.Init = function(SourceMenu) {
		this.CurrentMenu = this.GetFilteredMenu(this.MainMenu);
	}

	this.GetFilteredMenu = function(SourceMenu) {
		var ResultMenu = {
			Name: SourceMenu.Name,
			PositionInParentMenu: SourceMenu.PositionInParentMenu,
			IsEnable: SourceMenu.IsEnable,
			IsEnableCursor: SourceMenu.IsEnableCursor,
			IsBlockMenuCrossing: SourceMenu.IsBlockMenuCrossing,
			CallClass: SourceMenu.CallClass,
			CallFunctionParams: SourceMenu.CallFunctionParams,
			
			Parent: SourceMenu.Parent,
			Childs: new Array()
		};
		if (SourceMenu.CallClass != null) {
			Mess(SourceMenu.CallClass);
		}
		for (var i = 0; i < SourceMenu.Childs.length; i++) {
			if (SourceMenu.Childs[i].IsEnable == true) {
				ResultMenu.Childs[ResultMenu.Childs.length] = SourceMenu.Childs[i];
				ResultMenu.Childs[ResultMenu.Childs.length-1].PositionInParentMenu = ResultMenu.Childs.length-1;
			}
		}
		return ResultMenu;
	}
	
	this.DecodeVariable = function(MenuName) {
		var vb = '<variable:';
		var ve = '>';
		var b = MenuName.indexOf(vb);
		if (b >= 0) {
			var e = MenuName.indexOf(ve);
			var VariableName = MenuName.substr(b+vb.length, e-(b+vb.length));
			MenuName = MenuName.substr(0,b) + GetVariable(VariableName) + MenuName.substr(e+1);
		} else {
			return false;
		}
		return MenuName;
	}
	this.EvalVariable = function(MenuName) {
		var vb = '<eval:';
		var ve = '>';
		var b = MenuName.indexOf(vb);
		if (b >= 0) {
			var e = MenuName.indexOf(ve);
			var data = MenuName.substr(b+vb.length, e-(b+vb.length));
			MenuName = MenuName.substr(0,b) + eval(data) + MenuName.substr(e+1);
		} else {
			return false;
		}
		return MenuName;
	}
	this.GetMenuDecodeName = function(MenuName) {
		var tmpVal = '';
		while (true) {
			tmpVal = this.DecodeVariable(MenuName);
			if (!(tmpVal === false)) {
				MenuName = tmpVal; 
				continue;
			}
			tmpVal = this.EvalVariable(MenuName);
			if (!(tmpVal === false)) {
				MenuName = tmpVal; 
				continue;
			}
			break;
		}
		return MenuName;
	}
	this.Print = function() {
		disp1.text = this.GetMenuDecodeName(this.CurrentMenu.Childs[this.MenuDispShow[0]].Name);
		if (this.CurrentMenu.Childs.length > this.MenuDispShow[1]) {
			disp2.text = this.GetMenuDecodeName(this.CurrentMenu.Childs[this.MenuDispShow[1]].Name);
		} else {
			disp2.text = "";
		}
	}

// Обрабатываем нажатие клавиш прибора и другие события...
	this.OnEvent = function(Command) {
		if (Command != 'cursor') Mess(Command);
		if (this.MenuWorkType == 'Self' || Command == 'close_child_function' || Command == 'init') {
			switch (Command) {
				case "up":
					this.OnButtonUp();
					break;
				case "down":
					this.OnButtonDown();
					break;
				case "left":
					this.OnButtonUp();
					break;
				case "right":
					this.OnButtonUp();
					break;
				case "enter":
					this.OnButtonEnter();
					break;
				case "esc":
					this.OnButtonEsc();
					break;
				case "cursor":
					this.OnCursor();
					break;
				case "close_child_function":
					this.OnCloseChildFunction();
					break;
			}
		} else {
			if (this.MenuWorkType == 'Function') {
				var ChildMenu = this.GetChildMenu();
				if (ChildMenu.CallClass != null) {
					this.MenuWorkType = 'Function';
					this.MenuChildClass.OnEvent(this, Command, ChildMenu.CallFunctionParams);
					return;
				}
			}
		}
	}
// Функции событий...
	this.OnButtonDown = function() {
		if (this.CurrentMenu.IsBlockMenuCrossing) {
			if (this.MenuLinePos + 2 < this.CurrentMenu.Childs.length) {
				this.MenuLinePos+=2;
				this.MenuDispShow = [this.MenuLinePos, this.MenuLinePos+1];
			} 
		} else {
			if (this.MenuLinePos + 1 < this.CurrentMenu.Childs.length) {
				this.MenuLinePos++;
				this.MenuDispShow = [this.MenuLinePos-1, this.MenuLinePos];
			}
		}
		this.Print();
	}
	this.OnButtonUp = function() {
		if (this.CurrentMenu.IsBlockMenuCrossing) {
			if (this.MenuLinePos >= 2) {
				this.MenuLinePos-=2;
				this.MenuDispShow = [this.MenuLinePos, this.MenuLinePos+1];
			}
		} else {
			if (this.MenuLinePos > 0) {
				this.MenuLinePos--;
				this.MenuDispShow = [this.MenuLinePos, this.MenuLinePos+1];			
			}
		}
		this.Print();
	}
	this.OnButtonEnter = function() {
		var ChildMenu = this.GetChildMenu();
		if (ChildMenu.CallClass != null) {
			this.MenuWorkType = 'Function';
			this.MenuChildClass = ChildMenu.CallClass;
			Mess(this.MenuChildClass);
			this.MenuChildClass.OnEvent(this, "init", ChildMenu.CallFunctionParams);
			//eval(ChildMenu.CallClass+'(this, "init"'+((ChildMenu.CallFunctionParams.toString().length>0)?","+ChildMenu.CallFunctionParams.toString():"")+')');
			return;
		}
		if (ChildMenu.Childs.length > 0) {
			this.CurrentMenu = this.GetFilteredMenu(ChildMenu);
			this.MenuLinePos = 0;
			this.MenuDispShow[0] = this.MenuLinePos;
			this.MenuDispShow[1] = this.MenuLinePos+1;
			this.Print();
		}
	}
	this.OnButtonEsc = function() {
		var PositionInParentMenu = 0;
		if (this.CurrentMenu.Parent != null) {
			PositionInParentMenu = this.CurrentMenu.PositionInParentMenu;
			this.CurrentMenu = this.GetFilteredMenu(this.CurrentMenu.Parent);
			this.MenuLinePos = PositionInParentMenu;
			this.MenuDispShow[0] = this.MenuLinePos;
			this.MenuDispShow[1] = this.MenuLinePos+1;
			this.Print();
		}
	}
	this.OnCloseChildFunction = function() {
		this.MenuWorkType = 'Self';
	}
	this.OnCursor = function() {
		if (this.CurrentMenu.IsEnableCursor) {
			var Cursorline = (this.MenuDispShow[0] == this.MenuLinePos) ? 1 : 2;
			var Disp = (Cursorline == 1) ? disp1 : disp2;
			if (Disp.text.substr(0,1) == '_') {
				this.Print();
			} else {
				this.Print();
				Disp.text = '_'+Disp.text.substr(1);
			}
		} else {
			this.Print();
		}
	}	
// Вспомогательные функции...	
	this.GetChildMenu = function() {
		var Cursorline = (this.MenuDispShow[0] == this.MenuLinePos) ? 1 : 2;
		var ChildMenu;
		if (Cursorline == 1) {
			ChildMenu = this.CurrentMenu.Childs[this.MenuDispShow[0]];
		} else {
			ChildMenu = this.CurrentMenu.Childs[this.MenuDispShow[1]];
		}
		return ChildMenu;
	}
	this.AddMenu = function(Parent, Name, IsEnable, IsEnableCursor, IsBlockMenuCrossing, CallClass, CallFunctionParams) {
		var CurPos = Parent.Childs.length;
		Parent.Childs[CurPos] = {
			Name: Name,
			PositionInParentMenu: CurPos,
			IsEnable: IsEnable,
			IsEnableCursor: IsEnableCursor,
			IsBlockMenuCrossing: IsBlockMenuCrossing,
			CallClass: CallClass,
			CallFunctionParams: CallFunctionParams, 

			Parent: Parent,
			Childs: new Array()
		};	
		return Parent.Childs[CurPos];
	}
	this.GetMenuByName = function(Name, Menu) {
		if (Menu == null) Menu = this.MainMenu;
		for (var i = 0; i < Menu.Childs.length; i++) {
			if (Menu.Childs[i].Name == Name) {
				return Menu.Childs[i];
			}
			if (Menu.Childs[i].Childs.length > 0) {
				var res = this.GetMenuByName(Name, Menu.Childs[i]);
				if (res != null) {
					return res;
				}
			}
		}
		return null;
	}
}

TimerCursor.SetTimer('Menu.OnEvent("cursor")', 300);
function TimerCursor::OnTimer(FuncName) {
	eval(FuncName);
    TimerCursor.SetTimer('Menu.OnEvent("cursor")', 300);
}