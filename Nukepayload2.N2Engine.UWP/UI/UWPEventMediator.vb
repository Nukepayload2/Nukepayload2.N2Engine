Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal
Imports Windows.UI.Core

Public Class UWPEventMediator
    Sub New(attachedView As GameCanvas)
        Me.AttachedView = attachedView
        GameWindow = Window.Current.CoreWindow
    End Sub

    Private Sub GameWindow_KeyDown(sender As CoreWindow, args As KeyEventArgs) Handles GameWindow.KeyDown
        If Not args.KeyStatus.WasKeyDown Then
            AttachedView.RaiseKeyDown(MakeGameKeyEventArgs(args))
        End If
    End Sub

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Private Shared Function MakeGameKeyEventArgs(args As KeyEventArgs) As GameKeyboardEventArgs
        Return New GameKeyboardEventArgs(CType(args.VirtualKey, Input.Key), args.KeyStatus.AsN2KeyStatus)
    End Function

    Private Sub GameWindow_KeyUp(sender As CoreWindow, args As KeyEventArgs) Handles GameWindow.KeyUp
        AttachedView.RaiseKeyUp(MakeGameKeyEventArgs(args))
    End Sub

    Dim _mouseKeyStatus(5) As Boolean

    Private Sub GameWindow_PointerPressed(sender As CoreWindow, args As PointerEventArgs) Handles GameWindow.PointerPressed
        HandlePointerButtonEvent(args, AddressOf AttachedView.RaiseTouchDown)
    End Sub

    Private Sub HandlePointerButtonEvent(args As PointerEventArgs, raise As Action(Of GameTouchEventArgs))
        Dim currentPoint = args.CurrentPoint
        Dim pos = currentPoint.Position
        Select Case currentPoint.PointerDevice.PointerDeviceType
            Case Windows.Devices.Input.PointerDeviceType.Mouse
                Dim curKeyStat(5) As Boolean
                Dim prop = currentPoint.Properties
                curKeyStat(1) = prop.IsLeftButtonPressed
                curKeyStat(2) = prop.IsRightButtonPressed
                curKeyStat(3) = prop.IsMiddleButtonPressed
                curKeyStat(4) = prop.IsXButton1Pressed
                curKeyStat(5) = prop.IsXButton2Pressed
                For i = 1 To 5
                    Dim curStat = curKeyStat(i)
                    If _mouseKeyStatus(i) <> curStat Then
                        _mouseKeyStatus(i) = curStat
                        Dim e As New GameMouseEventArgs(CType(args.KeyModifiers, Input.VirtualKeyModifiers), New System.Numerics.Vector2(CSng(pos.X), CSng(pos.Y)), CType(i, Input.MouseKeys))
                        If curStat Then
                            AttachedView.RaiseMouseButtonDown(e)
                        Else
                            AttachedView.RaiseMouseButtonUp(e)
                        End If
                    End If
                Next
            Case Else
                raise(New GameTouchEventArgs(New System.Numerics.Vector2(CSng(pos.X), CSng(pos.Y)), currentPoint.PointerId, currentPoint.Properties.Pressure))
        End Select
    End Sub

    Private Sub GameWindow_PointerReleased(sender As CoreWindow, args As PointerEventArgs) Handles GameWindow.PointerReleased
        HandlePointerButtonEvent(args, AddressOf AttachedView.RaiseTouchUp)
    End Sub

    Private Sub GameWindow_PointerWheelChanged(sender As CoreWindow, args As PointerEventArgs) Handles GameWindow.PointerWheelChanged
        Dim currentPoint = args.CurrentPoint
        Dim pos = currentPoint.Position
        Dim e As New GameMouseEventArgs(CType(args.KeyModifiers, Input.VirtualKeyModifiers), New System.Numerics.Vector2(CSng(pos.X), CSng(pos.Y)), currentPoint.Properties.MouseWheelDelta)
        AttachedView.RaiseMouseWheelChanged(e)
    End Sub

    Private Sub GameWindow_PointerMoved(sender As CoreWindow, args As PointerEventArgs) Handles GameWindow.PointerMoved
        Dim currentPoint = args.CurrentPoint
        Dim pos = currentPoint.Position
        Select Case currentPoint.PointerDevice.PointerDeviceType
            Case Windows.Devices.Input.PointerDeviceType.Mouse
                AttachedView.RaiseMouseMove(New GameMouseEventArgs(CType(args.KeyModifiers, Input.VirtualKeyModifiers), New System.Numerics.Vector2(CSng(pos.X), CSng(pos.Y))))
            Case Else
                Dim lastPoints = From p In args.GetIntermediatePoints
                                 Where p.PointerId = currentPoint.PointerId
                If lastPoints.Any Then
                    Dim pts = lastPoints.ToArray
                    Dim index = 0
                    Dim lastTime = lastPoints(0).Timestamp
                    For i = 1 To pts.Length - 1
                        Dim timestamp = pts(i).Timestamp
                        If lastTime < timestamp Then
                            index = i
                            lastTime = timestamp
                        End If
                    Next
                    Dim lastPoint = pts(index).Position
                    AttachedView.RaiseTouchMove(New GameTouchEventArgs(
                                                New System.Numerics.Vector2(CSng(pos.X), CSng(pos.Y)),
                                                New System.Numerics.Vector2(CSng(lastPoint.X), CSng(lastPoint.Y)),
                                                currentPoint.PointerId, currentPoint.Properties.Pressure))
                End If
        End Select
    End Sub

    Public Property AttachedView As GameCanvas

    WithEvents GameWindow As CoreWindow

End Class
