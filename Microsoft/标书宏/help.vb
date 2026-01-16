
' 交叉引用 添加 书签(target_origin)
Sub CrossRefAddBookmark()
    Dim doc As Document: Set doc = ActiveDocument

    Dim fld As Field
    Dim bm As Bookmark
	Dim range As Range
	Dim bmName As String
	Dim parts() As String

    For Each fld In ActiveDocument.Fields
        Select Case fld.Type
            Case wdFieldRef
				' 交叉引用的书签
				Set range = fld.result
				bmName = GetBookmarkFromFieldCode(fld.Code.Text) & "_origin"

				' fld.result.text 				文本
				' fld.Code.Text    				" REF t_0001 \h"
				doc.Bookmarks.Add Name:=bmName, Range:=range.Duplicate
                
            Case wdFieldHyperlink
				' 自定义链接的书签
				parts = Split(fld.code.text, " ")
                If UBound(parts) = 3 Then
                    Debug.Print "自定义超链接: "; parts(3)
					Dim s As String
                    s = parts(3)
                    bmName = Mid(s, 2, Len(s) - 2) & "_origin"
                    Debug.Print "字段超链接: "; Mid(s, 2, Len(s) - 2)
                    Set range = fld.result
                    doc.bookmarks.add NAME:=bmName, range:=range.Duplicate
                End If
        End Select
    Next fld
End Sub

' 交叉引用及链接添加 返回
Sub CrossRefAddBack()

    Dim bm As Bookmark
	Dim bmName As String
	Dim bmOriginName As String
	Dim range As Range
	Dim topPos As Single

    For Each bm In ActiveDocument.Bookmarks
		bmName = bm.Name
		If Left(bmName, 3) = "ref" And InStr(bmName, "_origin") = 0 Then
			bmOriginName = bmName & "_origin"

			Dim rng As range
			Set rng = bm.Range.Duplicate     ' 用副本避免修改原范围
			' rng.Collapse wdCollapseStart
			topPos = rng.Information(wdVerticalPositionRelativeToPage)

			Dim shp As shape
			Set shp = ActiveDocument.Shapes.AddTextbox( _
				msoTextOrientationHorizontal, _
				0, 0, 24, 24, rng)
			
			' 设置形状相对界面定位
			With shp
				.RelativeHorizontalPosition = wdRelativeHorizontalPositionPage
				.RelativeVerticalPosition = wdRelativeVerticalPositionPage
				
				.Left = InchesToPoints(0)       ' 向右偏移（可自行调整0.2~0.5）
				.Top = topPos

				With .TextFrame.TextRange
					.text = "返回"
					.Font.Size = 9
					.Font.Color = RGB(0, 0, 200)           ' 文字
					.Font.Underline = wdUnderlineSingle    ' 加底
					.ParagraphFormat.Alignment = wdAlignParagraphCenter
					
					' 加入超链接 - 跳转
					.Hyperlinks.add _
						Anchor:=.Duplicate, _
						Address:="", _
						SubAddress:=bmOriginName, _
						TextToDisplay:="返回", _
						ScreenTip:="点击返回"
				End With
				' 外观设置
				.Fill.Transparency = 1                     ' 1 = 完全透明
				.Line.ForeColor.RGB = RGB(0, 0, 200)       ' 深藍色边框
				.Line.Visible = msoFalse                    ' 显示边框
				.Line.Weight = 0.25                        ' 边框粗细
			End With ' 定位到标题末尾（包含段落标记前）
		End If
    Next bm
	MsgBox "已为 交叉引用 插入返回！"
End Sub


Function GetBookmarkFromFieldCode(fieldCode As String) As String
    Dim parts() As String
    Dim bookmarkName As String
	'Word 的 Field 结构是：<空格>REF <书签名> <开关>
    ' 交叉引用域代码示例: " REF _Ref123456 \h"
    parts = Split(fieldCode, " ")
    
    If UBound(parts) > 0 Then
        bookmarkName = parts(2) ' 书签名在第三部分
    Else
        bookmarkName = ""
    End If
    
    GetBookmarkFromFieldCode = bookmarkName
End Function

Sub Get()
    Dim para As Paragraph
    For Each para In ActiveDocument.paragraphs
        If para.OutlineLevel >= wdOutlineLevel1 And para.OutlineLevel <= wdOutlineLevel3 Then
            ' 找到第一个匹配的，创建书签
            Debug.Print para.range.text
            Debug.Print para.range.Information(wdActiveEndSectionNumber)
        End If
    Next para
    
    Debug.Print "运行完毕"
End Sub

' Split(expression, delimiter, [limit], [compare]) 拆分字符串
' Left(string, length) 从左边截取字符串
' Mid(string, start [, length]) 从中间截取字符串， start 从 1 开始
' Len(string) 字符串长度
' UBound 获取数组最大下标， 数组下标 从 0 开始
' InStr([start], string, substring) 查找子串
' UCase(string) 转换成全大写
' Replace(expression, find, replace)

' Is Noting 判断适合 对象类型
