Public Sub mnuFileNew_click()
	If ChinaExcel.IsModified() Then '文档已经被更改
		rtn = MsgBox( "文档已被更改，是否保存？", vbExclamation Or vbYesNoCancel)
		If rtn = vbYes Then
			mnuFileSave_click
		ElseIf rtn = vbCancel Then
			Exit Sub
		End If
	End If
	ChinaExcel.SetMaxRows(0)
	ChinaExcel.SetMaxRows(18)
	ChinaExcel.SetMaxCols(8)
	ChinaExcel.FormProtect = false
	'menu_init
End Sub

'超级链接
Public Sub mnuEditHyperlink_click()
	strUrl = InputBox( "请输入超级链接地址：", "超级链接", "HTTP://" )
	ChinaExcel.SetCellURLType ChinaExcel.Row,ChinaExcel.Col,strUrl
End Sub

'设置粗体
Public Sub cmdBold_click()
	ChinaExcel.Bold = not ChinaExcel.Bold
End Sub

'设置斜体
Public Sub cmdItalic_click()
	ChinaExcel.Italic = not ChinaExcel.Italic
End Sub

'设置下划线
Public Sub cmdUnderline_click()
	ChinaExcel.Underline = not ChinaExcel.Underline
End Sub

'设置背景色
Public Sub cmdBackColor_click()
	ChinaExcel.OnSetCellBkColor
End Sub

'设置前景色
Public Sub cmdForeColor_click()
	ChinaExcel.OnSetTextColor
End Sub

'自动折行
Public Sub cmdWordWrap_click()
	ChinaExcel.AutoWrap = not ChinaExcel.AutoWrap
	nMenuID = MenuOcx.GetMenuID("AutoWrap")
	MenuOcx.SetMenuChecked nMenuID,ChinaExcel.AutoWrap
End Sub

'居左对齐
Public Sub cmdAlignLeft_click()
	ChinaExcel.HorzTextAlign = 1
End Sub

'居中对齐
Public Sub cmdAlignCenter_click()
	ChinaExcel.HorzTextAlign = 2
End Sub

'居右对齐
Public Sub cmdAlignRight_click()
	ChinaExcel.HorzTextAlign = 3
End Sub

'居上对齐
Public Sub cmdAlignTop_click()
	ChinaExcel.VertTextAlign = 1
End Sub

'垂直居中对齐
Public Sub cmdAlignMiddle_click()
	ChinaExcel.VertTextAlign = 2
End Sub

'居下对齐
Public Sub cmdAlignBottom_click()
	ChinaExcel.VertTextAlign = 3
End Sub

'画边框线
Public Sub cmdDrawBorder_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		.DrawCellBorder  StartRow, StartCol, EndRow, EndCol, BorderTypeSelect.value, 0,0
        End With
End Sub

'抹框线
Public Sub cmdEraseBorder_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		.ClearCellBorder  StartRow, StartCol, EndRow, EndCol,0
        End With
End Sub

'货币符号
Public Sub cmdCurrency_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		.SetCellDigitShowStyle StartRow, StartCol, EndRow, EndCol,2,2
    End With
End Sub

'百分号
Public Sub cmdPercent_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		.SetCellDigitShowStyle StartRow, StartCol, EndRow, EndCol,4,2
	End With
End Sub

'千分位
Public Sub cmdThousand_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		.SetCellDigitShowStyle StartRow, StartCol, EndRow, EndCol,5,2
	End With
End Sub

'关于超级报表插件
Public Sub cmdAbout_click()
	ChinaExcel.AboutBox
End Sub

'插入列
Public Sub cmdInsertCol_click()
	ChinaExcel.OnInsertBeforeCol
End Sub

'插入行
Public Sub cmdInsertRow_click()
	ChinaExcel.OnInsertBeforeRow
End Sub

'插入单元
Public Sub cmdInsertCell_click()
	ChinaExcel.OnInsertCell
End Sub

'删除单元
Public Sub cmdDeleteCell_click()
	ChinaExcel.OnDeleteCell
End Sub

'删除列
Public Sub cmdDeleteCol_click()
	ChinaExcel.OnDeleteCol
End Sub

'删除行
Public Sub cmdDeleteRow_click()
	ChinaExcel.OnDeleteRow
End Sub


'水平求和
Public Sub cmdFormulaSumH_click()
	With ChinaExcel
		StartCol = 0: StartRow = 0: EndCol = 0: EndRow = 0
		.GetSelectRegionWeb StartRow,StartCol,EndRow,EndCol
		.AutoSum StartRow,StartCol,EndRow,EndCol,2
	End With
End Sub

'垂直求和
Public Function InStrL(inString, srchString)
                                    '此函数用于查询srchString子字串在父字串inString中的最后一个位置
    If srchString = "" Then
        InStrL = 0
        Exit Function
    End If
    If Len(srchString) Then
        Do
            iLastPos = iCurPos
            iCurPos = InStr(iCurPos + 1, inString, srchString, vbTextCompare)
        Loop Until iCurPos = 0
    End If
    InStrL = iLastPos
End Function

Public Function StrGetSinglePara(ByVal strCellPara, ByVal strCharacter)
                    '传入单元参数strCellPara和特征字串strCharacter,函数可返回特征字串中的字符串值

    strChar1 = "<" & Trim(strCharacter) & ">"
    strChar2 = "</" & Trim(strCharacter) & ">"
    iStart = InStrL(strCellPara, strChar1)
    iEnd = InStrL(strCellPara, strChar2)
    If iStart > 0 And iEnd > iStart Then
        iCharacterLen = Len(Trim(strCharacter)) + 2
        iStart = iStart + iCharacterLen
        StrGetSinglePara = Trim(Mid(strCellPara, iStart, iEnd - iStart))
    Else
        StrGetSinglePara = ""
    End If
End Function

Public Function GetCellDefineValue(ByVal nRow,ByVal nCol)
    strCellPara = ChinaExcel.GetCellStatDefine(nRow, nCol)
    If Trim(strCellPara) <> "" Then
        strFldName = StrGetSinglePara(strCellPara, "fieldname")
    else
        strFldName=""
    end if
    GetCellDefineValue=strFldName
End Function

Public Function GetCellColName(nRow, nCol)
    strName = ChinaExcel.GetCellName(nRow,nCol)
    strNameA=""
    
    for iCount=1 to Len(strName)
        If Not IsNumeric(Mid(strName,iCount, 1)) Then
            strNameA = strNameA & Mid(strName,iCount, 1)
        else
            exit for
        End If
    next
    GetCellColName = strNameA
End Function


Public Sub cmdFormulaSumV_click()
	With ChinaExcel
'		StartCol = 0: StartRow = 0: EndCol = 0: EndRow = 0
'		.GetSelectRegionWeb StartRow,StartCol,EndRow,EndCol
'		.AutoSum StartRow,StartCol,EndRow,EndCol,1
        nRow=.Row
        nCol=.Col
'使用字段进行求和
'        strValue=GetCellDefineValue(nRow,nCol)
'        if strValue="" then
'            msgbox "没有找到定义的字段名"
'            exit sub
'        end if
'        .SetCellShowVal nRow+1,nCol,"=sum(@"+strValue+")"
'使用公式求和
        strValue=GetCellDefineValue(nRow,nCol)
        if strValue="" then
            msgbox "没有找到定义的字段名"
            exit sub
        end if
        strValueA=GetCellColName(nRow,nCol)
        if strValueA="" then
            msgbox "获取列名错误"
            exit sub
        end if
        qs=msgbox("列名求和选[YES],字段名求和选[NO]",vbYesNo + vbQuestion,"询问")
        if qs=vbyes then
            .SetCellShowVal nRow+1,nCol,"=sum(" & strValueA & nRow & ":" & strValueA & "0)"
        else
            .SetCellShowVal nRow+1,nCol,"=sum(@" & strValue & ")"
        end if
	End With
End Sub

'双向求和
Public Sub cmdFormulaSumHV_click()
	With ChinaExcel
		StartCol = 0: StartRow = 0: EndCol = 0: EndRow = 0
		.GetSelectRegionWeb StartRow,StartCol,EndRow,EndCol
		.AutoSum StartRow,StartCol,EndRow,EndCol,3
	End With
End Sub


'图表向导
Public Sub mnuDataWzdChart_click()
	ChinaExcel.OnChartWizard
End Sub

'设置图片为原始大小
Public Sub mnuSetCellImageOriginalSize_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		for row = StartRow to EndRow
			for col = StartCol to EndCol
				.SetCellImageSize row,col,1		
			next
		next
		.Refresh
	End With
End Sub

'设置图片为单元大小
Public Sub mnuSetCellImageCellSize_click()
	With ChinaExcel
		.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
		for row = StartRow to EndRow
			for col = StartCol to EndCol
				.SetCellImageSize row,col,0		
			next
		next
		.Refresh
	End With
End Sub

'删除图片
Public Sub mnuDeleteCellImage_click()
	ChinaExcel.GetSelectRegionWeb StartRow, StartCol, EndRow, EndCol
	ChinaExcel.DeleteCellImage StartRow, StartCol, EndRow, EndCol
End Sub

'设置每页打印的行数
Public Sub mnuSetOnePrintPageDetailZoneRows_click()
	nPageRows = ChinaExcel.GetOnePrintPageDetailZoneRows()
	nRow = InputBox( "说明：打印时每页显示的行数,不包括表头和表尾页脚、页前脚的行数(如果为0行,则表示没有设置每页打印的行数,系统按缺省进行分页)。 请输入每页打印的行数：", "设置每页打印的行数", nPageRows )
	If nRow <> "" Then ChinaExcel.SetOnePrintPageDetailZoneRows nRow
End Sub


'*****************************************************************
'**********      下拉列表框中的事件
'*****************************************************************
'设置字体
Public Sub changeFontName( ByVal value )
    With ChinaExcel
        lFontName = value
        .CellFontName = lFontName
    End With
End Sub

'设置字号
Public Sub changeFontSize( ByVal value )
    With ChinaExcel
        lFontSize = value
		.CellFontSize = lFontSize
    End With

End Sub
