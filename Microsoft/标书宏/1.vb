' 文档存在横竖混排，表格跨页等结构
' 1. 目录添加 toc 书签
' 2. 标题样式创建书签 h_index (针对 标题 1 2 3 样式创建全局索引，方便后续的前后查找)
' 3. 标题样式后创建 前 后 链接跳转(跳转到最近的标题)
' 4. 交叉引用，文本超链接(段落中文本及图片) 
' 	4.1 正文：每个交叉引用的新创建一个书签叫 "ref目标书签"
' 	4.2 表格
' 5. 手动实现分节(基于是混合排版)，每节页脚添加 上一节 目录 下一个 三个链接
' 页脚要关闭 首页不同,奇偶页不同，链接到前一节

' 记录标题的书签名
Dim HeadingBookmarkMap As Object  ' Scripting.Dictionary
Dim sectionPara() As String ' 记录每节中第一个出现的标题 1-3 的书签

Sub Main()
  	Dim doc As Document: Set doc = ActiveDocument 
	Dim bms As Bookmarks: Set bms = doc.Bookmarks

  	Application.ScreenUpdating = False

	' 创建目录书签
  	CreateTOCBookmark
	' 创建标题书签
  	CreateHeadingsBookmarks

	' 添加交叉引用及文本链接书签
    CrossRefAddBookmark
	'交叉引用及文本超链接添加书签
	CrossRefAddBack
	
	Dim maxSection As Long
	Dim secIndex As Long
	maxSection = doc.Sections.Count
	ReDim sectionPara(1 To maxSection) ' Section 编号是从 1 开始
  	Dim paraIndex As Long: paraIndex = 0
	Dim para As Paragraph
	Dim topPos As Single

  	For Each para In doc.Paragraphs

    	If para.OutlineLevel >= 1 And para.OutlineLevel <= 3 Then
			
			paraIndex = paraIndex + 1

			' 直接从映射取书签名（不需要查找）
            Dim bmName As String
            If HeadingBookmarkMap.Exists(paraIndex) Then
                bmName = HeadingBookmarkMap(paraIndex)
            Else
                ' 如果映射不存在（理论上不应该），跳过
                GoTo NextPara
            End If

			' 记录每个节中的第一个标题书签
			secIndex = para.Range.Information(wdActiveEndSectionNumber)
			' vbNullString 未命中
			If sectionPara(secIndex) = vbNullString Then
				sectionPara(secIndex) = bmName
			End If


			Dim nearest As Variant
            nearest = FindNearestHeadings(bmName)
            Dim prevBm As String: prevBm = nearest(0)
            Dim nextBm As String: nextBm = nearest(1)

			Dim rng As Range
			Set rng = para.Range.Duplicate     ' 用副本避免修改原范围
			' rng.MoveEnd wdCharacter, -1        ' 
			' rng.Collapse wdCollapseStart
			topPos = rng.Information(wdVerticalPositionRelativeToPage)

			' 上一级（左）
            Dim shpPrev As Shape
            Set shpPrev = doc.Shapes.AddTextbox( _
                msoTextOrientationHorizontal, 0, 0, 24, 24, rng)
            With shpPrev
                .RelativeHorizontalPosition =  wdRelativeHorizontalPositionPage 'wdRelativeHorizontalPositionCharacter
                .RelativeVerticalPosition = wdRelativeVerticalPositionPage
                .Left = InchesToPoints(0.3)
                .Top = topPos

                With .TextFrame.TextRange
                    .Text = "上"
                    .Font.Size = 9
                    .Font.Color = RGB(0, 0, 200)
                    .Font.Underline = wdUnderlineSingle
                    .ParagraphFormat.Alignment = wdAlignParagraphCenter
                    
                    If prevBm <> "" Then
                        .Hyperlinks.Add Anchor:=.Duplicate, Address:="", _
                            SubAddress:=prevBm, ScreenTip:="上一级"
					Else
						.Font.Color = RGB(150, 150, 150)   ' 灰色
            			.Font.Underline = wdUnderlineNone  ' 无下划线
                    End If
                End With
                
                .Fill.Transparency = 1
                .Line.ForeColor.RGB = RGB(0, 0, 200)
                .Line.Visible = msoFalse
                .Line.Weight = 0.25
            End With
            
            ' 下一级（右）
            Dim shpNext As Shape
            Set shpNext = doc.Shapes.AddTextbox( _
                msoTextOrientationHorizontal, 0, 0, 24, 24, rng)
            With shpNext
                .RelativeHorizontalPosition = wdRelativeHorizontalPositionPage ' wdRelativeHorizontalPositionCharacter
                .RelativeVerticalPosition = wdRelativeVerticalPositionPage
                .Left = InchesToPoints(0.6)
                .Top = topPos
                
                With .TextFrame.TextRange
                    .Text = "下"
                    .Font.Size = 9
                    .Font.Color = RGB(0, 0, 200)
                    .Font.Underline = wdUnderlineSingle
                    .ParagraphFormat.Alignment = wdAlignParagraphCenter
                    
                    If nextBm <> "" Then
                        .Hyperlinks.Add Anchor:=.Duplicate, Address:="", _
                            SubAddress:=nextBm, ScreenTip:="下一级"
					Else
						.Font.Color = RGB(150, 150, 150)   ' 灰色
            			.Font.Underline = wdUnderlineNone  ' 无下划线
                    End If
                End With
                
                .Fill.Transparency = 1
                .Line.ForeColor.RGB = RGB(0, 0, 200)
                .Line.Visible = msoFalse
                .Line.Weight = 0.25
            End With
			
		End If
NextPara:
  	Next para

	' 设置页脚
	SetFooterNavLink

	Application.ScreenUpdating = True
	MsgBox "设置完毕", vbInformation
End Sub

' 创建目录书签
Sub CreateTOCBookmark()
    If ActiveDocument.bookmarks.Exists("toc") Then
        Exit Sub
    End If

    Dim para As Paragraph
    For Each para In ActiveDocument.paragraphs
        If para.Style = "TOC 标题" Then
            ' 找到第一个匹配的，创建书签
            ActiveDocument.bookmarks.add "toc", para.range
            Debug.Print "已创建书签 toc"
            Exit Sub
        End If
    Next para
    
    MsgBox "未找到 TOC 标题，请手动选中目录位置后运行，或检查样式。"
End Sub

' 创建标题书签
Sub CreateHeadingsBookmarks()
	Set HeadingBookmarkMap = CreateObject("Scripting.Dictionary")

    Dim doc As Document: Set doc = ActiveDocument
    
    Dim para As Paragraph
	
    Application.ScreenUpdating = False
    
    ' 可选：先删除旧的h开头的书签，避免混乱
    Dim bm As Bookmark
    For Each bm In doc.Bookmarks
        If Left(bm.Name, 2) Like "h_" Then bm.Delete
    Next bm

	Dim paraIndex As Long: paraIndex = 0
    
    For Each para In doc.Paragraphs

		If para.OutlineLevel >= 1 And para.OutlineLevel <= 3 Then
			paraIndex = paraIndex + 1

			Dim bmName As String: bmName = ""
			
			bmName = "h_" & Format(paraIndex, "0000")
        
            doc.Bookmarks.Add Name:=bmName, Range:=para.Range.Duplicate
			
            HeadingBookmarkMap(paraIndex) = bmName
        
        End If
    Next para
    
    Application.ScreenUpdating = True
    
    Debug.Print "标题书签创建完成！"
End Sub

' ===============================================================
' 函数：FindNearestHeadings
' 功能：同时查找当前标题前面最近 + 后面最近的标题
' ===============================================================
Function FindNearestHeadings(currentBmName As String) As Variant
    
    Dim doc As Document
    Set doc = ActiveDocument
    
    Dim parts() As String
    parts = Split(currentBmName, "_")
    
    ' 格式无效，直接返回空数组
    If Left(currentBmName, 2) <> "h_" Then
        FindNearestHeadings = Array("", "")
        Exit Function
    End If
    
    Dim seq As Long
	seq = CLng(Mid(currentBmName, 3))
    
    Dim prevBm As String: prevBm = ""
    Dim nextBm As String: nextBm = ""
    
 
    If seq > 1 Then
        Dim prevName As String
        prevName = "h_" & Format(seq - 1, "0000")

        If doc.Bookmarks.Exists(prevName) Then prevBm = prevName
    End If
    
    Dim nextName As String
    nextName = "h_" & Format(seq + 1, "0000")
    If doc.Bookmarks.Exists(nextName) Then nextBm = nextName
    
    ' 返回数组
    FindNearestHeadings = Array(prevBm, nextBm)
End Function


' 交叉引用 添加 书签(target_origin)
Sub CrossRefAddBookmark()
    Dim doc As Document: Set doc = ActiveDocument

    Dim fld As Field
    Dim bm As Bookmark
	Dim range As Range
	Dim bmName As String
	Dim parts() As String

	For Each bm In ActiveDocument.Bookmarks
        bmName = bm.Name
        ' 如果书签名称以 "_origin" 结尾，删除该书签
        If Right(bmName, 7) = "_origin" Then
            bm.Delete
        End If
    Next bm

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
				' 链接的书签
				' 会包括标题[1-n]
				parts = Split(fld.code.text, " ")
				Debug.Print "字段超链接: "; fld.code.text
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

			Dim orient As String
			Dim ps As PageSetup
			Dim rng As range
			Set rng = bm.Range.Duplicate     ' 用副本避免修改原范围
			Set ps = bm.range.Sections(1).PageSetup

			orient = IIf(ps.Orientation = wdOrientLandscape, "横向", "纵向")

			If rng.Information(wdWithInTable) Then
                Debug.Print "在表格中无法定位"
            Else
				' rng.Collapse wdCollapseStart
				topPos = rng.Information(wdVerticalPositionRelativeToPage)

				Dim shp As shape
				Set shp = ActiveDocument.Shapes.AddTextbox( _
					msoTextOrientationHorizontal, _
					0, 0, 34+24, 24, rng) ' 返回信息多的时候，还得修改长度
				
				' 设置形状相对界面定位
				With shp
					.RelativeHorizontalPosition = wdRelativeHorizontalPositionPage
					.RelativeVerticalPosition = wdRelativeVerticalPositionPage
					
					.Left = ps.pageWidth - 46 ' InchesToPoints(0)       ' 向右偏移（可自行调整0.2~0.5）
					.Top = topPos

					With .TextFrame.TextRange
						' 加入超链接 - 跳转
						.Hyperlinks.add _
							Anchor:=.Duplicate, _
							Address:="", _
							SubAddress:=bmOriginName, _
							TextToDisplay:="返回"&Replace(bmName, "ref", ""), _
							ScreenTip:="点击返回"

						.Font.Size = 9
						.Font.Color = RGB(0, 0, 200)           ' 文字
						.Font.Underline = wdUnderlineSingle    ' 加底
						.ParagraphFormat.Alignment = wdAlignParagraphCenter
					End With
					' 外观设置
					.Fill.Transparency = 1                     ' 1 = 完全透明
					.Line.ForeColor.RGB = RGB(0, 0, 200)       ' 深藍色边框
					.Line.Visible = msoFalse                    ' 显示边框
					.Line.Weight = 0.25                        ' 边框粗细
				End With ' 定位到标题末尾（包含段落标记前）
			End If
		End If
    Next bm
	Debug.Print "已为 交叉引用 插入返回！"
End Sub


Function GetBookmarkFromFieldCode(fieldCode As String) As String
    Dim parts() As String
    Dim bookmarkName As String
	'Word 的 Field 结构是：<空格>REF <书签名> <开关>
    ' 交叉引用域代码示例: " REF _Ref123456 \h"
    parts = Split(fieldCode, " ")
    
    If UBound(parts) > 0 Then
        bookmarkName = parts(2) ' 书签名在第三部分
		Debug.Print "-->: " & bookmarkName
    Else
        bookmarkName = ""
    End If
    
    GetBookmarkFromFieldCode = bookmarkName
End Function

' 获取前节的标题书签
Function FindPrevBookmark(index As Long) As String
	Dim i As Long
	For i = index - 1 To 1 Step -1
		If sectionPara(i) <> vbNullString Then
            FindPrevBookmark = sectionPara(i)
            Exit Function
        End If
	Next
	FindPrevBookmark = ""
End Function

' 获取后节的标题书签
Function FindNextBookmark(index As Long, sectionLen As Long) As String
	Dim i As Long
	For i = index + 1 To sectionLen
        If sectionPara(i) <> vbNullString Then
            FindNextBookmark = sectionPara(i)
            Exit Function
        End If
    Next
    FindNextBookmark = ""
End Function

' 设置页脚
Sub SetFooterNavLink()
	Dim sectionLen As Long
	Dim sec As Section
    Dim i As Long
	Dim rPrev As Range
	Dim rToc As Range
	Dim rNext As Range
    Dim prevBm As String
    Dim nextBm As String
    Dim footerRange As Range

	sectionLen = UBound(sectionPara) ' ActiveDocument.Sections.Count
	For i = 2 To sectionLen
		Set sec = ActiveDocument.Sections(i)

		prevBm = FindPrevBookmark(i)
		nextBm = FindNextBookmark(i, sectionLen)

		With sec.Footers(wdHeaderFooterPrimary)
			.LinkToPrevious = False ' 断开链接到前一节

			Set footerRange = .Range
			footerRange.Text = ""
			
			' 上一节
			If prevBm <> "" Then
				Set rPrev = footerRange.Duplicate
        		rPrev.Collapse wdCollapseStart

				ActiveDocument.Hyperlinks.Add _
					Anchor:=rPrev, _
					SubAddress:=prevBm, _
					TextToDisplay:="上一节"

			footerRange.InsertAfter "  "
			End If

			' 插入目录
			Set rToc = footerRange.Duplicate
			rToc.Collapse wdCollapseEnd

			ActiveDocument.Hyperlinks.Add _
				Anchor:=rToc, _
				SubAddress:="toc", _
				TextToDisplay:="目录"
			
			' 下一节
			If nextBm <> "" Then
				footerRange.InsertAfter "  "
				Set rNext = footerRange.Duplicate
        		rNext.Collapse wdCollapseEnd
				
				ActiveDocument.Hyperlinks.Add _
					Anchor:=rNext, _
					SubAddress:=nextBm, _
					TextToDisplay:="下一节"
			End If
		End With
	Next
	Debug.Print "每节页脚创建完成！"
End Sub

Sub TestFindNearest()
    Dim result As Variant
    result = FindNearestHeadings("h_0002")
    
    MsgBox "前面最近：" & IIf(result(0) = "", "(无)", result(0)) & vbCrLf & _
           "后面最近：" & IIf(result(1) = "", "(无)", result(1))
End Sub