Imports FarseerPhysics

Namespace PhysicsIntegration

    Public Module PhysicsExtension
        ''' <summary>
        ''' 转换成物理引擎的单位
        ''' </summary>
        <Extension>
        Public Function ToPhysicsUnit(displayVector2 As Vector2) As Vector2
            Return New Vector2(ConvertUnits.ToSimUnits(displayVector2.X),
                               ConvertUnits.ToSimUnits(displayVector2.Y))
        End Function
        ''' <summary>
        ''' 转换成图形显示的单位
        ''' </summary>
        <Extension>
        Public Function ToDisplayUnit(physicsVector2 As Vector2) As Vector2
            Return New Vector2(ConvertUnits.ToDisplayUnits(physicsVector2.X),
                               ConvertUnits.ToDisplayUnits(physicsVector2.Y))
        End Function

    End Module

End Namespace