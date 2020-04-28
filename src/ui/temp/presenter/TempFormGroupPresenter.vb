Public Class TempFormGroupPresenter
    Implements TempFormContract.IGroupPresenter

    Public Function Insert() As Boolean Implements TempFormContract.IGroupPresenter.Insert
        
    End Function

    Public Function Delete(tab As Tab) As Boolean Implements TempFormContract.IGroupPresenter.Delete
    End Function

    Public Function Update(tab As Tab) As Boolean Implements TempFormContract.IGroupPresenter.Update
    End Function
End Class