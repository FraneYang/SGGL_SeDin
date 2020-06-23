

//实现报表打开的操作
function setInit(strTab)
{
    var obChinaExcel = document.getElementById('ChinaExcel');
    obChinaExcel.SetPath('subhsse'); //http://website/test/ceuser/ceuser.dat 能否访问到
    var ret = obChinaExcel.Login("金财软件", "6c048396fdf663df323ad7d1dd6eff17", "合肥诺必达信息技术有限公司");	
    //alert(ret);
    obChinaExcel.ReadHttpFile(strTab);
}

//实现报表数据录入提交操作
function SaveData()
{
	var strData;
	var obChinaExcel=document.getElementById('ChinaExcel');
	strData = obChinaExcel.ExportSaveDBData();
	document.form1.txtSaveData.value = strData;
	if (document.form1.txtSaveData.value=="")
	{
		alert("ExportSaveDBData输出为空");
	}
	else
	{
		alert(document.form1.txtSaveData.value);
		document.form1.submit();
	}

}

//实现计算
function Calculate()
{
    var obChinaExcel=document.getElementById('ChinaExcel');
    obChinaExcel.DesignMode = false;
    obChinaExcel.ReCalculate();
//    obChinaExcel.FormProtect = true;
}

//页面加载显示报表
function OnLoad(strTab)
{
    SetInit(strTab);
    Calculate();
}

//实现报表自定义向导
function Wizard()
{
    var obChinaExcel=document.getElementById('ChinaExcel');
    obChinaExcel.UserFunctionGuide();
}

//返回最近一次计算之前的状态
function retDesgin()
{
    var obChinaExcel=document.getElementById('ChinaExcel');
    obChinaExcel.RestoreAfterCalculate();
}

//关于打印设置
function PrintSetup()
{
    var obChinaExcel=document.getElementById('ChinaExcel');
    obChinaExcel.OnPrintSetup();
}

//打印页面设置
function PrintpaperSet() {
    var obChinaExcel = document.getElementById('ChinaExcel');
    obChinaExcel.OnPrintPaperSet();
}

//单元斜线设置
function SlashSet() {
    var obChinaExcel = document.getElementById('ChinaExcel');
    obChinaExcel.OnSlashSet();
}

//打印文档
function FilePrint()
{
    var obChinaExcel=document.getElementById('ChinaExcel');
    obChinaExcel.OnFilePrint();
}

//打印预览
function Preview()
{
    var obChinaExcel=document.getElementById('ChinaExcel');
   obChinaExcel.OnFilePrintPreview();
}

//输出
function onFileSave()
{
    var obChinaExcel=document.getElementById('ChinaExcel');
   obChinaExcel.onFileSave();

}

// 剪切
function onCut() {
    var obChinaExcel = document.getElementById('ChinaExcel');
    obChinaExcel.OnCut();
}

//复制
function onPaste() {
    var obChinaExcel = document.getElementById('ChinaExcel');
    obChinaExcel.OnPaste();
}

// 粘贴
function onCopy() {
    var obChinaExcel = document.getElementById('ChinaExcel');
    obChinaExcel.OnCopy();
}

//货币符号
function onCurrency() {

}

//设置单元样式
function OnSetCellShowStyle() {
    var obChinaExcel = document.getElementById('ChinaExcel');
    obChinaExcel.OnSetCellShowStyle();
}

//设置每页打印的行数
function OnSetOnePrintPageDetailZoneRows() {
    var obChinaExcel = document.getElementById('ChinaExcel');
    nPageRows = obChinaExcel.GetOnePrintPageDetailZoneRows()
    nRow = InputBox( "说明：打印时每页显示的行数,不包括表头和表尾页脚、页前脚的行数(如果为0行,则表示没有设置每页打印的行数,系统按缺省进行分页)。 请输入每页打印的行数：", "设置每页打印的行数", nPageRows )
	if( nRow != "")
    {
        obChinaExcel.SetOnePrintPageDetailZoneRows = nRow;
    }
}

//帮助
function Help()
{
    var obChinaExcel=document.getElementById('ChinaExcel');
   obChinaExcel.AboutBox();
}

//设置单元背景色
function onSetCellBkColor() 
{
    var obChinaExcel = document.getElementById('ChinaExcel');
    obChinaExcel.OnSetCellBkColor();
}

//保存报表
function onReportSave(reportId, reportName) {
    obChinaExcel=document.getElementById('ChinaExcel');
    var tabFile = obChinaExcel.SaveDataAsZipText();

    var sendData = "reportId=" + reportId;
    sendData += "&tabContent=" + encodeURIComponent(tabFile); //二进制需encodeURIComponent编码
    sendData += "&reportName=" + reportName;

    req = Ajax();
    req.onreadystatechange = myDeal;
    req.open("POST", "SaveTabFile.aspx", "false");
    req.setRequestHeader("content-length", sendData.length);
    req.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    req.send(sendData); //要POST的数据
    obChinaExcel.SetModifiedFlag(false); //设置表格修改标识
}

function Ajax() {
    if (window.XMLHttpRequest) {
        req = new XMLHttpRequest();
    } else if (window.ActiveXObject) {
        try {
            req = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (e1) {
            try {
                req = new ActiveXObject("MSXML2.XMLHTTP");
            } catch (e2) {
                try {
                    req = new ActiveXObject("MSXML3.XMLHTTP");
                } catch (e3) {
                    alert("创建Ajax失败：" + e3);
                }
            }
        }
    } else {
        alert("未能识别的浏览器");
    }
    return req;
}

function myDeal() {
    if (req.readyState == 4) {
        var ret = req.responseText;
        if (ret != "") {
            alert(ret);
        }
    }
}

function onFileOpen() {
    var obChinaExcel = document.getElementById('ChinaExcel');
    obChinaExcel.OnFileOpen();
}

// 设置单元格的值
function SetCellValue() {
    var obChinaExcel = document.getElementById('ChinaExcel');
    obChinaExcel.SetCellValue(1, 1, '您好');

    obChinaExcel.Refresh();
}

// 给报表里设置的变量传值（变量命名规则：V_Name为变量名,如第一个变量为:V_Name1,第二个为V_Name2,以此类推）
function SetCellValUseVarName(varValue) {
    var value = varValue.split("|");
    var obChinaExcel = document.getElementById('ChinaExcel');
    for (i = 0; i < value.length; i++) {
        if (value[i] == 'NULL') {
            obChinaExcel.SetCellValUseVarName('V_Name' + (parseInt(i) + 1), '');
        }
        else {
            obChinaExcel.SetCellValUseVarName('V_Name' + (parseInt(i) + 1), value[i]);
        }
    }
    obChinaExcel.ReCalculate();
    obChinaExcel.Refresh();

}

// 替换数据源里的参数（相当于给参数传值）
function ReplaceParameter(replaceParameter) {
    var obChinaExcel = document.getElementById('ChinaExcel');
    var parameterValue = replaceParameter.split("|");
    if (parameterValue.length > 1) {
        for (i = 0; i < parameterValue.length; i++) {
            if (parameterValue[i] == 'NULL') {
                obChinaExcel.ReplaceStatScript("${参数" + (i + 1) + "}", "NULL", 1);
             }
            else {
                obChinaExcel.ReplaceStatScript("${参数" + (i + 1) + "}", "'" + parameterValue[i] + "'", 1);
            }
        }
    }
    else {
        obChinaExcel.ReplaceStatScript("${参数1}", "'" + replaceParameter + "'", 1);
    }
}

//function ReplaceParameter(replaceParameter) {
//    var obChinaExcel = document.getElementById('ChinaExcel');
//    obChinaExcel.ReplaceStatScript("${参数1}", replaceParameter, 1);
//}

//计算报表
function CalculateTab(str)      //计算报表
{
    //ReplaceParameter(strParam1);
    //var cmdCalculate=document.getElementById('cmdCalculate');
    //GetData();
    var obChinaExcel= document.getElementById('ChinaExcel');
    //var obChinaExcel= ChinaExcel;					
    //var obChinaExcel= window.parent.lefttree.document.getElementById('ChinaExcel');	
    if (obChinaExcel.DesignMode) {
        obChinaExcel.SetCanRefresh(false);
        obChinaExcel.RestoreAfterCalculate(); //会重新加载计算之前的报表模板，确保可以重新计算

        var strStatScript = obChinaExcel.GetStatScript(1);
        var dname = new Array();  //数据源名称
        var dtype = new Array();  //数据源类型
        var dSQL1 = new Array();
        var dSQL2 = new Array();
        var strData = new Array(); //数据内容
        var dcount = 0;           //数据源个数
        var i, j;
        if (strStatScript.length == 0) {
            if (window.confirm('该报表没有统计脚本，继续计算请选[Yes]，否则选[No]')) {
                obChinaExcel.SetOnlyShowTipMessage(true);
                obChinaExcel.ReCalculate();
                obChinaExcel.SetOnlyShowTipMessage(false);
            }
        }
        else {
            var strQueryParameter = strQueryParameterUrl();
            for (i = 1; i < strStatScript.length; i++)//把统计脚本中的数据源类型、名称等存入数组
            {
                dtype[i] = strStatScript.substring(strStatScript.indexOf("<cmd>") + ("<cmd>").length, strStatScript.indexOf("</cmd>")); //数据条数
                dname[i] = strStatScript.substring(strStatScript.indexOf("<dname>") + ("<dname>").length, strStatScript.indexOf("</dname>")); //字段个数
                dSQL1[i] = strStatScript.substring(strStatScript.indexOf("<sql>") + ("<sql>").length, strStatScript.indexOf("</sql>"));
                if (dtype[i] == 2)//主从报表有第二条SQL
                {
                    dSQL2[i] = strStatScript.substring(strStatScript.indexOf("<sql2>") + ("<sql2>").length, strStatScript.indexOf("</sql2>"));
                }
                strStatScript = strStatScript.substr(strStatScript.indexOf("</data>") + ("</data>").length);
                if (strStatScript.indexOf("<cmd>") < 0) {
                    dcount = i;
                    break;
                }
            }
            var url;
            for (i = 1; i <= dcount; i++)//根据数据源名重新设置取数方式
            {
                strData[i] = "";
                //by ps 2011-8-16 没有变参
                if (dSQL1[i].indexOf("${") == -1 && dSQL1[i].indexOf("${") == -1) {
                    url = "CalculateChinaEx.aspx?func=GetSqlResult&dtype=" + dtype[i] + "&sql=" + encodeURIComponent(dSQL1[i]) + "&sql2=" + encodeURIComponent(dSQL2[i]) + "&reportId=" + str;
                } else {
                    url = "CalculateChinaEx.aspx?func=GetSqlResult&dtype=" + dtype[i] + "&sql=" + encodeURIComponent(dSQL1[i]) + "&sql2=" + encodeURIComponent(dSQL2[i]) + "&reportId=" + str + strQueryParameter;
                }
                obChinaExcel.SetStatDataSource1(url, 2, dname[i]);
            }

            obChinaExcel.SetOnlyShowTipMessage(true);
            obChinaExcel.ReCalculate();

            obChinaExcel.SetOnlyShowTipMessage(false);
        }
        obChinaExcel.SetCanRefresh(true);
        obChinaExcel.Refresh();
    }
}

function CalculateTabPaging(str, str2)      //计算报表
{
    var obChinaExcel = document.getElementById('ChinaExcel');
    obChinaExcel.SetCanRefresh(false);


    var pagesize = null;
    if (str2 == "库存查询每页显示固定行") {
        pagesize = document.getElementById("Text3").value;

    }
    else {
        pagesize = 20;
    }
    var pageIndex = document.getElementById("cpage").value;
    obChinaExcel.RestoreAfterCalculate(); //会重新加载计算之前的报表模板，确保可以重新计算

    var strStatScript = obChinaExcel.GetStatScript(1);
    //alert(strStatScript);
    var dname = new Array();  //数据源名称
    var dtype = new Array();  //数据源类型
    var dSQL1 = new Array();
    var dSQL2 = new Array();
    var strData = new Array(); //数据内容
    var dcount = 0;           //数据源个数
    var i, j;
    if (strStatScript.length == 0) {
        if (window.confirm('该报表没有统计脚本，继续计算请选[Yes]，否则选[No]')) {
            obChinaExcel.SetOnlyShowTipMessage(true);
            obChinaExcel.ReCalculate();
            obChinaExcel.SetOnlyShowTipMessage(false);
        }
    }
    else {
        var strQueryParameter = strQueryParameterUrl();
        for (i = 1; i < strStatScript.length; i++)//把统计脚本中的数据源类型、名称等存入数组
        {
            dtype[i] = strStatScript.substring(strStatScript.indexOf("<cmd>") + ("<cmd>").length, strStatScript.indexOf("</cmd>")); //数据条数
            dname[i] = strStatScript.substring(strStatScript.indexOf("<dname>") + ("<dname>").length, strStatScript.indexOf("</dname>")); //字段个数
            dSQL1[i] = strStatScript.substring(strStatScript.indexOf("<sql>") + ("<sql>").length, strStatScript.indexOf("</sql>"));
            if (dtype[i] == 2)//主从报表有第二条SQL
            {
                dSQL2[i] = strStatScript.substring(strStatScript.indexOf("<sql2>") + ("<sql2>").length, strStatScript.indexOf("</sql2>"));
            }
            strStatScript = strStatScript.substr(strStatScript.indexOf("</data>") + ("</data>").length);
            if (strStatScript.indexOf("<cmd>") < 0) {
                dcount = i;
                break;
            }
        }
        var url;
        for (i = 1; i <= dcount; i++)//根据数据源名重新设置取数方式
        {
            strData[i] = "";
            //没有变参
            if (dSQL1[i].indexOf("${") == -1 && dSQL1[i].indexOf("${") == -1) {
                url = "CalculateChinaExPaging.aspx?func=GetSqlResult&dtype=" + dtype[i] + "&sql=" + encodeURIComponent(dSQL1[i]) + "&sql2=" + encodeURIComponent(dSQL2[i]) + "&pageSize=" + pagesize + "&pageIndex=" + pageIndex + "&nodeid=" + str;
            } else {
                url = "CalculateChinaExPaging.aspx?func=GetSqlResult&dtype=" + dtype[i] + "&sql=" + encodeURIComponent(dSQL1[i]) + "&sql2=" + encodeURIComponent(dSQL2[i]) + "&pageSize=" + pagesize + "&pageIndex=" + pageIndex + "&nodeid=" + str + strQueryParameter;
            }
            obChinaExcel.SetStatDataSource1(url, 2, dname[i]);

        }

        obChinaExcel.SetOnlyShowTipMessage(true);
        obChinaExcel.ReCalculate();
        obChinaExcel.SetOnlyShowTipMessage(false);
    }
    obChinaExcel.SetCanRefresh(true);
    obChinaExcel.Refresh();

}

function strQueryParameterUrl() {
    var i, j, nCount, bSameName;
    var strTempA = "";
    var strTempB = "";
    var dParam = new Array();  //变参数组
    nCount = 0;
    var obChinaExcel = document.getElementById('ChinaExcel');
    for (i = 1; i <= obChinaExcel.GetMaxRow(); i++) {
        for (j = 1; j <= obChinaExcel.GetMaxCol(); j++) {
            strTempA = obChinaExcel.GetCellQueryParameter(i, j);
            if (strTempA != "") {
                bSameName = false;
                for (k = 1; k <= nCount; k++) {
                    if (dParam[k] == strTempA) {
                        bSameName = true;
                        break;
                    }
                }
                if (bSameName == false) {
                    nCount++;
                    dParam[nCount] = strTempA;
                }
            }
        }
    }
    for (k = 1; k <= nCount; k++) {
        strTempB = strTempB + "&" + encodeURIComponent(dParam[k]) + "=" + "${" + dParam[k] + "}";
    }
    return strTempB;
}

// 自定义字段（指数据源字段）
function DefineField(strFieldName,strFieldName1) {
    var obChinaExcel = document.getElementById('ChinaExcel');
    var strFieldDefine;

    strFieldDefine = "<data><cmd>6</cmd><stattype>1</stattype><type>2</type><fieldname>" + strFieldName + "</fieldname><insertflag>0</insertflag><showcontent>1</showcontent></data>";
    obChinaExcel.SetCellStatDefine(obChinaExcel.Row, obChinaExcel.Col, strFieldDefine);
//    strFieldDefine = "<data><cmd>6</cmd><stattype>1</stattype><type>2</type><fieldname>" + strFieldName1 + "</fieldname><insertflag>0</insertflag><showcontent>1</showcontent></data>";
//    obChinaExcel.SetCellStatDefine(obChinaExcel.Row, obChinaExcel.Col+1, strFieldDefine);
    obChinaExcel.Refresh(); 
}

function onCbClickEvent(obj, fNoEvent) {
    if (null != event) {
        event.cancelBubble = true;
    }
    // Regular push button
    onCbClick(obj.id, true);
    return (false);
}

function onCbClick(szCommand, fState) {
    //开始命令
    switch (szCommand.toUpperCase()) {
        case "CMDFILENEW": //新建
            mnuFileNew_click();
            break;
        case "CMDFILEOPEN": //打开文件
            mnuFileOpen_click();
            break;
        case "CMDEXCELFILEOPEN": //打开EXCEL文件
            mnuExcelFileOpen_click();
            break;
        case "CMDWEBFILEOPEN": //打开远程文件
            mnuFileWebOpen_click();
            break;
        case "CMDWEBXMLFILEOPEN": //打开远程XML文件
            mnuXMLFileWebOpen_click();
            break;
        case "CMDSAVEDATAASSTRING": //输出为字符串
            mnuSaveDataAsString_click();
            break;
        case "CMDFILESAVE": //保存文档
            SaveTabFile1(); //保存到服务器 tcf
            break;
        case "CMDFILESAVEAS": //另存为
            mnuFileSaveAs_click();
            break;
        case "CMDFILEPRINTPAPERSET": //打印页设置文档
            mnuPrintPaperSet_click();
            break;
        case "CMDFILEPRINTSETUP": //打印设置文档
            mnuFilePrintSetup_click();
            break;
        case "CMDFILEPRINT": //打印文档
            mnuFilePrint_click();
            break;
        case "CMDFILEPRINTPREVIEW": //打印预览文档
            mnuFilePrintPreview_click();
            break;
        case "CMDEDITCUT": //剪切
            ChinaExcel.OnCut();
            //mnuEditCut_click();
            break;
        case "CMDEDITCOPY": //复制
            ChinaExcel.OnCopy();
            //mnuEditCopy_click();
            break;
        case "CMDEDITPASTE": //粘贴
            ChinaExcel.OnPaste();
            //mnuEditPaste_click();
            break;
        case "CMDEDITFIND": //查找替换
            mnuEditFind_click();
            break;
        case "CMDEDITUNDO": //撤消
            //mnuEditUndo_click();
            break;
        case "CMDEDITREDO": //重做
            //mnuEditRedo_click();
            break;
        case "CMDSHAPE3D": //设置单元3维显示
            mnuShape3D_click();
            break;
        case "CMDROWLABEL": //设置行表头
            mnuRowLabel_click();
            break;
        case "CMDCOLLABEL": //设置列表头
            mnuColLabel_click();
            break;
        case "CMDSTATWIZARD": //
            ChinaExcel.OnStatWebWizard();
            break;
        case "CMDSORTDESCENDING": //降序排序
            cmdSortDescending_click();
            break;
        case "CMDFUNCTIONLIST": //函数列表
            mnuFunctionList_click();
            break;
        case "CMDUSERFUNCTIONGUIDE": //自定义函数向导
            mnuUserFunctionGuide_click();
            break;
        case "CMDFORMULASUMH": //水平求和
            cmdFormulaSumH_click();
            break;
        case "CMDFORMULASUMV": //垂直求和
            cmdFormulaSumV_click();
            break;
        case "CMDFORMULASUMHV": //双向求和
            cmdFormulaSumHV_click();
            break;
        case "CMDCHARTWZD": //图表向导
            mnuDataWzdChart_click();
            break;
        case "CMDINSERTPIC": //插入图片
            mnuFormatInsertPic_click();
            break;
        case "CMDINSERTCELLPIC": //插入单元图片
            mnuFormatInsertCellPic_click();
            break;
        case "CMDHYPERLINK": //超级链接
            mnuEditHyperlink_click();
            break;
        case "CMDFINANCEHEADERTYPE": //财务表头
            mnuFinanceHeader_click();
            break;
        case "CMDFINANCETYPE": //财务表览
            mnuFinance_click();
            break;
        case "CMDSHOWGRIDLINE": //显示/隐藏背景表格线
            with (ChinaExcel) {
                ShowGrid = !ShowGrid;
            }
            break;
        case "CMDSHOWHEADER": //显示/隐藏系统表头
            with (ChinaExcel) {
                ShowHeader = !ShowHeader;
            }
            break;
        //***********************************************************			 
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ 
        case "CMDBOLD": //设置粗体
            cmdBold_click();
            break;
        case "CMDITALIC": //设置斜体
            cmdItalic_click();
            break;
        case "CMDUNDERLINE": //设置下划线
            cmdUnderline_click();
            break;
        case "CMDBACKCOLOR": //设置背景色
            cmdBackColor_click();
            break;
        case "CMDFORECOLOR": //设置前景色
            cmdForeColor_click();
            break;
        case "CMDWORDWRAP": //设置自动折行
            cmdWordWrap_click();
            break;
        case "CMDALIGNLEFT": //左对齐
            cmdAlignLeft_click();
            break;
        case "CMDALIGNCENTER": //居中对齐
            cmdAlignCenter_click();
            break;
        case "CMDALIGNRIGHT": //居右对齐
            cmdAlignRight_click();
            break;
        case "CMDALIGNTOP": //居上对齐
            cmdAlignTop_click();
            break;
        case "CMDALIGNMIDDLE": //垂直居中对齐
            cmdAlignMiddle_click();
            break;
        case "CMDALIGNBOTTOM": //居下对齐
            cmdAlignBottom_click();
            break;
        case "CMDDRAWBORDER": //画框线
            cmdDrawBorder_click();
            break;
        case "CMDERASEBORDER": //抹框线
            cmdEraseBorder_click();
            break;
        case "CMDCURRENCY": //货币符号
            cmdCurrency_click();
            break;
        case "CMDPERCENT": //百分号
            cmdPercent_click();
            break;
        case "CMDTHOUSAND": //千分位
            cmdThousand_click();
            break;
        case "CMDABOUT": //关于超级报表插件
            cmdAbout_click();
            break;
        //***********************************************************			 
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ 
        case "CMDINSERTCOL": //插入列
            cmdInsertCol_click();
            break;
        case "CMDINSERTROW": //插入行
            cmdInsertRow_click();
            break;
        case "CMDINSERTCELL": //插入单元
            cmdInsertCell_click();
            break;
        case "CMDDELETECELL": //删除单元
            cmdDeleteCell_click();
            break;
        case "CMDDELETECOL": //删除列
            cmdDeleteCol_click();
            break;
        case "CMDDELETEROW": //删除行
            cmdDeleteRow_click();
            break;
        case "CMDMAXROWCOL": //设置表格行列数
            mnuMaxRowCol_click();
            break;
        case "CMDMERGECELL": //合并单元格
            mnuFormatMergeCell_click();
            break;
        case "CMDUNMERGECELL": //取消合并单元格
            mnuFormatUnMergeCell_click();
            break;
        case "CMDMERGEROW": //行组合
            cmdMergeRow_click();
            break;
        case "CMDMERGECOL": //列组合
            cmdMergeCol_click();
            break;
        case "CMDRECALCALL": //重算全表
            mnuFormulaReCalc_click();
            break;
        case "CMDFORMPROTECT": //整表保护
            mnuFormProtect_click();
            break;
        case "CMDREADONLY": //单元格只读
            mnuReadOnly_click();
            break;

        case "SETONEPRINTPAGEDETAILZONEROWS": //设置每页打印的行数
            mnuSetOnePrintPageDetailZoneRows_click();
            break;

    }
}
