
var jTimerId;
	function SetProgressTimeOut(baseController)
	{
	   showProgressFrame(document.title, 'InterfaceProgressForm.aspx');
		
		jTimerId = setInterval("VerifyProgressTimeOut('" + baseController + "')", 8000);
	}
	
	function VerifyProgressTimeOut(baseController)
	{
		if (document.all(baseController + "moInterfaceProgressControl_moInterfaceStatus").value != "")
		{
			if (document.all(baseController + "moInterfaceProgressControl_moInterfaceStatus").value != "SUCCESS")
			{
			    var btn = document.getElementById(baseController + "moInterfaceProgressControl_btnErrorHidden");
				btn.click();
			}
			else
			{
				document.all(baseController + "moInterfaceProgressControl_moInterfaceStatus").value = "";
			}

			closeProgressFrame();
			buttonAfterProgressBar(baseController);        }
	}
	
	function EnableInterfaceProgress(baseController)
	{
	    var btn = document.getElementById(baseController + "moInterfaceProgressControl_btnViewHidden");
	    btn.click();
			
	}
	
	function ShowInterfaceContainer()
	{
		document.all.item('iframe1').style.display = '';
		document.all.item('iframe1').style.visibility = 'visible';
		document.all.item('iframe1').src = "../Interfaces/InterfaceBaseForm.aspx";
	}
	
	function HideInterfaceContainer()
	{
		document.all.item('iframe1').style.display = 'none';
		document.all.item('iframe1').style.visibility = 'hidden';
		document.all.item('iframe1').src = "";
	}
	
	
	function buttonAfterProgressBar(baseController)
	{
	    var btn = document.getElementById(baseController + "btnAfterProgressBar");
		btn.click();
		return true;
	}
	
	
