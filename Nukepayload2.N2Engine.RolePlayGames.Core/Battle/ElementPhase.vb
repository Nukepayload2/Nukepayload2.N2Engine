''' <summary>
''' 典型的元素属性和变换
''' </summary>
<Flags>
Public Enum ElementPhase
    ''' <summary>
    ''' 没有属性，纯物理
    ''' </summary>
    None
    ''' <summary>
    ''' 水属性
    ''' </summary>
    Water
    ''' <summary>
    ''' 风属性
    ''' </summary>
    Wind
    ''' <summary>
    ''' 冰属性
    ''' </summary>
    Ice
    ''' <summary>
    ''' 火属性
    ''' </summary>
    Fire
    ''' <summary>
    ''' 沸属性
    ''' </summary>
    Steam
    ''' <summary>
    ''' 灼属性
    ''' </summary>
    Scorch
    ''' <summary>
    ''' 土/岩 属性
    ''' </summary>
    Earth = 8
    ''' <summary>
    ''' 木属性
    ''' </summary>
    Wood
    ''' <summary>
    ''' 溶属性
    ''' </summary>
    Gas = Wood
    ''' <summary>
    ''' 熔属性
    ''' </summary>
    Lava = Earth Or Fire
    ''' <summary>
    ''' 雷属性
    ''' </summary>
    Thunder = 16
    ''' <summary>
    ''' 岚属性
    ''' </summary>
    Laser
    ''' <summary>
    ''' 爆属性
    ''' </summary>
    Explosion = Earth Or Thunder
    ''' <summary>
    ''' 阴属性
    ''' </summary>
    Yin = 32
    ''' <summary>
    ''' 阳属性
    ''' </summary>
    Yang = 64
    ''' <summary>
    ''' 尘属性
    ''' </summary>
    Dust = Wind Or Earth Or Fire
End Enum