alter table Funcionarios add CDU_CodigoBarras nvarchar(20) null

select Artigo,Descricao,CodBarras as codbarrasartigo, 'A' as armazem ,STKActual from PriDemo.dbo.Artigo
go

select Codigo,Nome,CDU_CodigoBarras from PriDemo.dbo.Funcionarios
go

