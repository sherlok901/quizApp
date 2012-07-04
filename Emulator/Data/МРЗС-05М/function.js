function Mess(str) {
    Debug.Print(str);
}

String.prototype.repeat = function(l){
	return new Array(l+1).join(this);
}

Array.prototype.inArray = function (value) {
	for (var i=0; i < this.length; i++) {
		if (this[i] === value) {
			return i;
		}
	}
	return -1;
};

Array.prototype.insert = function(index, value){
	this.splice(index,0,value);
}


Array.prototype.remove = function(index){
	this.splice(index,1);
}

