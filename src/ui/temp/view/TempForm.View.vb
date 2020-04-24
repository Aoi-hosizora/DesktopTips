Partial Public Class TempForm
    Implements TempFormContract.IView

    Private ReadOnly _globalPresenter As TempFormContract.IGlobalPresenter = New TempFormGlobalPresenter(Me)

    ''' <summary>
    ''' 引用 Form
    ''' </summary>
    Public Function GetMe() As TempForm Implements TempFormContract.IView.GetMe
        Return Me
    End Function

End Class
