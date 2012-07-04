function btnR01::OnClick() 					{ SomeCheckClick(btnR01, 'btnR01check', 'check'); }
function btnR02::OnClick() 					{ SomeCheckClick(btnR02, 'btnR02check', 'check'); }
function btnR03::OnClick() 					{ SomeCheckClick(btnR03, 'btnR03check', 'check'); }
function btnR04::OnClick() 					{ SomeCheckClick(btnR04, 'btnR04check', 'check'); }
function btnR05::OnClick() 					{ SomeCheckClick(btnR05, 'btnR05check', 'check'); }

function btnDV01::OnClick() 				{ SomeCheckClick(btnDV01, 'btnDV01check', 'checkr'); }
function btnDV02::OnClick() 				{ SomeCheckClick(btnDV02, 'btnDV02check', 'checkr'); }
function btnDV03::OnClick() 				{ SomeCheckClick(btnDV03, 'btnDV03check', 'checkr'); }
function btnDV04::OnClick() 				{ SomeCheckClick(btnDV04, 'btnDV04check', 'checkr'); }
function btnDV05::OnClick() 				{ SomeCheckClick(btnDV05, 'btnDV05check', 'checkr'); }
function btnDV06::OnClick() 				{ SomeCheckClick(btnDV06, 'btnDV06check', 'checkr'); }

function DeviceIndicator::OnClick() 		{ SomeCheckClick(DeviceIndicator, 'DeviceIndicatorcheck', 'lamp_on', 'bmp'); }
function SDIIndicator1::OnClick() 			{ SomeCheckClick(SDIIndicator1, 'SDIIndicator1check', 'lamp_on', 'bmp'); }
function SDIIndicator2::OnClick() 			{ SomeCheckClick(SDIIndicator2, 'SDIIndicator2check', 'lamp_on', 'bmp'); }
function SDIIndicator3::OnClick() 			{ SomeCheckClick(SDIIndicator3, 'SDIIndicator3check', 'lamp_on', 'bmp'); }
function SDIIndicator4::OnClick() 			{ SomeCheckClick(SDIIndicator4, 'SDIIndicator4check', 'lamp_on', 'bmp'); }
function SDIIndicator5::OnClick() 			{ SomeCheckClick(SDIIndicator5, 'SDIIndicator5check', 'lamp_on', 'bmp'); }
function SDIIndicator6::OnClick() 			{ SomeCheckClick(SDIIndicator6, 'SDIIndicator6check', 'lamp_on', 'bmp'); }

function btn1::OnClick() 					{ Menu.OnEvent("1");			blinc(btn1, 	'1',		false);}
function btn2::OnClick() 					{ Menu.OnEvent("2");			blinc(btn2, 	'2',		false);}
function btn3::OnClick() 					{ Menu.OnEvent("3");			blinc(btn3, 	'3',		false);}
function btn4::OnClick() 					{ Menu.OnEvent("4");			blinc(btn4, 	'4',		false);}
function btn5::OnClick() 					{ Menu.OnEvent("5");			blinc(btn5, 	'5',		false);}		
function btn6::OnClick() 					{ Menu.OnEvent("6");			blinc(btn6, 	'6',		false);}
function btn7::OnClick() 					{ Menu.OnEvent("7");			blinc(btn7, 	'7',		false);}
function btn8::OnClick() 					{ Menu.OnEvent("8");			blinc(btn8, 	'8',		false);}
function btn9::OnClick() 					{ Menu.OnEvent("9");			blinc(btn9, 	'9',		false);}
function btn0::OnClick() 					{ Menu.OnEvent("0");			blinc(btn0, 	'0',		false);}

function btnDot::OnClick() 					{ Menu.OnEvent("dot");			blinc(btnDot, 	'dot',		false);}
function btnF::OnClick() 					{ Menu.OnEvent("f"); OnClickF();blinc(btnF, 	'f',		false);}

function btnEnter::OnClick() 				{ Menu.OnEvent("enter");		blinc(btnEnter, 	'enter',	false);}
function btnEsc::OnClick() 					{ Menu.OnEvent("esc");			blinc(btnEsc, 	'esc',		false);}

function btnBOTTOM::OnClick() 				{ Menu.OnEvent("down"); 		blinc(btnBOTTOM, 	'bottom',	false);}
function btnUP::OnClick() 					{ Menu.OnEvent("up"); 			blinc(btnUP, 		'up',		false);}
function btnLEFT::OnClick() 				{ Menu.OnEvent("left"); 		blinc(btnLEFT, 		'left',		false);}
function btnRIGHT::OnClick() 				{ Menu.OnEvent("right"); 		blinc(btnRIGHT, 	'right',	false);}

function UpDownF::OnClick(ClickType)		{ SomeUpDownClick('UpDownF', 	ClickType); }
function UpDownZIO::OnClick(ClickType)		{ SomeUpDownClick('UpDownZIO', 	ClickType); }
                                                              
function UpDownIaA::OnClick(ClickType)		{ SomeUpDownClick('UpDownIaA', 	ClickType); }
function UpDownIbA::OnClick(ClickType)		{ SomeUpDownClick('UpDownIbA', 	ClickType); }
function UpDownIcA::OnClick(ClickType)		{ SomeUpDownClick('UpDownIcA', 	ClickType); }
function UpDownIzfA::OnClick(ClickType)		{ SomeUpDownClick('UpDownIzfA', ClickType); }
                                                              
function UpDownIaO::OnClick(ClickType)		{ SomeUpDownClick('UpDownIaO', 	ClickType); }
function UpDownIbO::OnClick(ClickType)		{ SomeUpDownClick('UpDownIbO', 	ClickType); }
function UpDownIcO::OnClick(ClickType)		{ SomeUpDownClick('UpDownIcO', 	ClickType); }
function UpDownIzfO::OnClick(ClickType)		{ SomeUpDownClick('UpDownIzfO', ClickType); }
                                                              
function UpDownUaB::OnClick(ClickType)		{ SomeUpDownClick('UpDownUaB', 	ClickType); }
function UpDownUbB::OnClick(ClickType)		{ SomeUpDownClick('UpDownUbB', 	ClickType); }
function UpDownUcB::OnClick(ClickType)		{ SomeUpDownClick('UpDownUcB', 	ClickType); }
function UpDownUzfB::OnClick(ClickType)		{ SomeUpDownClick('UpDownUzfB', ClickType); }
                                                              
function UpDownUaO::OnClick(ClickType)		{ SomeUpDownClick('UpDownUaO', 	ClickType); }
function UpDownUbO::OnClick(ClickType)		{ SomeUpDownClick('UpDownUbO', 	ClickType); }
function UpDownUcO::OnClick(ClickType)		{ SomeUpDownClick('UpDownUcO', 	ClickType); }
function UpDownUzfO::OnClick(ClickType)		{ SomeUpDownClick('UpDownUzfO', ClickType); }

function EditF::OnClick(ClickType)			{ SomeEditClick('EditF'); }
function EditZIO::OnClick(ClickType)		{ SomeEditClick('EditZIO'); }
                                                              
function EditIaA::OnClick(ClickType)		{ SomeEditClick('EditIaA'); }
function EditIbA::OnClick(ClickType)		{ SomeEditClick('EditIbA'); }
function EditIcA::OnClick(ClickType)		{ SomeEditClick('EditIcA'); }
function EditIzfA::OnClick(ClickType)		{ SomeEditClick('EditIzfA'); }
                                                              
function EditIaO::OnClick(ClickType)		{ SomeEditClick('EditIaO'); }
function EditIbO::OnClick(ClickType)		{ SomeEditClick('EditIbO'); }
function EditIcO::OnClick(ClickType)		{ SomeEditClick('EditIcO'); }
function EditIzfO::OnClick(ClickType)		{ SomeEditClick('EditIzfO'); }
                                                              
function EditUaB::OnClick(ClickType)		{ SomeEditClick('EditUaB'); }
function EditUbB::OnClick(ClickType)		{ SomeEditClick('EditUbB'); }
function EditUcB::OnClick(ClickType)		{ SomeEditClick('EditUcB'); }
function EditUzfB::OnClick(ClickType)		{ SomeEditClick('EditUzfB'); }
                                                              
function EditUaO::OnClick(ClickType)		{ SomeEditClick('EditUaO'); }
function EditUbO::OnClick(ClickType)		{ SomeEditClick('EditUbO'); }
function EditUcO::OnClick(ClickType)		{ SomeEditClick('EditUcO'); }
function EditUzfO::OnClick(ClickType)		{ SomeEditClick('EditUzfO'); }

function EditF::OnChange()					{ SomeEditChange('EditF'); }
function EditZIO::OnChange()				{ SomeEditChange('EditZIO'); }
                                                                      
function EditIaA::OnChange()				{ SomeEditChange('EditIaA'); }
function EditIbA::OnChange()				{ SomeEditChange('EditIbA'); }
function EditIcA::OnChange()				{ SomeEditChange('EditIcA'); }
function EditIzfA::OnChange()				{ SomeEditChange('EditIzfA'); }
                                                                      
function EditIaO::OnChange()				{ SomeEditChange('EditIaO'); }
function EditIbO::OnChange()				{ SomeEditChange('EditIbO'); }
function EditIcO::OnChange()				{ SomeEditChange('EditIcO'); }
function EditIzfO::OnChange()				{ SomeEditChange('EditIzfO'); }
                                                                      
function EditUaB::OnChange()				{ SomeEditChange('EditUaB'); }
function EditUbB::OnChange()				{ SomeEditChange('EditUbB'); }
function EditUcB::OnChange()				{ SomeEditChange('EditUcB'); }
function EditUzfB::OnChange()				{ SomeEditChange('EditUzfB'); }
                                                                      
function EditUaO::OnChange()				{ SomeEditChange('EditUaO'); }
function EditUbO::OnChange()				{ SomeEditChange('EditUbO'); }
function EditUcO::OnChange()				{ SomeEditChange('EditUcO'); }
function EditUzfO::OnChange()				{ SomeEditChange('EditUzfO'); }
