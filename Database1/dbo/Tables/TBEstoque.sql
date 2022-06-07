CREATE TABLE [dbo].[TBEstoque] (
    [Numero]         INT          IDENTITY (1, 1) NOT NULL,
    [Nome]           VARCHAR (50) NULL,
    [Quantidade]     INT          NULL,
    [Data_Cadastro]  DATETIME     NULL,
    [Data_Alteracao] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([Numero] ASC)
);

