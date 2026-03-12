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
				
				If range.Information(wdWithInTable) Then
                    Debug.Print "交叉引用在表格中: "; fld.result.text
				Else
                    Debug.Print "交叉引用不在表格中: "; fld.result.text
                End If
                
            Case wdFieldHyperlink
				' 自定义链接的书签
				parts = Split(fld.code.text, " ")
                If UBound(parts) = 2 And Left(parts(2), 4) = """ref" Then
                    Debug.Print "自定义超链接: "; parts(2)
					Dim s As String
                    s = parts(2)
                    bmName = Mid(s, 2, Len(s) - 2) & "_origin"
                    Debug.Print "字段超链接: "; Mid(s, 2, Len(s) - 2)
                    Set range = fld.result
                    doc.bookmarks.add NAME:=bmName, range:=range.Duplicate
                End If
        End Select
    Next fld
End Sub

' 获取交叉引用的地址书签
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

' 获取节的号码
Sub GetSectionNumber()
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

Sub GetPageSetupByBookmark()
    Dim bmName As String
    bmName = "ref002"

    If Not ActiveDocument.bookmarks.Exists(bmName) Then
        MsgBox "书签不存在: " & bmName
        Exit Sub
    End If

    Dim bm As Bookmark
    Set bm = ActiveDocument.bookmarks(bmName)

    Dim ps As PageSetup
    Set ps = bm.range.Sections(1).PageSetup

    Dim orient As String
    orient = IIf(ps.Orientation = wdOrientLandscape, "横向", "纵向")
    
    Dim rng As range
    Set rng = bm.range.Duplicate     ' 用副本避免修改原范围

    Dim topPos As Single
    topPos = rng.Information(wdVerticalPositionRelativeToPage)

    Dim shp As shape
    Set shp = ActiveDocument.Shapes.AddTextbox( _
        msoTextOrientationHorizontal, _
        0, 0, 34, 24, rng)
    
    With shp
        .RelativeHorizontalPosition = wdRelativeHorizontalPositionPage
        .RelativeVerticalPosition = wdRelativeVerticalPositionPage
        
        .Left = ps.pageWidth - 46  'InchesToPoints(0)
        .Top = topPos

        With .TextFrame.TextRange
            .Hyperlinks.add _
                Anchor:=.Duplicate, _
                Address:="", _
                SubAddress:="ref002_origin", _
                TextToDisplay:="返回", _
                ScreenTip:="点击返回"

            .Font.Size = 9
            .Font.Color = RGB(0, 0, 200)           
            .Font.Underline = wdUnderlineSingle    
            .ParagraphFormat.Alignment = wdAlignParagraphCenter
        End With
        ' 外观设置
        .Fill.Transparency = 1                     
        .Line.ForeColor.RGB = RGB(0, 0, 200)      
        .Line.Visible = msoFalse                    
    End With 
    
    MsgBox _
        "书签: " & bmName & vbCrLf & _
        "页面方向: " & orient & vbCrLf & _
        "宽度: " & ps.pageWidth & " pt" & vbCrLf & _
        "高度: " & ps.pageHeight & " pt"
End Sub


' Left(string, length) 从左边截取字符串, Left$ 是 Left 的字符串专用高速版本
' Mid(string, start [, length]) 从中间截取字符串， start 从 1 开始
' Right(string. length)
' Split(expression, delimiter, [limit], [compare]) 拆分字符串
' Len(string) 字符串长度
' UBound 获取数组最大下标， 数组下标 从 0 开始
' InStr([start], string, substring) 查找子串, 返回第一次出现位置(从1开始)，找不到返回 0
' UCase(string) 转换成全大写
' Replace(expression, find, replace)

' Is Noting 判断适合 对象类型
' InchesToPoints MillimetersToPoints CentimetersToPoints