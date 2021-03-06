CREATE Function [dbo].[SpaceUpper](@Original VarChar(1024))
Returns VarChar(8000)
As
Begin

  While PatIndex('%[^ ][A-Z]%', @Original Collate Latin1_General_Bin) > 0
    Set @Original = Replace(@Original Collate Latin1_General_Bin, SubString(@Original, 1+PatIndex('%[^ ][A-Z]%', @Original Collate Latin1_General_Bin), 1), ' '+ SubString(@Original, 1+PatIndex('%[^ ][A-Z]%', @Original Collate Latin1_General_Bin), 1))

  Return LTrim(@Original)
End
GO
